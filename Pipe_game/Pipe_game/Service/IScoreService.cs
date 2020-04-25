using System.Collections.Generic;
using Pipe_game.entity;

namespace Pipe_game.Service
{
    public interface IScoreService
    {
        void AddScore(Score score);

        IList<Score> GetScores();

    }
}