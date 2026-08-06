using FlexiScan.Qrs.Services.Data.DTOs;
using FlexiScan.Qrs.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexiScan.Qrs.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class QrCodesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IQrCodeService _qrCodeService;
        private static string? _htmlTemplateCache;
        public QrCodesController(IQrCodeService qrCodeService,
            IWebHostEnvironment env)
        {
            _env = env;
            _qrCodeService = qrCodeService;
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectAsync(string shortCode)
        {
            var metadata = new ScanMetadata
            {
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()!,
                UserAgent = Request.Headers["User-Agent"]!,
                Referrer = Request.Headers["Referer"]!
            };

            QrCodeResult qrCode = await _qrCodeService.ProcessScanAsync(shortCode, metadata);

            if (qrCode == null)
            {
                return NotFound();
            }

            if (_htmlTemplateCache == null)
            {
                string templatePath = Path.Combine(_env.ContentRootPath, "Templates", "RedirectTemplate.html");
                _htmlTemplateCache = await System.IO.File.ReadAllTextAsync(templatePath);
            }

            string finalHtml = _htmlTemplateCache
                .Replace("{{DESTINATION_URL}}", qrCode.DestinationUrl)
                .Replace("{{PIXEL_URL}}", qrCode.PixelUrl);

            return Content(finalHtml, "text/html");
        }
    }
}

