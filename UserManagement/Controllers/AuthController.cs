using Microsoft.AspNetCore.Mvc;
using UserManagement.Services;

namespace UserManagement.Controllers;
public class AuthController : Controller
{
    private readonly IUserService userService;

    public AuthController(IUserService userService)
    {
        this.userService = userService;
    }

    // NOTE:
    // Displays login page.
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        // IMPORTANT:
        // Login logic is delegated to service layer.
        // Blocked users are not allowed to login.
        var user = await userService.LoginAsync(email, password);

        if (user == null)
        {
            // NOTE:
            // Generic error message (no details for security reasons).
            ViewBag.Error = "Invalid credentials or blocked";
            return View();
        }

        // IMPORTANT:
        // Simple session-based authentication.
        HttpContext.Session.SetString("UserId", user.Id.ToString());
        return RedirectToAction("Index", "Users");
    }

    // NOTE:
    // Displays registration page.
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string name, string email, string password)
    {
        try
        {
            // IMPORTANT:
            // User is registered as Unverified (task requirement).
            await userService.RegisterAsync(name, email, password);
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            // NOTE:
            // Exception message comes from service (e.g. duplicate email).
            ViewBag.Error = ex.Message;
            return View();
        }
    }

    public IActionResult Logout()
    {
        // NOTE:
        // Clears session and logs user out.
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}