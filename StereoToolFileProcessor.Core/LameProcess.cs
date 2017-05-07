using System;
using System.Globalization;
using System.Text;

namespace StereoToolFileProcessor.Core
{
    /// <summary>
    /// Class to control the Lame process
    /// </summary>
    public class LameProcess : BaseProcess
    {
        public LameProcess()
            : base("lame.exe")
        {
        }
        
        /// <summary>
        /// It's to decode or encode?
        /// </summary>
        public bool Decode { get; set; }

        /// <summary>
        /// Path to origin file
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Path to destiny file 
        /// </summary>
        public string Destiny { get; set; }

        /// <summary>
        /// Options for lame
        /// </summary>
        public LameOptions Options { get; set; }

        public override void ValidateBeforeProcess()
        {
            if (string.IsNullOrEmpty(Origin))
                throw new ArgumentException("Origin file path not informed!");

            if (!Decode && (Options == null))
                throw new ArgumentException("Options not informed to encode!");
        }

        /// <summary>
        /// Generate arguments to the process
        /// </summary>
        protected override string GenerateArgs()
        {
            var pathDst = Destiny;
            if (string.IsNullOrEmpty(pathDst))
                pathDst = "-";
            else
                pathDst = $"\"{pathDst}\"";

            var sb = new StringBuilder();
            if (Decode)
            {
                sb.Append(" --decode");
            }
            else
            {
                if (Options.VBR.HasValue)
                    sb.Append($" -V {Options.VBR.Value}");

                if (Options.Birate.HasValue)
                    sb.Append($" -b {Options.Birate.Value}");
                if (Options.BirateMax.HasValue)
                    sb.Append($" -B {Options.BirateMax.Value}");

                if (Options.Gain.HasValue)
                    sb.Append($" --gain {(Options.Gain.Value >= 0 ? "+" : "-")}{Options.Gain.Value.ToString("0.00", CultureInfo.InvariantCulture)}");

                if (Options.NoReplayGain)
                    sb.Append(" --noreplaygain");
            }

            //path
            sb.Append($" \"{Origin}\" ");
            sb.Append(pathDst);

            return sb.ToString();
        }

    }
}
