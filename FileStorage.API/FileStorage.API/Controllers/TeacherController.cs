using AutoMapper;
using FileStorage.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController(ISender sender) : ControllerBase
    {
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetTeacherById([FromRoute] int Id)
        {
            var result = await sender.Send(new GetTeacherByIdQuerry(Id));
            return Ok(result);
        }
    }
}
