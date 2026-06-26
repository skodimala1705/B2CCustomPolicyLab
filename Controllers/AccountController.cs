using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace B2CHelloWorld.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            "B2CSignIn");
    }

    public IActionResult Logout()
    {
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            "B2CSignInCookie",
            "B2CSignIn");
    }
}