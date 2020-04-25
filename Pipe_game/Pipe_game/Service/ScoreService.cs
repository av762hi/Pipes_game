using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Pipe_game.entity;

namespace Pipe_game.Service
{
    public class ScoreService
    {
        public class ScoreServiceFile: IScoreService
        {
            private const string FileName = "score.bin";
            
            private List<Score> scores = new List<Score>();
            
            public  void AddScore(Score score)
            {
                scores.Add(score);

                SaveScore();
            }
            
            public IList<Score> GetScores()
            {
                LoadScore();

             //   return (from c in scores select c).ToList();

                return (from s in scores
                        orderby s.Scores descending
                        select s).Take(3).ToList();


            }

            public void ClearScores()
            {
                scores.Clear();
                File.Delete(FileName);
            }
            
            private void SaveScore()
            {
                using (var fs = File.OpenWrite(FileName))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(fs, scores);
                }
            }
            
            private void LoadScore()
            {
                if (File.Exists(FileName))
                {
                    using (var fs = File.OpenRead(FileName))
                    {
                        var bf = new BinaryFormatter();
                        scores = (List<Score>)bf.Deserialize(fs);
                    }
                }
            }
        }
    }
}