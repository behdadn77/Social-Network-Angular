using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using B3hdadSocialNetwork.Areas.Identity.Data;
using B3hdadSocialNetwork.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace B3hdadSocialNetwork.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
       private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<LogoutModel> logger;
        IHttpContextAccessor httpContextAccessor;


        public AuthenticationController(SignInManager<ApplicationUser> _signInManager,
                              UserManager<ApplicationUser> _userManager,
                              RoleManager<IdentityRole> _roleManager,
                              ILogger<LogoutModel> _logger,
                              IHttpContextAccessor _httpContextAccessor
            )
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            logger = _logger;
            httpContextAccessor = _httpContextAccessor;
        }
        //-----------Get Signed in user info------//
        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return null;
            }
            return Json(new
            {
                email = user.Email,
                name = user.FirstName,
                family = user.LastName
            });
        }
        [HttpGet]
        public bool IsAuthenticated() => User.Identity.IsAuthenticated;

        //-------------Login-----------------//
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]LoginViewModel obj, string returnUrl)
        {
            returnUrl = returnUrl ?? "/"; // default is home

            var user = await userManager.FindByNameAsync(obj.Email);
            if (user != null)
            {
                if (!userManager.IsEmailConfirmedAsync(user).Result)
                {
                    return Json(new { url = "/email-confirm" });
                }
                var status = await signInManager.PasswordSignInAsync(user, obj.Password, obj.RememberMe, obj.RememberMe);
                //var s = User.Identity.IsAuthenticated;
                if (status.Succeeded)
                {
                    return Json(new { url = returnUrl });
                }
                else
                {
                    return Json("Password doesn't match the Email Address!");
                }
            }

            // User Doesn't exist
            return Json("User Doesn't exist");
            
        }

        //-------------Google Authenication---------------//
        public IActionResult GoogleLogin()
        {
            string redirectAction = Url.Action("RedirectFromGoogleLogin"); 
            var properties =
                signInManager.ConfigureExternalAuthenticationProperties("Google", redirectAction);
            return new ChallengeResult("Google", properties);
        }
        public async Task<IActionResult> RedirectFromGoogleLogin()
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var username = info.Principal
                .FindFirst(System.Security.Claims.ClaimTypes.Email).Value;
            var name = info.Principal
                .FindFirst(System.Security.Claims.ClaimTypes.GivenName).Value;
            var family = info.Principal
                .FindFirst(System.Security.Claims.ClaimTypes.Surname).Value;

            var founduser = await userManager.FindByNameAsync(username);
            if (founduser == null) //User doesn't exist must be registerd 
            {
                return RedirectToAction("SignUp", new SignUpViewModel() {
                    Email=username,
                    Name=name,
                    Family=family
                });
            }
            else
            {
                //login with google credentials
                
            }

            return RedirectToAction("Index");
        }

        //--------------SignUp------------------//
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SignUpViewModel obj) 
        {
            var role = await roleManager.FindByNameAsync("admins");
            if (role == null)
            {
                role = new IdentityRole("admins");
                await roleManager.CreateAsync(role);
            }

            role = await roleManager.FindByNameAsync("user");
            if (role == null)
            {
                role = new IdentityRole("user");
                await roleManager.CreateAsync(role);
            }                                                       //add this section to startup later
            ///////////////////////////////////////////////////////////////////////////////
            var user = await userManager.FindByNameAsync(obj.Email); 
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = obj.Email,
                    UserName = obj.Email,
                    FirstName = obj.Name,
                    LastName = obj.Family
                };

                var status = await userManager.CreateAsync(user,obj.Password);
                if (status.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                    await SendVerificationEmailAsync(user);
                    
                    return Redirect("/confirm-email");
                }
                return Json("Sothing went wrong!");
            }
            else
            {
                return Json("This Email Address already existes.");
            }
        }

        //----------Confirm Email-----------------//

        public async Task SendVerificationEmailAsync(ApplicationUser user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string body = $"<div style='border:2px solid green;border-border-radius:10px;padding:10px'>" +
                $"HI, <b>{user.FirstName + " " + user.LastName}</b><br/>"
                + "To confirm your account, Please use this token:" +
                $"<h3>{token}</h3>"
                + "<br/><b>B3hdad</b></div>";

            System.Net.Mail.MailMessage msg =
                new System.Net.Mail.MailMessage("emailaddress", user.Email);
            msg.Subject = "Confirm Email Address";
            msg.Body = body;
            msg.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();// "smtp.gmail.com", 465);
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials =
                new System.Net.NetworkCredential("user", "pass");
            client.Send(msg);

        }
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromForm]string token)
        {
            //var user = await userManager.FindByIdAsync(id);
            var user = await userManager.GetUserAsync(HttpContext.User);
            var status = await userManager.ConfirmEmailAsync(user, token);
            if (status.Succeeded)
            {
                return Redirect("/sign-in");
            }
            return Json("Error");
        }


        //----------Logout-----------------//
        [HttpGet]
        public async Task<IActionResult> SignOut([FromRoute]string returnUrl)
        {
            returnUrl = returnUrl ?? "/"; // default is home
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

    }
}