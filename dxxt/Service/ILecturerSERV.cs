using dxxt.Models;
using dxxt.Repositories;
namespace dxxt.Service
   
{
    public interface ILecturerSERV
    {
        Task<(IEnumerable<Lecturer>, int)> GetLecturerListAsync(int pageNumber, int pageSize, string searchQuery);
        Task<IEnumerable<Lecturer>> GetLecturerByIdAsync(int LecturerID);
        Task<IEnumerable<Subject>> GetSubjectsByLecturerIdAsync(int LecturerID);
        Task<int> AddLecturerAsync(Lecturer lecturer);
        Task<int> UpdateLecturerAsync(Lecturer lecturer);
        Task<int> DeleteLecturerAsync(int LecturerID);
        Task<bool> IsLecturerTeachingSubjectsAsync(int lecturerId);
    }

}
