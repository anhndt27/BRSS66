using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BRSS66.Web.MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authentication;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthenticationService authentication, ILogger<AccountController> logger)
        {
            _authentication = authentication;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authentication.Register(model);

                    if (result)
                    {
                        return RedirectToAction("index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while executing Register in AuthorController");
                throw;
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authentication.Login(model);

                    if (result)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while executing Login in AuthorController");
                throw;
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                if (await _authentication.Logout())
                {
                    return RedirectToAction("Login", "Account");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while executing Logout in AuthorController");
                throw;
            }
        }
    }
}