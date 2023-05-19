using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class WsTestController : ControllerBase
    {
        [HttpGet("/aa")]
        public string GetAa()
        {
            return "aa";
        }

        
        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            while (true)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("aa")), WebSocketMessageType.Text, false, CancellationToken.None);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
