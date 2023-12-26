using dxxt.Models;
using dxxt.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Drawing.Printing;

namespace dxxt.Service
{
    public class Scoreserv : IScoreSERV
    {
        private readonly IScoreREPO _Scorerepo;

        public Scoreserv(IScoreREPO Scorerepo)
        {
            _Scorerepo = Scorerepo;
        }

        public async Task<(IEnumerable<ScoreStuSub>, int)> GetScoreListAsync(int pageNumber, int pageSize, string searchQuery)
        {
            return await _Scorerepo.GetScoreListAsync(pageNumber, pageSize, searchQuery);
        }

        public async Task<IEnumerable<ScoreStuSub>> GetScoreByIdAsync(int ScoreID)
        {
            return await _Scorerepo.GetScoreByIdAsync(ScoreID);
        }

        public async Task<int> AddScoreAsync(Score score)
        {
            return await _Scorerepo.AddScoreAsync(score);
        }

        public async Task<int> UpdateScoreAsync(Score score)
        {
            return await _Scorerepo.UpdateScoreAsync(score);
        }

        public async Task<int> DeleteScoreAsync(int scoreId)
        {
            return await _Scorerepo.DeleteScoreAsync(scoreId);
        }
        public async Task<bool> DoesScoreExist(int studentId, int subjectId, int? scoreId = null)
        {
            return await _Scorerepo.CheckIfScoreExists(studentId, subjectId, scoreId);
        }

        public async Task<List<StudentScoresViewModel>> GetStudentGroupedScoresAsync()
        {
            return await _Scorerepo.GetGroupedScoresAsync();
        }


        public async Task<bool> CanEnrollInSubject(int studentId, int? excludeScoreId = null)
        {
            int count = await _Scorerepo.GetFailedOrPendingSubjectCountAsync(studentId, excludeScoreId);
            return count < 10;
        }


    }

}
