﻿using FileStorage.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectTopicController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSubjectsWithTopics()
        {
            var result = await sender.Send(new GetAllSubjectsWithTopicsQuerry());
            return Ok(result);
        }

        [HttpGet("{subjectId:int}")]
        public async Task<IActionResult> GetAllTopicsBySubjectIdAsync([FromRoute] int subjectId)
        {
            var result = await sender.Send(new GetAllTopicsBySubjectIdQuerry(subjectId));
            return Ok(result);
        }

    }
}
