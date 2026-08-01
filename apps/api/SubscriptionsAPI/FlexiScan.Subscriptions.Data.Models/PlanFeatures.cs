using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiScan.Subscriptions.Data.Models
{
    public class PlanFeatures
    {
        public int MaxActiveCodes { get; set; }
        public int MaxDailyScans { get; set; }
        public bool HasAdvancedAnalytics { get; set; }
        public bool AllowCustomLogos { get; set; }
        public bool AllowTrackingPixels { get; set; }
        public bool AllowCustomDomains { get; set; }
        public bool AllowApiAccess { get; set; }
    }
}
