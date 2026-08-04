using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FlexiScan.Qrs.Data.Models
{
    public class CustomDomain
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string DomainName { get; set; } = null!;
        public bool IsVerified { get; set; }
    }
}
