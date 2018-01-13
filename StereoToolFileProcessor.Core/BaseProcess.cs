using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StereoToolFileProcessor.Core
{
    /// <summary>
    /// Base class to control the execution of the process
    /// </summary>
    public abstract class BaseProcess : IDisposable
    {
        protected Process _pr;

        public BaseProcess(string fileProcess)
        {
            //create process class
            _pr = new Process();
            _pr.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileProcess);

            _pr.StartInfo.CreateNoWindow = true;
            _pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _pr.StartInfo.UseShellExecute = false;

            //handle events
            _pr.OutputDataReceived += (ss, ee) =>
            {
                OutputReceived?.Invoke(this, ee);
            };
            _pr.ErrorDataReceived += (ss, ee) =>
            {
                OutputReceived?.Invoke(this, ee);
            };
        }

        /// <summary>
        /// Process output event
        /// </summary>
        public event DataReceivedEventHandler OutputReceived;

        /// <summary>
        /// Generate arguments to the process
        /// </summary>
        protected abstract string GenerateArgs();

        /// <summary>
        /// Do all validation needed to execute the process
        /// </summary>
        public virtual void ValidateBeforeProcess()
        {
            if (!File.Exists(_pr.StartInfo.FileName))
            {
                var n = Path.GetFileName(_pr.StartInfo.FileName);
                throw new FileNotFoundException($"File \"{n}\" not found for processing!", n);
            }
        }

        /// <summary>
        /// Execute the process in a hidden window
        /// </summary>
        public async Task<bool> Execute()
        {
            return await Execute(CancellationToken.None);
        }

        /// <summary>
        /// Execute the process in a hidden window
        /// </summary>
        /// <param name="cancToken">Cancellation Token</param>
        public virtual async Task<bool> Execute(CancellationToken cancToken)
        {
            //do the validation
            ValidateBeforeProcess();

            //create args
            var args = GenerateArgs();

            //runs in a thread
            return await Task.Run<bool>(() =>
            {
                var bOutputEvent = (OutputReceived != null);
                _pr.StartInfo.RedirectStandardError = bOutputEvent;
                _pr.StartInfo.RedirectStandardOutput = bOutputEvent;

                _pr.StartInfo.Arguments = args;
                _pr.Start();

                if (bOutputEvent)
                {
                    _pr.BeginOutputReadLine();
                    _pr.BeginErrorReadLine();
                }

                //wait for finish, or cancel
                while (!_pr.HasExited)
                {
                    _pr.WaitForExit(500);
                    if (cancToken.IsCancellationRequested)
                    {
                        _pr.Kill();
                        _pr.WaitForExit(5000); //wait kill hapens
                        break;
                    }
                }
                return _pr.HasExited && (_pr.ExitCode == 0);
            }, cancToken);
        }


        public virtual void Dispose()
        {
            if (_pr != null)
            {
                _pr.Dispose();
                _pr = null;
            }
        }
    }
}
