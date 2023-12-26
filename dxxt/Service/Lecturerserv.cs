using dxxt.Models;
using dxxt.Repositories;

namespace dxxt.Service
{
    public class Lecturerserv : ILecturerSERV
    {
        private readonly ILecturerREPO _Lecturerrepo;

        public Lecturerserv(ILecturerREPO Lecturerrepo)
        {
            _Lecturerrepo = Lecturerrepo;
        }

        public async Task<(IEnumerable<Lecturer>, int)> GetLecturerListAsync(int pageNumber, int pageSize, string searchQuery)
        {
            return await _Lecturerrepo.GetLecturerListAsync(pageNumber, pageSize, searchQuery);
        }


        public async Task<IEnumerable<Subject>> GetSubjectsByLecturerIdAsync(int LecturerID)
        {
            return await _Lecturerrepo.GetSubjectsByLecturerIdAsync(LecturerID);
        }

        public async Task<IEnumerable<Lecturer>> GetLecturerByIdAsync(int LecturerID)
        {
            return await _Lecturerrepo.GetLecturerByIdAsync(LecturerID);
        }

        public async Task<int> AddLecturerAsync(Lecturer lecturer)
        {
            return await _Lecturerrepo.AddLecturerAsync(lecturer);
        }

        public async Task<int> UpdateLecturerAsync(Lecturer lecturer)
        {
            return await _Lecturerrepo.UpdateLecturerAsync(lecturer);
        }

        public async Task<int> DeleteLecturerAsync(int LecturerID)
        {
            return await _Lecturerrepo.DeleteLecturerAsync(LecturerID);
        }

        public async Task<bool> IsLecturerTeachingSubjectsAsync(int lecturerId)
        {
            return await _Lecturerrepo.IsLecturerTeachingSubjectsAsync(lecturerId);
        }
    }
}
