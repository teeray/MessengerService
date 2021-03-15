using DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Users;
using System;
using System.Linq;

namespace UserService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserRepository repo;
        public UserController(UserRepository _repo)
        {
            repo = _repo;
        }
        [HttpGet, Route("api/v1/users")]
        public IActionResult GetUsers()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, repo.GetUsers().Where(w => !w.IsDeleted && !w.IsDisabled).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpGet,Route("api/v1/users/{authId}")]
        public IActionResult GetUser(string authId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, repo.GetUsers().FirstOrDefault(f => f.AuthID == authId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPost,Route("api/v1/users")]
        public IActionResult PostUser([FromBody] User model)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, repo.PostUser(model));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPut, Route("api/v1/users")]
        public IActionResult PutUser([FromBody] User model)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, repo.PutUser(model));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
