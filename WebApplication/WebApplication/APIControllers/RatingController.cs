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
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService = new RatingServiceEF();


        [HttpGet]
        public IEnumerable<Rating> Get()
        {
            return _ratingService.GetRatings();
        }


        [HttpPost]
        public void Post([FromBody] Rating rating)
        {
            _ratingService.AddRating(rating);
        }
    }

}

