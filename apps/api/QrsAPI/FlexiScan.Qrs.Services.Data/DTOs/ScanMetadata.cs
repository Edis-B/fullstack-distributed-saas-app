using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiScan.Qrs.Services.Data.DTOs
{
    public class ScanMetadata
    {
        public string IpAddress { get; set; } = null!;
        public string UserAgent { get; set; } = null!;
        public string Referrer { get; set; } = null!;
    }

}
