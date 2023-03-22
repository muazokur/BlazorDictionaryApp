﻿using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDictionary.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryContoller : ControllerBase
    {
        private readonly IMediator mediator;

        [HttpPost]
        [Route("CreateEntryComment")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntry")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }


    }
}
