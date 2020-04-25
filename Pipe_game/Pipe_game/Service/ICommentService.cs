using System.Collections.Generic;
using Pipe_game.entity;

namespace Pipe_game.Service
{
    public interface ICommentService
    {

        void AddComment(Comment comment);

            IList<Comment> GetComments();

         //   void ClearComments();
        
    }
}