using FlexiScan.Qrs.Data;
using FlexiScan.Qrs.Services.Data.DTOs;
using FlexiScan.Qrs.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiScan.Qrs.Services.Data.Implementations
{
    public class QrCodeService : IQrCodeService
    {
        private readonly QrsDbContext _qrsDbContext;
        public QrCodeService(QrsDbContext qrsDbContext)
        {
            _qrsDbContext = qrsDbContext;
        }

        public async Task<QrCodeResult> ProcessScanAsync(string shortCode, ScanMetadata scanMetadata)
        {
            var qrCode = await _qrsDbContext.QrCodes
                .FirstOrDefaultAsync(x => x.ShortCode == shortCode);

            if (qrCode == null)
            {
                return null!;
            }

            new ScanEvent
            {
                QrCodeId = qrCode.Id,
                TimeStamp = DateTime.Now,
                AnonymizedIp = scanMetadata.IpAddress,
                Country = scanMetadata.UserAgent
            };

            return new QrCodeResult
            {
                DestinationUrl = qrCode.DestinationUrl,
                PixelUrl = qrCode.TrackingPixelId
            };
        }
    }
}
