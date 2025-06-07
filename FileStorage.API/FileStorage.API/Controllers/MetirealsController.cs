using AutoMapper;
using FileStorage.Application.Commands;
using FileStorage.Application.DTOs;
using FileStorage.Application.Querries;
using FileStorage.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetirealsController(ISender sender, IMapper mapper) : ControllerBase
    {
        private readonly IMapper mapper = mapper;

        [HttpGet] // this is the method to get all the topics with their corresponding metireals
        public async Task<IActionResult> GetAllMetirealsWithTopicsAsync()
        {
            var result = await sender.Send(new GetAllMetirealsWithTopicsQuerry());
            return Ok(result);
        }
        
        [HttpGet("{topicId:int}")]
        public async Task<IActionResult> GetAllMetirealsByTopicIdAsync([FromRoute] int topicId)
        {
            var result = await sender.Send(new GetAllMetirealsByTopicIdQuerry(topicId));
            var response = mapper.Map<List<GetAllMetirealResponseDTO>>(result);
            return Ok(response);
        }

        [HttpPost("{topicId:int}")]
        public async Task<IActionResult> CreateMetirealAsync([FromRoute] int topicId, [FromBody] CreateMetirealRequestDTO createMetirealRequest) 
        {
            var result = await sender.Send(new CreateMetirealCommand(topicId, createMetirealRequest));
            return Ok(result);
        }
    }
}
