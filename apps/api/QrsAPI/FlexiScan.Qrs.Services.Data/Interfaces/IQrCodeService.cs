using FlexiScan.Qrs.Services.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiScan.Qrs.Services.Data.Interfaces
{
    public interface IQrCodeService
    {
        Task<QrCodeResult> ProcessScanAsync(string shortCode, ScanMetadata scanMetadata);
    }
}
