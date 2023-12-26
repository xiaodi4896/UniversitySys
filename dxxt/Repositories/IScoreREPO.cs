using dxxt.Models;

namespace dxxt.Repositories
{
    public interface IScoreREPO
    {
        public Task<(IEnumerable<ScoreStuSub>, int)> GetScoreListAsync(int pageNumber, int pageSize, string searchQuery);
        public Task<IEnumerable<ScoreStuSub>> GetScoreByIdAsync(int ScoreID);
        public Task<int> AddScoreAsync(Score score);
        public Task<int> UpdateScoreAsync(Score score);
        public Task<int> DeleteScoreAsync(int ScoreID);
        Task<List<StudentScoresViewModel>> GetGroupedScoresAsync();
        Task<bool> CheckIfScoreExists(int studentId, int subjectId, int? scoreId = null);
        Task<int> GetFailedOrPendingSubjectCountAsync(int studentId, int? excludeScoreId = null);
    }


}