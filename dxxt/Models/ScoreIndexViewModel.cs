namespace dxxt.Models
{
    public class ScoreIndexViewModel
    {
        public IEnumerable<ScoreStuSub>? Scores { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
