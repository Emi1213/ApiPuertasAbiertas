using Microsoft.AspNetCore.SignalR;

namespace ApiPuertasAbiertas.API.Realtime;

public class RbacHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var perfilId = Context.GetHttpContext()!.Request.Query["perfilId"].ToString();

        if (!string.IsNullOrWhiteSpace(perfilId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"role:{perfilId}");
        }

        await base.OnConnectedAsync();
    }
}
