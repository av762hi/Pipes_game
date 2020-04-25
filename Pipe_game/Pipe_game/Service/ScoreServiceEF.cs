using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pipe_game.entity;
using Pipe_game.Service;

namespace Pipe_game.Service
{
    public class ScoreServiceEF : IScoreService
    {
        public void AddScore(Score score)
        {
            using (var context = new PipeGameDbContext())
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }
        }

        public IList<Score> GetScores()
        {
            using (var context = new PipeGameDbContext())
            {
                return (from s in context.Scores
                        orderby s.Scores
                            descending
                        select s).Take(5).ToList();
            }
        }

        public void ClearScores()
        {
            using (var context = new PipeGameDbContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Scores");
            }
        }
    }
}
