using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FlexiScan.Qrs.Data.Models
{
    [PrimaryKey(nameof(UserId), nameof(YearMonth))]
    public class UserUsageCache
    {
        public string UserId { get; set; } = null!;
        public string YearMonth { get; set; } = null!;
        public int CurrentMonthScans { get; set; }
        public int TotalActiveCodes { get; set; }
        public bool IsScanLimitReached { get; set; }
    }
}
