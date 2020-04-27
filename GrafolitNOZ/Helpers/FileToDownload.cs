using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Helpers
{
    public class FileToDownload
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] Content { get; set; }
    }
}