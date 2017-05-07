namespace StereoToolFileProcessor.Core
{
    /// <summary>
    /// Describes the options for Lame
    /// </summary>
    public class LameOptions
    {

        /// <summary>
        /// Gain control value
        /// </summary>
        public decimal? Gain { get; set; }

        /// <summary>
        /// Swap the stereo channels
        /// </summary>
        public bool SwapChannel { get; set; }

        /// <summary>
        /// Lowpass filter value
        /// </summary>
        public int? Lowpass { get; set; }

        /// <summary>
        /// Width for the lowpass filter
        /// </summary>
        public int? LowpassWidth { get; set; }

        /// <summary>
        /// Highpass filter value
        /// </summary>
        public int? Highpass { get; set; }

        /// <summary>
        /// Width for the higpass filter
        /// </summary>
        public int? HighpassWidth { get; set; }

        /// <summary>
        /// Resample frequency
        /// </summary>
        public int? Resample { get; set; }


        /// <summary>
        /// Quality of the internal algorithm 0..9, 0 being the slowest and the highest quality. Default it's 3
        /// </summary>
        public short? Quality { get; set; }

        /// <summary>
        /// Prioridade de processo: 0,1 = baixa, 2 = normal, 3,4 = alta; padrão é 0.
        /// </summary>
        public short? Priority { get; set; }


        /// <summary>
        /// Constant birate (CBR) or (VBR) variable min: 8, 16, 24, ..., 320
        /// </summary>
        public int? Birate { get; set; }

        /// <summary>
        /// Only for (VBR), maximum
        /// </summary>
        public int? BirateMax { get; set; }

        /// <summary>
        /// Use VBR? For true it's necessary to set the quality between 0 (highest quality) and 9999 (lowest quality), to default sets 4.
        /// </summary>
        public short? VBR { get; set; }

        /// <summary>
        /// Set No Replay Gain
        /// </summary>
        public bool NoReplayGain { get; set; }



        
        public LameOptions()
        {
        }

        public LameOptions(int cbr)
        {
            VBR = null;
            Birate = cbr;
            BirateMax = null;
        }

        public LameOptions(int vbrMin, int vbrMax)
        {
            VBR = 4;
            Birate = vbrMin;
            BirateMax = vbrMax;
        }

    }
}
