using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StereoToolFileProcessor.Core
{
    /// <summary>
    /// Class to control all the process for doing the Stereo Tool magic in one MP3 file.
    /// </summary>
    public class Mp3STProcessor : IDisposable
    {
        static Random _rdn;
        LameProcess _decoder;
        LameProcess _encoder;
        StereoToolProcess _stp;
        ID3CopyProcess _id3;

        public Mp3STProcessor()
        {
            _decoder = new LameProcess()
            {
                Decode = true
            };
            _encoder = new LameProcess()
            {
                Decode = false
            };
            _stp = new StereoToolProcess();
            //_stp.OutputReceived += _OutputReceived_toTest;
            _id3 = new ID3CopyProcess();
        }

        /* //for test only
        static StreamWriter log = null;
        private void _OutputReceived_toTest(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (log == null)
            {
                log = new StreamWriter(@"E:\temp\stp.log", true);
            }
            log.Write(e.Data);
            log.Flush();
        }*/

        /// <summary>
        /// Read or set options for lame encode
        /// </summary>
        public LameOptions EncodeOptions
        {
            get { return _encoder.Options; }
            set { _encoder.Options = value; }
        }
        
        /// <summary>
        /// Path to Stereo Tools config's file
        /// </summary>
        public string PathSTConfigs
        {
            get { return _stp.Config; }
            set { _stp.Config = value; }
        }


        /// <summary>
        /// Runs a conversion from origin file to destination
        /// </summary>
        /// <param name="originPath">Path to the original file (mp3)</param>
        /// <param name="outputDir">Destination directory after conversion</param>
        public async Task<bool> Execute(string originPath, string outputDir)
        {
            return await Execute(originPath, outputDir, CancellationToken.None);
        }

        /// <summary>
        /// Runs a conversion from origin file to destination
        /// </summary>
        /// <param name="originPath">Path to the original file (mp3)</param>
        /// <param name="outputDir">Destination directory after conversion</param>
        /// <param name="cancellationToken">Cancellation token to allow cancellation of the conversion process</param>
        public async Task<bool> Execute(string originPath, string outputDir, CancellationToken cancellationToken)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            //configure paths
            _decoder.Origin = originPath;
            _decoder.Destiny = Path.Combine(baseDir, $"_temp_toprocess_wave_{(_rdn ?? (_rdn = new Random())).Next(1, 9999999)}.wav");
            _stp.Origin = _decoder.Destiny;
            _stp.Destiny = Path.Combine(baseDir, $"_temp_processed_wave_{(_rdn ?? (_rdn = new Random())).Next(1, 9999999)}.wav");
            _encoder.Origin = _stp.Destiny;
            _encoder.Destiny = Path.Combine(outputDir, Path.GetFileName(originPath));
            _id3.Origin = originPath; //initial mp3 file
            _id3.Destiny = _encoder.Destiny; //final processed mp3 file

            //validate if all set
            _decoder.ValidateBeforeProcess();
            _stp.ValidateBeforeProcess();
            _encoder.ValidateBeforeProcess();
            _id3.ValidateBeforeProcess();

            try
            {
                //first of all we need to decode mp3 file into wav..
                bool bOk = await _decoder.Execute(cancellationToken);

                //now if it's ok we need to process with Stereo Tool (long time here)
                if (bOk)
                {
                    bOk = await _stp.Execute(cancellationToken);

                    //delete initial temp wave
                    if (File.Exists(_stp.Origin))
                    {
                        File.Delete(_stp.Origin);
                    }
                }

                //again, if's ok encode again into mp3 file at output dir
                if (bOk)
                {
                    bOk = await _encoder.Execute(cancellationToken);

                    //delete processed temp wave
                    if (File.Exists(_stp.Destiny))
                    {
                        File.Delete(_stp.Destiny);
                    }
                }

                //at last if everything it's ok, copy ID3 tags
                if (bOk)
                {
                    bOk = await _id3.Execute(cancellationToken);
                }

                return bOk;
            }
            finally
            {
                if (File.Exists(_decoder.Destiny))
                {
                    File.Delete(_decoder.Destiny);
                }
                if (File.Exists(_stp.Destiny))
                {
                    File.Delete(_stp.Destiny);
                }
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            _decoder.Dispose();
            _encoder.Dispose();
            _stp.Dispose();
            _id3.Dispose();
        }
    }
}
