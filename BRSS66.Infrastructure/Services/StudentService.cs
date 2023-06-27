using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.Mapper;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.Infrastructure.Services;

public class StudentService : IStudentServices
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<Student>> Get()
    {
        return await _studentRepository.Get();
    }

    public async Task<PagedResponse<StudentResponse>> GetDataAsync(DataTablesRequest param)
    {
        var ltsStudents = await _studentRepository.GetPaging(param);
        //List<StudentResponse> ltsStudentDto = ltsStudents.Item1.Select(s => s.MapToStudentResponse()).ToList();
        //IQueryable<StudentResponse> lts = ltsStudents.Item1.AsQueryable().MapListStudentDto();
        return ltsStudents;
    }


    public async Task<bool> CreateAsync(StudentRequest model)
    {
        if (model == null)
        {
            throw new Exception("Student is required");
        }

        Student student = model.MapToStudent();
        await _studentRepository.Add(student);
        return true;
    }

    public async Task<StudentResponse> GetByIdAsync(int id)
    {
        var resStudent = await _studentRepository.Get(id);
        if (resStudent == null)
        {
            throw new Exception($"Student id:{id} not found");
        }

        return resStudent.MapToStudentResponse();
    }

    public async Task<bool> UpdateAsync(int id, StudentRequest model)
    {
        if (model == null)
        {
            throw new Exception("Student is required");
        }

        var resStudent = await _studentRepository.Get(id);
        if (resStudent == null)
        {
            throw new Exception($"Student id:{id} not found");
        }

        await _studentRepository.Update(model.MapToStudent());
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var resStudent = await _studentRepository.Get(id);
        if (resStudent == null)
        {
            throw new Exception($"Student id:{id} not found");
        }

        await _studentRepository.Delete(resStudent);
        return true;
    }
}