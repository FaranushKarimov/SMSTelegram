using Microsoft.AspNetCore.Mvc;
using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Models.Sms;
using SMSTelegram.Filters;

namespace SMSTelegram.Controllers;

[ApiController]
[Route("api/[controller]")]
[TypeFilter<ApiExceptionFilter>]
public class SendSmsToTelegramNumController(ISendSmsToTelegramNumService sendSmsService) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] SendSmsCommand command, CancellationToken cancellationToken)
    {
        await sendSmsService.HandleAsync(command, cancellationToken);
        return Ok("Message processing completed.");
    }
    
    [HttpPost("broadcast")]
    public async Task<IActionResult> Broadcast([FromBody] string message, CancellationToken cancellationToken)
    {
        await sendSmsService.BroadcastAsync(message, cancellationToken);
        return Ok("Broadcast completed.");
    }
}