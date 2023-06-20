using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.Mapper;
using BRSS66.ApplicationCore.Models;
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

    public async Task<List<StudentResponse>> GetDataAsync(JqueryDatatableParam param)
    {
        try
        {
            var ltsStudents = await _studentRepository.GetPaging(param);
            List<StudentResponse> ltsStudentDto = ltsStudents.Select(s => s.MapToStudentResponse()).ToList();
            return ltsStudentDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

   
    public async Task<bool> CreateAsync(StudentRequest model)
    {
        try
        {
            if (model == null)
            {
                throw new Exception("Student is required");
            }

            Student student = model.MapToStudent();
            await _studentRepository.Add(student);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<StudentResponse> GetByIdAsync(int id)
    {
        try
        {
            var resStudent = await _studentRepository.Get(id);
            if (resStudent == null)
            {
                throw new Exception($"Student id:{id} not found");
            }

            return resStudent.MapToStudentResponse();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, StudentRequest model)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, StudentRequest model)
    {
        try
        {
            var resStudent = await _studentRepository.Get(id);
            if (resStudent == null)
            {
                throw new Exception($"Student id:{id} not found");
            }

            await _studentRepository.Delete(resStudent);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
}