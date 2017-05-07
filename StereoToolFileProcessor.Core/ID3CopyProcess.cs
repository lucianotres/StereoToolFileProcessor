using System;
using System.Globalization;
using System.Text;

namespace StereoToolFileProcessor.Core
{
    /// <summary>
    /// Class to control the Lame process
    /// </summary>
    public class ID3CopyProcess : BaseProcess
    {
        public ID3CopyProcess()
            : base("id3.exe")
        {
        }
        
        /// <summary>
        /// Path to origin file
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Path to destiny file 
        /// </summary>
        public string Destiny { get; set; }

        public override void ValidateBeforeProcess()
        {
            if (string.IsNullOrEmpty(Origin))
                throw new ArgumentException("Origin file path not informed!");
            if (string.IsNullOrEmpty(Destiny))
                throw new ArgumentException("Destiny file path not informed!");
        }

        /// <summary>
        /// Generate arguments to the process
        /// </summary>
        protected override string GenerateArgs()
        {
            return $"-2 -D \"{Origin}\" \"{Destiny}\"";
        }

    }
}
