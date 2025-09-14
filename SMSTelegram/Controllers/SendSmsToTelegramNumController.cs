using Microsoft.AspNetCore.Mvc;
using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Models.Sms;

namespace SMSTelegram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SendSmsToTelegramNumController(ISendSmsToTelegramNumService sendSmsService) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] SendSmsCommand command, CancellationToken cancellationToken)
    {
        await sendSmsService.HandleAsync(command, cancellationToken);
        return Ok("Message processing completed.");
    }
}