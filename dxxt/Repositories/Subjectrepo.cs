using dxxt.Data;
using dxxt.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace dxxt.Repositories
{
    public class Subjectrepo : ISubjectREPO
    {
        private readonly dxxtContext _dxxtContext;

        public Subjectrepo(dxxtContext dbContext)
        {
            _dxxtContext = dbContext;
        }

        public async Task<(IEnumerable<SubjectLec>, int)> GetSubjectListAsync(int pageNumber, int pageSize, string searchQuery)
        {
            var totalCountParam = new SqlParameter
            {
                ParameterName = "@TotalCount",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var parameters = new[]
            {
            new SqlParameter("@PageNumber", pageNumber),
            new SqlParameter("@PageSize", pageSize),
            new SqlParameter("@SearchQuery", string.IsNullOrEmpty(searchQuery) ? DBNull.Value : (object)searchQuery),
            totalCountParam
            };

            var result = await _dxxtContext.SubjectLec
                        .FromSqlRaw("EXEC GetSubjectListPagedAndSearched @PageNumber, @PageSize, @SearchQuery, @TotalCount OUT", parameters)
                        .ToListAsync();

            int totalCount = (int)totalCountParam.Value;
            return (result, totalCount);
        }

        public async Task<IEnumerable<SubjectLec>> GetSubjectByIdAsync(int SubjectID)
        {
            var param = new SqlParameter("@SubjectID", SubjectID);

            var subjectDetails = await _dxxtContext.SubjectLec
                                 .FromSqlRaw("EXEC GetSubjectByID @SubjectID", param)
                                 .ToListAsync();

            return subjectDetails;
        }

        public async Task<int> AddSubjectAsync(Subject subject)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@SubjectName", subject.SubjectName));
            parameter.Add(new SqlParameter("@LecturerID", subject.LecturerID));
            parameter.Add(new SqlParameter("@IsDelete", subject.IsDelete));

            var result = await Task.Run(() => _dxxtContext.Database
           .ExecuteSqlRawAsync(@"EXEC AddNewSubject @SubjectName, @LecturerID, @IsDelete", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateSubjectAsync(Subject subject)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@SubjectID", subject.SubjectID));
            parameter.Add(new SqlParameter("@SubjectName", subject.SubjectName));
            parameter.Add(new SqlParameter("@LecturerID", subject.LecturerID));
            parameter.Add(new SqlParameter("@IsDelete", subject.IsDelete));

            var result = await Task.Run(() => _dxxtContext.Database
            .ExecuteSqlRawAsync(@"EXEC UpdateSubject @SubjectID, @SubjectName, @LecturerID, @IsDelete", parameter.ToArray()));
            return result;
        }
        public async Task<int> DeleteSubjectAsync(int SubjectID)
        {
            return await Task.Run(() => _dxxtContext.Database.ExecuteSqlInterpolatedAsync($"DeleteSubjectByID {SubjectID}"));
        }
        public async Task<bool> SubjectNameExistsAsync(string subjectName)
        {
            var subjectNameParam = new SqlParameter("@SubjectName", subjectName);
            var existsParam = new SqlParameter
            {
                ParameterName = "@Exists",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };

            await _dxxtContext.Database.ExecuteSqlRawAsync("EXEC CheckSubjectNameExists @SubjectName, @Exists OUT",
                                                           subjectNameParam, existsParam);

            return (bool)existsParam.Value;
        }
        public async Task<IEnumerable<Subject>> GetAvailableSubjectsForStudentAsync(int studentId)
        {
            var studentIdParam = new SqlParameter("@StudentID", studentId);
            return await _dxxtContext.Subject
                        .FromSqlRaw("EXEC GetAvailableSubjectsForStudent @StudentID", studentIdParam)
                        .ToListAsync();
        }

    }
}
