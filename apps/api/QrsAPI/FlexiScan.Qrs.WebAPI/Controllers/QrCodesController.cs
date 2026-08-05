using FlexiScan.Qrs.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexiScan.Qrs.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class QrCodesController : ControllerBase
    {
        private readonly IQrCodeService _qrCodeService;
        public QrCodesController(IQrCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> GetQrCodeDestinationAsync(string shortCode)
        {
            var qrCode = await _qrCodeService.GetQrCodeAsync(shortCode);

            return Ok(qrCode);
        }
    }
}
