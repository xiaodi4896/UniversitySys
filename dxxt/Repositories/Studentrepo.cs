using dxxt.Data;
using dxxt.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace dxxt.Repositories
{
    public class Studentrepo : IStudentREPO
    {
        private readonly dxxtContext _dxxtContext;

        public Studentrepo(dxxtContext dbContext)
        {
            _dxxtContext = dbContext;
        }

        public async Task<(IEnumerable<Student>, int)> GetStudentListAsync(int pageNumber, int pageSize, string searchQuery)
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

            var result = await _dxxtContext.Student
                            .FromSqlRaw("EXEC GetStudentListPagedAndSearched @PageNumber, @PageSize, @SearchQuery, @TotalCount OUT", parameters)
                            .ToListAsync();

            int totalCount = (int)totalCountParam.Value;
            return (result, totalCount);
        }

        public async Task<IEnumerable<Student>> GetStudentByIdAsync(int StudentID)
        {
            var param = new SqlParameter("@StudentID", StudentID);

            var studentDetails = await Task.Run(() => _dxxtContext.Student
                            .FromSqlRaw(@"EXEC GetStudentByID @StudentID", param).ToListAsync());

            return studentDetails;
        }

        public async Task<int> AddStudentAsync(Student student)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@StudentName", student.StudentName));
            parameter.Add(new SqlParameter("@Email", student.Email));
            parameter.Add(new SqlParameter("@Intake", student.Intake));
            parameter.Add(new SqlParameter("@IsDelete", student.IsDelete));

            var result = await Task.Run(() => _dxxtContext.Database
           .ExecuteSqlRawAsync(@"EXEC AddNewStudent @StudentName, @Email, @Intake, @IsDelete", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateStudentAsync(Student student)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@StudentID", student.StudentID));
            parameter.Add(new SqlParameter("@StudentName", student.StudentName));
            parameter.Add(new SqlParameter("@Email", student.Email));
            parameter.Add(new SqlParameter("@Intake", student.Intake));
            parameter.Add(new SqlParameter("@IsDelete", student.IsDelete));

            var result = await Task.Run(() => _dxxtContext.Database
            .ExecuteSqlRawAsync(@"EXEC UpdateStudent @StudentID, @StudentName, @Email, @Intake, @IsDelete", parameter.ToArray()));
            return result;
        }
        public async Task<int> DeleteStudentAsync(int StudentID)
        {
            var parameter = new SqlParameter("@StudentID", StudentID);
            return await _dxxtContext.Database
                     .ExecuteSqlRawAsync("EXEC DeleteStudentByID @StudentID", parameter);
        }
        public async Task<bool> StudentEmailExistsAsync(string email)
        {
            var emailParam = new SqlParameter("@Email", email);
            var existsParam = new SqlParameter
            {
                ParameterName = "@Exists",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };

            await _dxxtContext.Database.ExecuteSqlRawAsync("EXEC CheckStudentEmailExists @Email, @Exists OUT",
                                                           emailParam, existsParam);

            return (bool)existsParam.Value;
        }
        public async Task<bool> CanEnrollInMoreSubjects(int studentId)
        {
            var sqlCommand = "EXEC StudentEnrollCheck @StudentID";
            var studentIdParam = new SqlParameter("@StudentID", studentId);

            // Open connection within a using statement
            using (var connection = new SqlConnection(_dxxtContext.connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlCommand;
                    command.Parameters.Add(studentIdParam);

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        if (await result.ReadAsync())
                        {
                            return Convert.ToBoolean(result.GetInt32(0));
                        }
                        else
                        {
                            throw new InvalidOperationException("Expected a result from CheckSubjectEnrollmentLimit");
                        }
                    }
                }
            } // Connection will be closed here automatically
        }
        
        public async Task<StudentDetailsViewModel> GetStudentDetailsAsync(int StudentID)
        {
            var studentDetailsViewModel = new StudentDetailsViewModel();
            var subjectsAndScores = new List<SubjectScoreViewModel>();

            // Use a 'using' statement to ensure the connection is closed properly
            using (var connection = _dxxtContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC GetStudentDetails @StudentID";
                    command.Parameters.Add(new SqlParameter("@StudentID", StudentID));

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                        {
                            // Assuming the first row contains the student details
                            if (studentDetailsViewModel.StudentName == null)
                            {
                                studentDetailsViewModel.StudentName = result.GetString(result.GetOrdinal("StudentName"));
                                studentDetailsViewModel.Email = result.GetString(result.GetOrdinal("Email"));
                                studentDetailsViewModel.Intake = result.GetDateTime(result.GetOrdinal("Intake"));
                                // Map other student properties if needed
                            }

                            // Handling nullable SubjectName and Marks
                            var subjectName = result.IsDBNull(result.GetOrdinal("SubjectName"))
                                              ? null
                                              : result.GetString(result.GetOrdinal("SubjectName"));
                            var marks = result.IsDBNull(result.GetOrdinal("Marks"))
                                        ? (decimal?)null
                                        : result.GetDecimal(result.GetOrdinal("Marks"));

                            if (subjectName != null)
                            {
                                subjectsAndScores.Add(new SubjectScoreViewModel
                                {
                                    SubjectName = subjectName,
                                    Marks = marks
                                });
                            }
                        }
                    }
                }

                // Ensure connection is closed before making another database call
                connection.Close();
            }

            // Check enrollment eligibility outside the connection block
            studentDetailsViewModel.CanEnrollMore = await CanEnrollInMoreSubjects(StudentID);
            studentDetailsViewModel.SubjectsAndScores = subjectsAndScores;

            return studentDetailsViewModel;
        }



    }
}
