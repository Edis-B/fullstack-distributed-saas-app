using FlexiScan.Qrs.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FlexiScan.Qrs.Services.Data
{
    public class ScanEvent
    {
        public Guid Id { get; set; }
        
        public Guid QrCodeId { get; set; }
        [ForeignKey(nameof(QrCodeId))]
        public QrCode QrCode { get; set; } = null!;

        public DateTime TimeStamp { get; set; }
        public string AnonymizedIp { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string DeviceType { get; set; } = null!;
        public string OperatingSystem { get; set; } = null!;
    }
}
