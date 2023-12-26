using dxxt.Data;
using dxxt.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace dxxt.Repositories
{
    public class Scorerepo : IScoreREPO
    {
        private readonly dxxtContext _dxxtContext;

        public Scorerepo(dxxtContext dbContext)
        {
            _dxxtContext = dbContext;
        }

        public async Task<(IEnumerable<ScoreStuSub>, int)> GetScoreListAsync(int pageNumber, int pageSize, string searchQuery)
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

            var result = await _dxxtContext.ScoreStuSub
                        .FromSqlRaw("EXEC GetScoreListPagedAndSearched @PageNumber, @PageSize, @SearchQuery, @TotalCount OUT", parameters)
                        .ToListAsync();

            int totalCount = (int)totalCountParam.Value;
            return (result, totalCount);
        }

        public async Task<IEnumerable<ScoreStuSub>> GetScoreByIdAsync(int ScoreID)
        {
            var param = new SqlParameter("@ScoreID", ScoreID);
            return await _dxxtContext.ScoreStuSub
                .FromSqlRaw("EXEC GetScoreByID @ScoreID", param)
                .ToListAsync();
        }

        public async Task<int> AddScoreAsync(Score score)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@StudentID", score.StudentID));
            parameter.Add(new SqlParameter("@SubjectID", score.SubjectID));
            parameter.Add(new SqlParameter("@Marks", score.Marks ?? (object)DBNull.Value));
            parameter.Add(new SqlParameter("@IsDelete", score.IsDelete));

            var result = await Task.Run(() => _dxxtContext.Database
           .ExecuteSqlRawAsync(@"EXEC AddNewScore @StudentID, @SubjectID, @Marks, @IsDelete", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateScoreAsync(Score score)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ScoreID", score.ScoreID));
            parameter.Add(new SqlParameter("@StudentID", score.StudentID));
            parameter.Add(new SqlParameter("@SubjectID", score.SubjectID));
            parameter.Add(new SqlParameter("@Marks", score.Marks ?? (object)DBNull.Value));
            parameter.Add(new SqlParameter("@IsDelete", score.IsDelete));

            var result = await Task.Run(() => _dxxtContext.Database
            .ExecuteSqlRawAsync(@"EXEC UpdateScore @ScoreID, @StudentID, @SubjectID, @Marks, @IsDelete", parameter.ToArray()));
            return result;
        }
        public async Task<int> DeleteScoreAsync(int ScoreID)
        {
            var parameter = new SqlParameter("@ScoreID", ScoreID);
            return await _dxxtContext.Database
                      .ExecuteSqlRawAsync("EXEC DeleteScoreByID @ScoreID", parameter);
        }

        public async Task<bool> CheckIfScoreExists(int studentId, int subjectId, int? scoreId = null)
        {
            using (var command = _dxxtContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "CheckIfScoreExists";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StudentID", studentId));
                command.Parameters.Add(new SqlParameter("@SubjectID", subjectId));
                command.Parameters.Add(new SqlParameter("@ExcludeScoreID", scoreId ?? (object)DBNull.Value));

                await _dxxtContext.Database.OpenConnectionAsync();

                var result = await command.ExecuteScalarAsync();
                await _dxxtContext.Database.CloseConnectionAsync();

                return Convert.ToInt32(result) == 1;
            }
        }
        public async Task<List<StudentScoresViewModel>> GetGroupedScoresAsync()
        {
            var scoreList = new List<ScoreStuSub>();

            using (var command = _dxxtContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetGroupedScores";
                command.CommandType = CommandType.StoredProcedure;

                await _dxxtContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        scoreList.Add(new ScoreStuSub
                        {
                            // Populate ScoreStuSub from result
                        });
                    }
                }
                await _dxxtContext.Database.CloseConnectionAsync();
            }

            // Transform to StudentScoresViewModel
            var groupedScores = scoreList
                .GroupBy(s => new { s.StudentID, s.StudentName })
                .Select(g => new StudentScoresViewModel
                {
                    StudentID = g.Key.StudentID,
                    StudentName = g.Key.StudentName,
                    SubjectScores = g.Select(s => new SubjectScore
                    {
                        SubjectID = s.SubjectID,
                        SubjectName = s.SubjectName,
                        Marks = s.Marks
                    }).ToList()
                }).ToList();

            return groupedScores;
        }


        public async Task<int> GetFailedOrPendingSubjectCountAsync(int studentId, int? excludeScoreId = null)
        {
            using (var command = _dxxtContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetFailedOrPendingSubjectCount";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StudentID", studentId));
                command.Parameters.Add(new SqlParameter("@ExcludeScoreID", excludeScoreId ?? (object)DBNull.Value));

                await _dxxtContext.Database.OpenConnectionAsync();

                var result = await command.ExecuteScalarAsync();
                await _dxxtContext.Database.CloseConnectionAsync();

                return Convert.ToInt32(result);
            }
        }



    }
}
