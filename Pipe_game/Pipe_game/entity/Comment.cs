using System;
namespace Pipe_game.entity
{
    [Serializable]
    public class Comment
    {
        public int Id { get; set; }
        public string Game { get; set; }

        public string Player { get; set; }

        public string Text { get; set; }
    
    }
}