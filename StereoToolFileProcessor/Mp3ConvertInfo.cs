using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StereoToolFileProcessor
{
    public class Mp3ConvertInfo
    {
        public string origem { get; set; }
        public string destino { get; set; }

        public bool JaExiste { get; set; }

        public Mp3ConvertInfo(string org, string pathDestino)
        {
            this.origem = org;
            this.destino = Path.Combine(pathDestino, Path.GetFileNameWithoutExtension(org) + ".mp3");
            this.JaExiste = File.Exists(this.destino);
        }
    }
}
