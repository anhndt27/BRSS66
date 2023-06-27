using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Mapper;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;
using BRSS66.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace BRSS66.Infrastructure.Repositories;

public class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }


    public Task<PagedResponse<StudentResponse>> GetPaging(DataTablesRequest param)
    {
        var data = _context.Students!.AsNoTracking().AsQueryable();
        // await _context.Students.ToListAsync();
        //(from student in _context.Students select student);

        int start = param.Start;
        int length = param.Length;
        string searchValue = param.Search?.Value!;
        bool searchRegex = param.Search?.Regex ?? false;
        List<Column> columns = param.Columns ?? new List<Column>();
        List<Order> orders = param.Order ?? new List<Order>();
        int sortColumn = orders.Count > 0 ? orders[0].Column : 0;

        if (!string.IsNullOrEmpty(searchValue))
        {
            data = data.Where(x => x.Name!.ToLower().Contains(searchValue.ToLower())
                                   || x.Code!.ToLower().Contains(searchValue.ToLower()));
        }

        if (sortColumn >= 0 && sortColumn < columns.Count)
        {
            var column = columns[sortColumn];
            var propertyName = column.Data ?? column.Name;
            if (!string.IsNullOrEmpty(propertyName))
            {
                var direction = orders.Count > 0 ? orders[0].Dir!.ToLower() : "asc";
                switch (propertyName)
                {
                    case "name":
                        switch (direction)
                        {
                            case "asc":
                                data = data.OrderBy(s => s.Name);
                                break;
                            case "desc":
                                data = data.OrderByDescending(s => s.Name);
                                break;
                            default:
                                data = data.OrderBy(s => s.Name);
                                break;
                        }
                        break;
                    case "code":
                        switch (direction)
                        {
                            case "asc":
                                data = data.OrderBy(s => s.Code);
                                break;
                            case "desc":
                                data = data.OrderByDescending(s => s.Code);
                                break;
                            default:
                                data = data.OrderBy(s => s.Code);
                                break;
                        }
                        break;
                    default:
                        data = data.OrderBy(s => s.Name);
                        break;
                }
                //default:
            }
        }

        int recordsTotal = data.Count();
        var empList = data.Skip(start).Take(length).MapListStudentDto().ToList();

        return Task.FromResult(new PagedResponse<StudentResponse>()
        {
            TotalItems = recordsTotal,
            Items = empList
        });
    }

    public async Task<List<Student>> Search(string term)
    {
        return await _context.Students!.Where(s => s.Name!.Contains(term)).ToListAsync();
    }

    public async Task<List<Student>> GetAll()
    {
        return await _context.Students!.ToListAsync();
    }
}