using API.MyContacts.Filters;
using API.MyContacts.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Linq;
using System;

namespace API.MyContacts.Controllers
{
    [RoutePrefix("api/v1/account")]
    public class AccountController : ApiController
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [RequestValidationFilter()]
        [Route("")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Hometown = model.Hometown };
            var result = await UserManager.CreateAsync(user, model.Password);
            var status = new StatusModel() { Id = model.Email, ResultCode = "Success" };
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return Created(this.Request.RequestUri, status);
            }
            else
            {
                status.ResultCode = "ME002";
                status.Reason = string.Join(Environment.NewLine, result.Errors);
                return Ok(status);
            }

        }
    }
}
