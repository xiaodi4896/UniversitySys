using dxxt.Models;
using dxxt.Repositories;
namespace dxxt.Service
{
    public interface IStudentSERV
    {
        Task<(IEnumerable<Student>, int)> GetStudentListAsync(int pageNumber, int pageSize, string searchQuery);
        Task<IEnumerable<Student>> GetStudentByIdAsync(int StudentID);
        Task<int> AddStudentAsync(Student student);
        Task<int> UpdateStudentAsync(Student student);
        Task<int> DeleteStudentAsync(int StudentID);
        Task<bool> CanStudentEnrollInMoreSubjects(int studentId);
        Task<bool> StudentEmailExistsAsync(string email);
        Task<StudentDetailsViewModel> GetStudentDetailsAsync(int StudentID);
    }

}
