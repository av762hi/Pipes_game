using System;

namespace Pipe_game.entity
{
    [Serializable]
    public class Score
    {
        public int Id { get; set; }
        public string Game { get; set; }

        public string Player { get; set; }

        public int Scores { get; set; }
    }
}