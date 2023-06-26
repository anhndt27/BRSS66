using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    // GET
    public IActionResult Index(DataTablesRequest param)
    {
        return View();
    }
    
    [HttpPost]
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
            else ViewBag.Alert = "Chị ong Nâu nâu nâu nâu";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Create in StudentController");
            throw;
        }

        return View(model);
    }
    
    public async Task<IActionResult> AddStudent([FromRoute] int id)
    {
        var temp = await  _courseServices.GetByIdAsync(id);
        ViewBag.getTitleCourse = temp.Title!; 
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddStudent([FromRoute] int id, string studentId)
    {
        try
        {
            int[] intArray = studentId.Split(new char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            foreach (var stuId in intArray)
            {
                if (await _courseServices.AddStudentToCourse(id, stuId))
                {
                    ViewBag.Alert =
                        $"Add new student has id = {studentId} to course!";
                }
                else ViewBag.Alert = $"{studentId} can't add to Course";
            }
        }
        catch(Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Create in StudentController");
            throw;
        }

        return View();
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

    public async Task<IActionResult> AddStudentSelect2([FromBody] EnrollmentRequest request)
    {
        try
        {
            foreach (var studentId in request.StudentIdAray)
            {
                var enrollment = new EnrollmentRequest()
                {
                    CourseId = request.CourseId,
                    StudentId = studentId
                };
                await _courseServices.AddStudentToCourse(enrollment.CourseId, enrollment.StudentId);
            }
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Create in StudentController");
            throw;
        }
    }
}