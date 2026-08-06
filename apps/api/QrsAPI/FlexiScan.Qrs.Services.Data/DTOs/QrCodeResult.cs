using FlexiScan.Qrs.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiScan.Qrs.Services.Data.DTOs
{
    public class QrCodeResult
    {
        public string? DestinationUrl { get; set; }
        public string? PixelUrl { get; set; }
        public string[] Errors { get; set; } = Array.Empty<string>();
    }
}
