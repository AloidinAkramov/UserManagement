using Microsoft.AspNetCore.Mvc;
using UserManagement.Services;

namespace UserManagement.Controllers;
public class UsersController : Controller
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // IMPORTANT:
        // Only logged-in users are allowed to see users list.
        // Authentication is handled via session.
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        var users = await userService.GetAllAsync();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Block(Guid[] ids)
    {
        // NOTE:
        // Authorization check for POST actions.
        if (!IsLoggedIn())
            return RedirectToAction("Login", "Auth");

        // NOTE:
        // If no users are selected, simply return to list.
        if (ids == null || ids.Length == 0)
            return RedirectToAction("Index");

        // IMPORTANT:
        // Bulk block operation.
        await userService.BlockAsync(ids.ToList());

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Unblock(Guid[] ids)
    {
        // NOTE:
        // Only authenticated users can perform this action.
        if (!IsLoggedIn())
            return RedirectToAction("Login", "Auth");

        if (ids == null || ids.Length == 0)
            return RedirectToAction("Index");

        // IMPORTANT:
        // Bulk unblock operation.
        await userService.UnblockAsync(ids.ToList());

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid[] ids)
    {
        // IMPORTANT:
        // Physical delete of users (not soft delete),
        // required by task description.
        if (!IsLoggedIn())
            return RedirectToAction("Login", "Auth");

        if (ids == null || ids.Length == 0)
            return RedirectToAction("Index");

        await userService.DeleteAsync(ids.ToList());

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUnverified()
    {
        // IMPORTANT:
        // Deletes ALL users with Unverified status.
        // This is a toolbar action.
        if (!IsLoggedIn())
            return RedirectToAction("Login", "Auth");

        await userService.DeleteUnverifiedAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Confirm(Guid id)
    {
        // NOTE:
        // Fake email confirmation (button-based),
        // allowed by task requirements.
        if (!IsLoggedIn())
            return RedirectToAction("Login", "Auth");

        if (id == Guid.Empty)
            return RedirectToAction("Index");

        await userService.ConfirmAsync(id);

        return RedirectToAction("Index");
    }

    private bool IsLoggedIn()
    {
        // NOTE:
        // Simple session-based authentication helper.
        return HttpContext.Session.GetString("UserId") != null;
    }
}