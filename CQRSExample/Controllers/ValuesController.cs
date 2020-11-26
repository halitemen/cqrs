using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSExample.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ValuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var query = new Queries.GetCommentsQuery();
            var result =  _mediator.Send(query);
            return Ok(result.Result);
        }

        [HttpGet("Get/{id}")]
        public ActionResult<CommentDto> Get(int id)
        {
            var query = new Queries.GetCommentQuery(id);
            var result = _mediator.Send(query);
            return Ok(result.Result);
        }

        [HttpPost("Post")]
        public void Post(CommentDto commentDto)
        {

        }
    }
}
