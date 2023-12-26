using dxxt.Models;
using dxxt.Repositories;

namespace dxxt.Service
{
    public class Subjectserv : ISubjectSERV
    {
        private readonly ISubjectREPO _Subjectrepo;

        public Subjectserv(ISubjectREPO Subjectrepo)
        {
            _Subjectrepo = Subjectrepo;
        }

        public async Task<(IEnumerable<SubjectLec>, int)> GetSubjectListAsync(int pageNumber, int pageSize, string searchQuery)
        {
            return await _Subjectrepo.GetSubjectListAsync(pageNumber, pageSize, searchQuery);
        }

        public async Task<IEnumerable<SubjectLec>> GetSubjectByIdAsync(int SubjectID)
        {
            return await _Subjectrepo.GetSubjectByIdAsync(SubjectID);
        }

        public async Task<int> AddSubjectAsync(Subject subject)
        {
            return await _Subjectrepo.AddSubjectAsync(subject);
        }

        public async Task<int> UpdateSubjectAsync(Subject subject)
        {
            return await _Subjectrepo.UpdateSubjectAsync(subject);
        }

        public async Task<int> DeleteSubjectAsync(int SubjectID)
        {
            return await _Subjectrepo.DeleteSubjectAsync(SubjectID);
        }
        public async Task<bool> SubjectNameExistsAsync(string subjectName)
        {
            return await _Subjectrepo.SubjectNameExistsAsync(subjectName);
        }
        public async Task<IEnumerable<Subject>> GetAvailableSubjectsForStudentAsync(int studentId)
        {
            return await _Subjectrepo.GetAvailableSubjectsForStudentAsync(studentId);
        }

    }
}
