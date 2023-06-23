using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BRSS66.Web.MVC.Controllers;

[Authorize]
public class StudentController : Controller
{
    private readonly IStudentServices _studentServices;
    private readonly ILogger<StudentController> _logger;

    public StudentController(IStudentServices studentServices, ILogger<StudentController> logger)
    {
        _studentServices = studentServices;
        _logger = logger;
    }

    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetData(DataTablesRequest param)
    {
        try
        {
            var students = await _studentServices.GetDataAsync(param);
            var returnObject = new
            {
                draw = param.Draw, recordsFiltered = students.TotalItems, recordsTotal = students.TotalItems,
                data = students.Items
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
            var res = await _studentServices.GetByIdAsync(id);
            return View(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing GetDetails in StudentController");
            throw;
        }
    }

    public ActionResult Create()
    {
        return View();
    }

    // POST: UserManagerController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentRequest entity)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (await _studentServices.CreateAsync(entity))
                {
                    ViewBag.Alert = "Kià chú là chú ếch con có 2 là 2 mắt tròn";
                }
            }
            else ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while executing Create in StudentController");
            throw;
        }

        return View(entity);
    }

    public async Task<ActionResult> Edit(int id)
    {
        var res = await _studentServices.GetByIdAsync(id);
        return View(res);
    }

    // POST: UserManagerController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, StudentRequest entity)
    {
        try
        {
            if (await _studentServices.UpdateAsync(id, entity))
            {
                ViewBag.Alert = "AlertsHelper.ShowAlert(Alerts.Success, \"Update Ok!\")!";
            }
            else ViewBag.Alert = "AlertsHelper.ShowAlert(Alerts.Danger, \"Unknown error\")!";
            //return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Edit in StudentController");
            throw;
        }

        return View();
    }

// GET: UserManagerController/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        var res = await _studentServices.GetByIdAsync(id);
        return View(res);
    }

// POST: UserManagerController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id, bool isDelete)
    {
        try
        {
            if (await _studentServices.DeleteAsync(id))
            {
                return RedirectToAction(nameof(Index), new { deleteFlag = true });
            }

            ViewBag.Alert = "Delete successfully!";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing Delete in StudentController");
            throw;
        }

        return View();
    }
}