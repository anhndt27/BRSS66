using BRSS66.ApplicationCore.Business;
using BRSS66.ApplicationCore.Enum;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.Models;
using BRSS66.ApplicationCore.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BRSS66.Web.MVC.Controllers;

[Authorize]
public class StudentController : Controller
{
    public readonly IStudentServices _studentServices;

    public StudentController(IStudentServices studentServices)
    {
        _studentServices = studentServices;
    }
    public ActionResult Index()
    {
        return View();
    }
    // 
    [HttpPost]
    public async Task<IActionResult> GetData()
    {
        try
        {
            var param = new JqueryDatatableParam()
            {
                Draw = Request.Form["draw"],
                SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"],
                SortColumnDirection = Request.Form["order[0][dir]"],
                SearchValue = Request.Form["search[value]"],
                PageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0"),
                Skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0")
            };
            
            var students = await _studentServices.GetDataAsync(param);
            var returnObject = new
            {
                draw = param.Draw, recordsFiltered = students.Item2, recordsTotal = students.Item2, data = students.Item1
            };
            return Ok(returnObject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var res = await _studentServices.GetByIdAsync(id);
            return View(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            if (await _studentServices.CreateAsync(entity))
            {
                ViewBag.Alert = AlertsHelper.ShowAlert(Alerts.Success, "Create Ok!")!;
            }
            else ViewBag.Alert = AlertsHelper.ShowAlert(Alerts.Danger, "Unknown error")!;
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("",
                $"Unable to save changes. Student Code is already exist!");
            Console.WriteLine(ex);
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
    public async Task<ActionResult> Edit(int id,StudentRequest entity)
    {
        try
        {
            if (await _studentServices.UpdateAsync(id,entity))
            {
                ViewBag.Alert = AlertsHelper.ShowAlert(Alerts.Success, "Update Ok!")!;
            }
            else ViewBag.Alert = AlertsHelper.ShowAlert(Alerts.Danger, "Unknown error")!;
            //return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            ModelState.AddModelError("",
                $"Unable to save changes. Student Code is already exist!");
            Console.WriteLine(e);
        }

        return View();
    }

// GET: UserManagerController/Delete/5
    public ActionResult Delete()
    {
        return View();
    }

// POST: UserManagerController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id,StudentRequest entity)
    {
        try
        {
            if (await _studentServices.DeleteAsync(id,entity))
            {
                return RedirectToAction(nameof(Index), new { deleteFlag = true });
            }
            else ViewBag.Alert = AlertsHelper.ShowAlert(Alerts.Danger, "Remove faile")!;
        }
        catch (InvalidDataException)
        {
            return View();
        }

        return View();
    }
}