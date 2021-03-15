using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class IdentityRepository
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signManager;
        public IdentityRepository(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signManager)
        {
            userManager = _userManager;
            signManager = _signManager;
        }
        public async Task<string> GetJWTToken(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            var principle = await signManager.CreateUserPrincipalAsync(user);
            var token = new JwtSecurityTokenHandler().CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("super_duper_secret_key")),SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddDays(1),
                Subject = (ClaimsIdentity)principle.Identity,
                 Audience = "http://localhost:4200",
                 Issuer = "https://localhost:5000"
            });
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> PostRegistration(string username, string password)
        {
            var find = await userManager.FindByNameAsync(username);
            if(find == null)
            {
                var identity = new IdentityUser() { UserName = username };

                var create = await userManager.CreateAsync(identity, password);
                return create.Succeeded;
            }
            return true;
        }
        public async Task<IdentityUser> PostLogin(string username, string password)
        {
            var find = await userManager.FindByNameAsync(username);
            if(find == null)
            {
                return null;
            }
            var can = await signManager.CanSignInAsync(find);
            if (!can)
            {
                return null;
            }
            var signin = await signManager.PasswordSignInAsync(find, password, false, false);
            if (signin.Succeeded)
            {
                return find;
            }
            return null;            
        }
        public async Task<IdentityUser> GetUserByToken(ClaimsPrincipal principle)
        {
            return await userManager.GetUserAsync(principle);
        }
        public async Task<string> GetPasswordResetToken(string username)
        {
            var find = await userManager.FindByNameAsync(username);
            if(find == null)
            {
                return string.Empty;
            }
            return await userManager.GeneratePasswordResetTokenAsync(find);
        }
        public async Task<bool> ResetPassword(string username, string password, string token)
        {
            var find = await userManager.FindByNameAsync(username);
            if(find == null)
            {
                return false;
            }
            var reset = await userManager.ResetPasswordAsync(find, token, password);
            return reset.Succeeded;            
        }
        public async Task Logout()
        {
            await signManager.SignOutAsync();
        }
    }
}
