using dxxt.Models;
using dxxt.Repositories;
namespace dxxt.Service
{
    public interface ISubjectSERV
    {
        Task<(IEnumerable<SubjectLec>, int)> GetSubjectListAsync(int pageNumber, int pageSize, string searchQuery);
        Task<IEnumerable<SubjectLec>> GetSubjectByIdAsync(int SubjectID);
        Task<int> AddSubjectAsync(Subject subject);
        Task<int> UpdateSubjectAsync(Subject subject);
        Task<int> DeleteSubjectAsync(int SubjectID);
        Task<bool> SubjectNameExistsAsync(string subjectName);
        Task<IEnumerable<Subject>> GetAvailableSubjectsForStudentAsync(int studentId);
    }

}
