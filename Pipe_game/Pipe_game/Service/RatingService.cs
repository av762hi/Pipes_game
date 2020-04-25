using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Pipe_game.entity;

namespace Pipe_game.Service
{
    public class RatingService
    {
        public class RatingServiceFile : IRatingService
        {
            private const string FileName = "rating.bin";

            private List<Rating> ratings = new List<Rating>();

            public  void AddRating(Rating rating)
            {
                ratings.Add(rating);

                SaveRating();
            }

            public IList<Rating> GetRatings()
            {
                LoadRating();

                return (from c in ratings select c).ToList();
               
            }

            public void ClearRatings()
            {
                ratings.Clear();
                File.Delete(FileName);
            }


            private void SaveRating()
            {
                using (var fs = File.OpenWrite(FileName))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(fs, ratings);
                }
            }

            private void LoadRating()
            {
                if (File.Exists(FileName))
                {
                    using (var fs = File.OpenRead(FileName))
                    {
                        var bf = new BinaryFormatter();
                        ratings = (List<Rating>)bf.Deserialize(fs);
                    }
                }
            }

        }

    }
}