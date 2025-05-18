using FileStorage.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetirealsController(ISender sender) : ControllerBase
    {
        [HttpGet("{topicId:int}")]
        public async Task<IActionResult> GetAllMetirealsByTopicIdAsync([FromRoute] int topicId)
        {
            var result = await sender.Send(new GetAllMetirealsByTopicIdQuerry(topicId));
            return Ok(result);
        }
    }
}
