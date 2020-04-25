using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pipe_game.entity;
using Pipe_game.Service;

namespace Pipe_game.Service
{
    public class RatingServiceEF : IRatingService
    {
        public void AddRating(Rating rating)
        {
            using (var context = new PipeGameDbContext())
            {
                context.Ratings.Add(rating);
                context.SaveChanges();
            }
        }

        public IList<Rating> GetRatings()
        {
            using (var context = new PipeGameDbContext())
            {
                return (from s in context.Ratings
                        orderby s.Ratings
                            descending
                        select s).ToList();
            }
        }

    }
}
