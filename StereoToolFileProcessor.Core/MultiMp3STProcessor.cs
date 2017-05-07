using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StereoToolFileProcessor.Core
{
    /// <summary>
    /// Class to manage multiple Mp3STProcessors
    /// </summary>
    public class MultiMp3STProcessor
    {
        private int _MaxConcurrentProcesses;
        private List<FileProcessInfo> _inputPaths;
        private object _lock = new object();
        private List<Mp3STProcessor> _processors;

        public MultiMp3STProcessor()
        {
            _MaxConcurrentProcesses = 4;
            _inputPaths = new List<FileProcessInfo>();
            _processors = new List<Mp3STProcessor>();
        }

        /// <summary>
        /// Number of the concurrent processes of Mp3STProcessor
        /// </summary>
        public int MaxConcurrentProcesses
        {
            get => _MaxConcurrentProcesses;
            set => _MaxConcurrentProcesses = (value < 1 ? 1 : value);
        }

        /// <summary>
        /// Directory to put all output files
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// List of files to execute the process
        /// </summary>
        public IList<FileProcessInfo> InputPaths { get => _inputPaths; }

        /// <summary>
        /// Path to the config file for Stereo Tool
        /// </summary>
        public string STConfigFile { get; set; }

        /// <summary>
        /// Options for Lame Encode
        /// </summary>
        public LameOptions EncodeOptions { get; set; }


        /// <summary>
        /// Every time that one process ends
        /// </summary>
        public event EventHandler ProcessProgress;
        

        private void DoProgress(FileProcessInfo fpi)
        {
            lock (_lock)
            {
                ProcessProgress?.Invoke(this, EventArgs.Empty);
            }
        }
        

        private async Task ExecuteNext(Mp3STProcessor p, CancellationToken ct)
        {
            //locate the next one to execute..
            FileProcessInfo n;
            lock (_lock)
            {
                n = InputPaths.FirstOrDefault(w => !w.Done && !w.Processing);
                if (n != null)
                {
                    n.Processing = true;
                }
            }
            if (n != null)
            {
                try
                {
                    await p.Execute(n.Path, OutputPath, ct);
                }
                catch (Exception err)
                {
                    n.Done = false;
                    n.Error = err.Message;
                }
                finally
                {
                    //ends processing
                    n.Done = true;
                    n.Processing = false;

                    //raise progress event
                    DoProgress(n);

                    //try to execute the next, if not cancelled
                    if (!ct.IsCancellationRequested)
                    {
                        await ExecuteNext(p, ct);
                    }
                }
            }
        }


        public async Task Execute(CancellationToken cancellationToken)
        {
            //clear status
            foreach(var f in _inputPaths)
            {
                f.Done = false;
                f.Processing = false;
            }

            //create the processors
            _processors.Clear();
            for (int i = 0; i < MaxConcurrentProcesses; i++)
            {
                _processors.Add(new Mp3STProcessor()
                {
                    PathSTConfigs = STConfigFile,
                    EncodeOptions = EncodeOptions
                });
            }

            //execute the processors
            var a = _processors
                .Select(s => ExecuteNext(s, cancellationToken))
                .ToList();

            //then wait all
            await Task.WhenAll(a); 
        }

    }
}
