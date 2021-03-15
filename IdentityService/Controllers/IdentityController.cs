using DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.ViewModel;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        IdentityRepository repo;
        public IdentityController(IdentityRepository _repo)
        {
            repo = _repo;
        }
        [HttpGet, Route("api/v1/checklogin")]
        public async Task<IActionResult> GetCheckLogin()
        {
            try
            {
                var login = await repo.GetUserByToken(User);
                if(login == null)
                {
                    return StatusCode(StatusCodes.Status200OK, null);
                }
                var token = await repo.GetJWTToken(login.UserName);
                return StatusCode(StatusCodes.Status200OK, new LoginViewModel() { Token = token, User = login });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost, Route("api/v1/registration")]
        public async Task<IActionResult> PostRegistration([FromBody] LoginDTO model)
        {
            try
            {
                var reg = await repo.PostRegistration(model.UserName, model.Password);
                var login = await repo.PostLogin(model.UserName, model.Password);
                var token = await repo.GetJWTToken(model.UserName);
                return StatusCode(StatusCodes.Status200OK, new LoginViewModel() { Token = token, User = login });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

            }
        }
        [AllowAnonymous]
        [HttpPost,Route("api/v1/login")]
        public async Task<IActionResult> PostLogin([FromBody] LoginDTO model)
        {
            try
            {
                var login = await repo.PostLogin(model.UserName, model.Password);
                var token = await repo.GetJWTToken(model.UserName);
                return StatusCode(StatusCodes.Status200OK, new LoginViewModel() { Token = token, User = login });

            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet,Route("api/v1/passwordreset/token/{username}")]
        public async Task<IActionResult> GetPasswordResetToken(string username)
        {
            try
            {
                var token = await repo.GetPasswordResetToken(username);

                return StatusCode(StatusCodes.Status200OK, new PasswordResetDTO() { Code  = token});
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost,Route("api/v1/passwordreset")]
        public async Task<IActionResult> PostPasswordReset([FromBody] PasswordResetDTO model)
        {
            try
            {
                if(await repo.ResetPassword(model.UserName, model.Password, model.Code))
                {
                    var login = await repo.PostLogin(model.UserName, model.Password);
                    var token = await repo.GetJWTToken(model.UserName);

                    return StatusCode(StatusCodes.Status200OK, new LoginViewModel() { User = login, Token = token });
                }
                return StatusCode(StatusCodes.Status200OK, null);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPost, Route("api/v1/logout")]
        public async Task<IActionResult> PostLogout()
        {
            try
            {
                await repo.Logout();
                return StatusCode(StatusCodes.Status200OK, true);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, false);
                throw;
            }
        }
    }
}
