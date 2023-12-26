using dxxt.Data;
using dxxt.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace dxxt.Repositories
{
    public class Lecturerrepo : ILecturerREPO
    {
        private readonly dxxtContext _dxxtContext;

        public Lecturerrepo(dxxtContext dbContext)
        {
            _dxxtContext = dbContext;
        }

        public async Task<(IEnumerable<Lecturer>, int)> GetLecturerListAsync(int pageNumber, int pageSize, string searchQuery)
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

            var result = await _dxxtContext.Lecturer
                            .FromSqlRaw("EXEC GetLecturerListPagedAndSearched @PageNumber, @PageSize, @SearchQuery, @TotalCount OUT", parameters)
                            .ToListAsync();

            int totalCount = (int)totalCountParam.Value;
            return (result, totalCount);
        }


        public async Task<IEnumerable<Subject>> GetSubjectsByLecturerIdAsync(int LecturerID)
        {
            var param = new SqlParameter("@LecturerID", LecturerID);

            return await _dxxtContext.Subject
                .FromSqlRaw("EXEC dbo.GetSubjectsByLecturerID @LecturerID", param)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lecturer>> GetLecturerByIdAsync(int LecturerID)
        {
            var param = new SqlParameter("@LecturerID", LecturerID);

            var lecturerDetails = await Task.Run(() => _dxxtContext.Lecturer
                            .FromSqlRaw(@"EXEC GetLecturerByID @LecturerID", param).ToListAsync());

            return lecturerDetails;
        }

        public async Task<int> AddLecturerAsync(Lecturer lecturer)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@LecturerName", lecturer.LecturerName));
            parameter.Add(new SqlParameter("@IsDelete", lecturer.IsDelete));

            var result = await Task.Run(() => _dxxtContext.Database
           .ExecuteSqlRawAsync(@"EXEC AddNewLecturer @LecturerName, @IsDelete", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateLecturerAsync(Lecturer lecturer)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@LecturerID", lecturer.LecturerID));
            parameter.Add(new SqlParameter("@LecturerName", lecturer.LecturerName));
            parameter.Add(new SqlParameter("@IsDelete", lecturer.IsDelete));

            var result = await Task.Run(() => _dxxtContext.Database
            .ExecuteSqlRawAsync(@"EXEC UpdateLecturer @LecturerID, @LecturerName, @IsDelete", parameter.ToArray()));
            return result;
        }
        public async Task<int> DeleteLecturerAsync(int LecturerID)
        {
            var parameter = new SqlParameter("@LecturerID", LecturerID);
            return await _dxxtContext.Database
                     .ExecuteSqlRawAsync("EXEC DeleteLecturerByID @LecturerID", parameter);
        }
        public async Task<bool> IsLecturerTeachingSubjectsAsync(int lecturerId)
        {
            var lecturerIdParam = new SqlParameter("@LecturerID", lecturerId);
            var isTeachingParam = new SqlParameter
            {
                ParameterName = "@IsTeaching",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };

            await _dxxtContext.Database.ExecuteSqlRawAsync("EXEC CheckLecturerTeachingSubjects @LecturerID, @IsTeaching OUT",
                                                           lecturerIdParam, isTeachingParam);

            return (bool)isTeachingParam.Value;
        }

    }
}
