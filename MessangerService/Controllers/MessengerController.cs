using DAL.Repository;
using DAL.WebSockets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.Messengers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MessangerService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessengerController : ControllerBase
    {
        MessengerRepository repo;
        IHubContext<MessageSocket> hub;
        public MessengerController(MessengerRepository _repo, IHubContext<MessageSocket> _hub)
        {
            repo = _repo;
            hub = _hub;
        }
        [HttpGet,Route("api/v1/pools/members/{authId}")]
        public IActionResult GetPools(string authId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, repo.GetPools().Where(w => w.Members.Any(a => a.AuthID == authId)).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPost,Route("api/v1/pools")]
        public async Task<IActionResult> PostPools([FromBody] Pool model)
        {
            try
            {
                var pool = repo.PostPool(model);
                await hub.Clients.All.SendAsync("sendPool", pool);
                return StatusCode(StatusCodes.Status200OK, pool);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPost, Route("api/v1/pools/messages")]
        public async Task<IActionResult> PostMessages([FromBody] Message model)
        {
            try
            {
                var message = repo.PostMessage(model);
                await hub.Clients.All.SendAsync("sendMessage", message);
                return StatusCode(StatusCodes.Status200OK, message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost, Route("api/v1/pools/members")]
        public async Task<IActionResult> PostMembers([FromBody] Member model)
        {
            try
            {
                var members = repo.PostMember(model);
                await hub.Clients.All.SendAsync("addMember", members);
                return StatusCode(StatusCodes.Status200OK, members);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPut, Route("api/v1/pools/members")]
        public async Task<IActionResult> PutMembers([FromBody] Member model)
        {
            try
            {
                var members = repo.PutMember(model);
                await hub.Clients.All.SendAsync("updateMember", members);
                return StatusCode(StatusCodes.Status200OK,members);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
