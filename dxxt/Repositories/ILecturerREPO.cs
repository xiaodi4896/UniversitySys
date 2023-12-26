using dxxt.Models;

namespace dxxt.Repositories
{
    public interface ILecturerREPO
    {
        public Task<(IEnumerable<Lecturer>, int)> GetLecturerListAsync(int pageNumber, int pageSize, string searchQuery);
        public Task<IEnumerable<Lecturer>> GetLecturerByIdAsync(int LecturerID);
        public Task<IEnumerable<Subject>> GetSubjectsByLecturerIdAsync(int LecturerID);
        public Task<int> AddLecturerAsync(Lecturer lecturer);
        public Task<int> UpdateLecturerAsync(Lecturer lecturer);
        public Task<int> DeleteLecturerAsync(int LecturerID);
        public Task<bool> IsLecturerTeachingSubjectsAsync(int lecturerId);
    }
    
}
