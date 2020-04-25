using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Pipe_game.entity;
using Pipe_game.Service;


namespace WebApplication.APIControllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService = new CommentServiceEF();


        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return _commentService.GetComments();
        }


        [HttpPost]
        public void Post([FromBody] Comment comment)
        {
            _commentService.AddComment(comment);
        }
    }

}


