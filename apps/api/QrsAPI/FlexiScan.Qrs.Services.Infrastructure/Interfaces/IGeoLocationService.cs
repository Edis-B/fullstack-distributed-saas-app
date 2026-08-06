using FlexiScan.Qrs.Services.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiScan.Qrs.Services.Infrastructure.Interfaces
{
    public interface IGeoLocationService
    {
        Task<LocationData> GetLocationDataAsync(string ipAdress);
    }
}
