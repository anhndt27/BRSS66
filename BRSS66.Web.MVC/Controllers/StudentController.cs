using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BRSS66.Web.MVC.Controllers;

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
                draw = param.Draw, recordsFiltered = students.Count(), recordsTotal = students.Count(), data = students
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
}