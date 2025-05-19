using FileStorage.Application.Commands;
using FileStorage.Application.DTOs;
using FileStorage.Application.Querries;
using FileStorage.Domain.Entities;
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

        [HttpPost("{topicId:int}")]
        public async Task<IActionResult> CreateMetirealAsync([FromRoute] int topicId, [FromBody] CreateMetirealRequestDTO createMetirealRequest) 
        {
            MetirialEntity metirial = new MetirialEntity
            {
                UploadLink = createMetirealRequest.UploadLink,
                FileType = createMetirealRequest.FileType,
                SavedName = createMetirealRequest.SavedName,
                TopicId = topicId
            };
            var result = await sender.Send(new CreateMetirealCommand(metirial));
            return Ok(result);
        }
    }
}
