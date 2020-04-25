using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pipe_game.entity;
using Pipe_game.Service;

namespace Pipe_game.Service
{
    public class CommentServiceEF : ICommentService
    {
        public void AddComment(Comment comment)
        {
            using (var context = new PipeGameDbContext())
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        public IList<Comment> GetComments()
        {
            using (var context = new PipeGameDbContext())
            {
                return (from s in context.Comments
                        orderby s.Text
                            descending
                        select s).ToList();
            }
        }

    }
}
