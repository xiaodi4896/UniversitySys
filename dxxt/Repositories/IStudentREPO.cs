using dxxt.Models;

namespace dxxt.Repositories
{
    public interface IStudentREPO
    {
        public Task<(IEnumerable<Student>, int)> GetStudentListAsync(int pageNumber, int pageSize, string searchQuery);
        public Task<IEnumerable<Student>> GetStudentByIdAsync(int StudentID);
        public Task<int> AddStudentAsync(Student student);
        public Task<int> UpdateStudentAsync(Student student);
        public Task<int> DeleteStudentAsync(int StudentID);
        public Task<bool> CanEnrollInMoreSubjects(int studentId);
        public Task<bool> StudentEmailExistsAsync(string email);
        public Task<StudentDetailsViewModel> GetStudentDetailsAsync(int StudentID);
    }

}
