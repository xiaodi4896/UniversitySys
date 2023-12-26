using dxxt.Models;

namespace dxxt.Repositories
{
    public interface ISubjectREPO
    {
        public Task<(IEnumerable<SubjectLec>, int)> GetSubjectListAsync(int pageNumber, int pageSize, string searchQuery);
        public Task<IEnumerable<SubjectLec>> GetSubjectByIdAsync(int SubjectID);
        public Task<int> AddSubjectAsync(Subject subject);
        public Task<int> UpdateSubjectAsync(Subject subject);
        public Task<int> DeleteSubjectAsync(int SubjectID);
        public Task<bool> SubjectNameExistsAsync(string subjectName);
        public Task<IEnumerable<Subject>> GetAvailableSubjectsForStudentAsync(int studentId);
    }

}
