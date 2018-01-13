using System;
using System.Collections.Generic;
using System.IO;
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
        /// Length of path from base directory to build directory tree
        /// </summary>
        public int OrignBasePathLength { get; set; }

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
        /// Skip the processing if it exists in the destiny
        /// </summary>
        public bool SkipIfItExists { get; set; }

        /// <summary>
        /// Every time that one process ends
        /// </summary>
        public event EventHandler<ProcessProgressArgs> ProcessProgress;


        public class ProcessProgressArgs : EventArgs
        {
            public FileProcessInfo Info { get; set; }
        }

        private void DoProgress(FileProcessInfo fpi)
        {
            lock (_lock)
            {
                ProcessProgress?.Invoke(this, new ProcessProgressArgs { Info = fpi });
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
                    var otpPath = await ResolverOutputPath(n);

                    //skip with it exisits?
                    if (SkipIfItExists && File.Exists(Path.Combine(otpPath, Path.GetFileName(n.Path))))
                    {
                        n.Error = "Skiped";
                    }
                    else
                    {
                        await p.Execute(n.Path, otpPath, ct);
                    }
                }
                catch (Exception err)
                {
                    n.Error = err.Message;
                }
                finally
                {
                    //ends processing
                    n.Done = true;
                    n.Processing = false;
                    if (ct.IsCancellationRequested)
                    {
                        n.Error = "Canceled";
                    }

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

        /// <summary>
        /// Check directory tree for output file
        /// </summary>
        private async Task<string> ResolverOutputPath(FileProcessInfo n)
        {
            return await Task.Run(() =>
            {
                //get tree
                var dif = Path.GetDirectoryName(n.Path)
                    .Substring(OrignBasePathLength)
                    .Split('\\')
                    .Where(w => !string.IsNullOrEmpty(w))
                    .ToList();

                //build the tree
                var path = OutputPath;
                foreach (var p in dif)
                {
                    path = Path.Combine(path, p);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }

                return path;
            });
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            //clear status
            foreach (var f in _inputPaths)
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
