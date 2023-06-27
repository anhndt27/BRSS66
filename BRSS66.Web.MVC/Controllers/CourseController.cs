using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BRSS66.Web.MVC.Controllers;

[Authorize]
public class CourseController : Controller
{
    private readonly ICourseServices _courseServices;
    private readonly ILogger<CourseController> _logger;

    
    public CourseController(ICourseServices courseServices, ILogger<CourseController> logger)
    {
        _courseServices = courseServices;
        _logger = logger;
        
    }

    [Route("/Course/Index")]
    public IActionResult Index(DataTablesRequest param)
    {
        return View();
    }
    
    [HttpPost]
    [Route("/Course/GetData/")]
    public async Task<IActionResult> GetData(DataTablesRequest param)
    {
        try
        {
            var course = await _courseServices.Get(param);
            var returnObject = new
            {
                draw = param.Draw, recordsFiltered = course.TotalItems, recordsTotal = course.TotalItems,
                data = course.Items
            };
            return Ok(returnObject);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing GetData in StudentController");
            throw;
        }
    }
    
    [Route("/Course/Detail/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        try
        {
            var res = await _courseServices.GetByIdAsync(id);
            return View(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Details in CourseController");
            throw;
        }
    }

    // GET: CourseController/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CourseController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseRequest entity)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _courseServices.CreateAsync(entity);
            }
            else ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Create in StudentController");
            throw;
        }

        return View(entity);
    }

    // GET: CourseController/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var res = await _courseServices.GetByIdAsync(id);
        return View(res);
    }

    // POST: CourseController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseRequest entity)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (await _courseServices.UpdateAsync(id, entity))
                {
                    ViewBag.Alert = "Update Ok!";
                }
                else ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Create in StudentController");
            throw;
        }

        return View();
    }

    // GET: CourseController/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _courseServices.GetByIdAsync(id);
        return View();
    }

    // POST: CourseController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CourseRequest model)
    {
        try
        {
            if (await _courseServices.DeleteAsync(id))
            {
                return RedirectToAction(nameof(Index), new { deleteFlag = true });
            }

            ViewBag.Alert = "Chị ong Nâu nâu nâu nâu";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Create in StudentController");
            throw;
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddUseModal(EnrollmentRequest model)
    {
        try
        {
            if (await _courseServices.AddStudentToCourse(model.CourseId,
                    model.StudentId))
            {
                ViewBag.Alert = "Add new student to course!";
            }
            else ViewBag.Alert = "Unknown error";
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes. Try again.");
            ViewBag.Alerts = "lane Catch fix bug now";
        }

        return RedirectToAction("Index", "Course");
    }
    
    [Route("/Course/Select2Student/")]
    public IActionResult Select2Student(int courseId)
    {
        string id = HttpContext.Request.Query["id"]!;
        ViewBag.Id = id;
        return View();
    }
    
    [Route("/Course/Search/")]
    public async Task<IActionResult> Search(string? term)
    {
        if (term == null)
        {
            return new JsonResult(await _courseServices.GetAll());
        }
        return new JsonResult(await _courseServices.Search(term));
    }

    [HttpPost]
    public async Task<IActionResult> AddStudentSelect2(int[] ids, int courseId)
    {
        try
        {
            foreach (var studentId in ids)
            {
                if (await _courseServices.AddStudentToCourse(courseId, studentId))
                {
                    ViewBag.Alert =
                        $"Add new student has id = {studentId} to course!";
                }
                else ViewBag.Alert = $"{studentId} can't add to Course";
            }
        }
        catch (DbUpdateException  e)  when (e.InnerException is SqlException innerException)
        {
            _logger.LogError(e, "An error occurred while executing Create in StudentController");
            if (innerException.Number == 2627) // Lỗi violation of primary key constraint
            {
                ModelState.AddModelError(string.Empty, "Mã đăng ký đã tồn tại.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi lưu thông tin đăng ký.");
            }

            return RedirectToAction("Select2Student", "Course");
        }

        return RedirectToAction("Index", "Course");
    }
}