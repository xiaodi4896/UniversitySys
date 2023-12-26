using dxxt.Models;
using dxxt.Repositories;

namespace dxxt.Service
{
    public class Studentserv : IStudentSERV
    {
        private readonly IStudentREPO _Studentrepo;

        public Studentserv(IStudentREPO Studentrepo)
        {
            _Studentrepo = Studentrepo;
        }

        public async Task<(IEnumerable<Student>, int)> GetStudentListAsync(int pageNumber, int pageSize, string searchQuery)
        {
            return await _Studentrepo.GetStudentListAsync(pageNumber, pageSize, searchQuery);
        }

        public async Task<IEnumerable<Student>> GetStudentByIdAsync(int StudentID)
        {
            return await _Studentrepo.GetStudentByIdAsync(StudentID);
        }

        public async Task<int> AddStudentAsync(Student student)
        {
            return await _Studentrepo.AddStudentAsync(student);
        }

        public async Task<int> UpdateStudentAsync(Student student)
        {
            return await _Studentrepo.UpdateStudentAsync(student);
        }

        public async Task<int> DeleteStudentAsync(int StudentID)
        {
            return await _Studentrepo.DeleteStudentAsync(StudentID);
        }
        public async Task<bool> CanStudentEnrollInMoreSubjects(int studentId)
        {
            return await _Studentrepo.CanEnrollInMoreSubjects(studentId);
        }
        public async Task<bool> StudentEmailExistsAsync(string email)
        {
            return await _Studentrepo.StudentEmailExistsAsync(email);
        }
        public async Task<StudentDetailsViewModel> GetStudentDetailsAsync(int StudentID)
        {
            return await _Studentrepo.GetStudentDetailsAsync(StudentID);
        }
    }
}
