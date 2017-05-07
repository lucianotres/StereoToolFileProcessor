using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StereoToolFileProcessor.Core
{
    public class FileProcessInfo
    {
        public string Path { get; set; }
        public string Error { get; set; }
        public bool Done { get; set; }
        public bool Processing { get; set; }
    }
}
