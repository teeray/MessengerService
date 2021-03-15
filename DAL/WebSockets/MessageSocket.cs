using Microsoft.AspNetCore.SignalR;
using Models.Messengers;
using System.Threading.Tasks;

namespace DAL.WebSockets
{
    public class MessageSocket : Hub
    {
        public async Task sendPool(Pool pool)
        {
            await this.Clients.All.SendAsync("sendPool", pool);
        }
        public async Task SendMessage(Message message)
        {
            await this.Clients.All.SendAsync("sendMessage", message);
        }
        public async Task AddMember(Member member)
        {
            await this.Clients.All.SendAsync("addMember", member);
        }
        public async Task UpdateMember(Member member)
        {
            await this.Clients.All.SendAsync("updateMember", member);
        }
    }
}
