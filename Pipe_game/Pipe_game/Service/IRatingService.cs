using System.Collections.Generic;
using Pipe_game.entity;

namespace Pipe_game.Service
{
    public interface IRatingService
        {
            void AddRating(Rating rating);

            IList<Rating> GetRatings();

          //  void ClearRatings();
        }

    }
