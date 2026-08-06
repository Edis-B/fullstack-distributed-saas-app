using FlexiScan.Qrs.Services.Infrastructure.DTOs;
using FlexiScan.Qrs.Services.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace FlexiScan.Qrs.Services.Infrastructure.Implementations
{
    public class IpApiGeoLocationService : IGeoLocationService
    {
        private readonly HttpClient _httpClient;
        public IpApiGeoLocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LocationData> GetLocationDataAsync(string ipAddress)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://ip-api.com/json/{ipAddress}");

            var data = await response.Content.ReadAsStringAsync();

            return new LocationData
            {

            };
        }
    }
}
