using System.Data.SqlTypes;
using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Models;
using BRSS66.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BRSS66.Infrastructure.Repositories;

public class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
    private AppDbContext _context;

    public StudentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    

    public Task<List<Student>> GetPaging(JqueryDatatableParam param)
    {
        var data = _context.Students!.AsNoTracking().AsQueryable();
            // await _context.Students.ToListAsync();
            //(from student in _context.Students select student);
        if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
        {
            switch (param.SortColumn)
            {
                case "Name":
                    switch (param.SortColumnDirection)
                    {
                        case "ASC":
                            data = data.OrderBy(s => s.Name);
                            break;
                        case "DES":
                            data = data.OrderByDescending(s => s.Name);
                            break;
                        default:
                            data = data.OrderBy(s => s.Name);
                            break;
                    }
                    break;
                case "Code":
                    switch (param.SortColumnDirection)
                    {
                        case "ASC":
                            data = data.OrderBy(s => s.Code);
                            break;
                        case "DES":
                            data = data.OrderByDescending(s => s.Code);
                            break;
                        default:
                            data = data.OrderBy(s => s.Code);
                            break;
                    }
                    break;
                default:
                    data = data.OrderBy(x => x.Name);
                    break;
            }
        }

        if (!string.IsNullOrEmpty(param.SearchValue))
        {
            data = data.Where(x => x.Name!.ToLower().Contains(param.SearchValue.ToLower())
                                   || x.Code!.ToLower().Contains(param.SearchValue.ToLower()));
        }
        var empList = data.Skip(param.Skip).Take(param.PageSize).ToList();
        return  Task.FromResult(empList);
    }
}