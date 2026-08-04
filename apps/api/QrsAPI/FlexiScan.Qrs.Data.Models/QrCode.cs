using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexiScan.Qrs.Data.Models
{
    public class QrCode
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string ShortCode { get; set; } = null!;
        public string DestinationUrl { get; set; } = null!;
        public bool IsActive { get; set; }

        public string? TrackingPixelId { get; set; }
        public string? LogoUrl { get; set; }
        public Guid? CustomDomainId { get; set; }

        [ForeignKey(nameof(CustomDomainId))]
        public CustomDomain? CustomDomain { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
