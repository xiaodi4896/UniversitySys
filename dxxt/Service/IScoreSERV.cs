using dxxt.Models;
namespace dxxt.Service
{
    public interface IScoreSERV
    {
        Task<(IEnumerable<ScoreStuSub>, int)> GetScoreListAsync(int pageNumber, int pageSize, string searchQuery);
        Task<IEnumerable<ScoreStuSub>> GetScoreByIdAsync(int ScoreID);
        Task<int> AddScoreAsync(Score score);
        Task<int> UpdateScoreAsync(Score score);
        Task<int> DeleteScoreAsync(int ScoreID);
        Task<bool> DoesScoreExist(int studentId, int subjectId, int? scoreId = null);
        Task<List<StudentScoresViewModel>> GetStudentGroupedScoresAsync();
        Task<bool> CanEnrollInSubject(int studentId, int? excludeScoreId = null);
    }
}
