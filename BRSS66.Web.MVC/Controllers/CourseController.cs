using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BRSS66.Web.MVC.Controllers;

public class CourseController : Controller
{

    public readonly ICourseServices _courseServices;

    public CourseController(ICourseServices courseServices)
    {
        _courseServices = courseServices;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var ltscourse = await _courseServices.Get();
        return View(ltscourse);
    }
}