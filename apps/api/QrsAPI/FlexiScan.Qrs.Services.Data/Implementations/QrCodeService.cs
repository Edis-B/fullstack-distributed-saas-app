using FlexiScan.Qrs.Data;
using FlexiScan.Qrs.Services.Data.DTOs;
using FlexiScan.Qrs.Services.Data.Interfaces;
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

        public Task<QrCodeResult> GetQrCodeAsync(string shortCode)
        {
            throw new NotImplementedException();
        }
    }
}
