using System;
using System.Text;

namespace StereoToolFileProcessor.Core
{
    /// <summary>
    /// Class to control the stereo_tool_cmd process
    /// </summary>
    public class StereoToolProcess : BaseProcess
    {
        public StereoToolProcess()
            :base("stereo_tool_cmd.exe")
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

        /// <summary>
        /// Path to config file 
        /// </summary>
        public string Config { get; set; }

        public override void ValidateBeforeProcess()
        {
            base.ValidateBeforeProcess();

            if (string.IsNullOrEmpty(Origin))
                throw new ArgumentException("Origin file path not informed!");

            if (string.IsNullOrEmpty(Config))
                throw new ArgumentException("Config file path not informed!");
        }

        /// <summary>
        /// Generate arguments to the process
        /// </summary>
        protected override string GenerateArgs()
        {
            var sb = new StringBuilder();

            //config
            var pathDst = Destiny;
            if (string.IsNullOrEmpty(pathDst))
                pathDst = "-";
            else
                pathDst = $"\"{pathDst}\"";

            //Format args
            //sb.Append(" -v");
            sb.Append($" \"{Origin}\" ");
            sb.Append(pathDst);
            sb.Append($" -s \"{Config}\"");

            return sb.ToString();
        }
        
    }
}
