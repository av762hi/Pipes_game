using System;

namespace Pipe_game.entity
{
    [Serializable]
    public class Rating
    {
        public int Id { get; set; }
        public string Game { get; set; }

        public string Player { get; set; }

        public int Ratings { get; set; }
    }
}