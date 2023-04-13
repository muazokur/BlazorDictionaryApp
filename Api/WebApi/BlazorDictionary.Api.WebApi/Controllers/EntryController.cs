using BlazorDictionary.Api.Application.Features.Queries.GetEntries;
using BlazorDictionary.Api.Application.Features.Queries.GetEntryComments;
using BlazorDictionary.Api.Application.Features.Queries.GetEntryDetail;
using BlazorDictionary.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorDictionary.Api.Application.Features.Queries.GetUserEntries;
using BlazorDictionary.Api.WebApi.Extensions;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDictionary.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
        private readonly IMediator mediator;

        public EntryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await mediator.Send(new GetEntryDetailQuery(id, UserId));

                return Ok(result);
            }
            catch (Exception)
            {
                var result = await mediator.Send(new GetEntryDetailQuery(id, Guid.NewGuid()));

                return Ok(result);
            }

        }


        [HttpGet]
        [Route("Comments/id")]
        public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
        {
            try
            {
                var result = await mediator.Send(new GetEntryCommentsQuery(page, pageSize, id, UserId));

                return Ok(result);
            }
            catch (Exception)
            {

                var result = await mediator.Send(new GetEntryCommentsQuery(page, pageSize, id, Guid.NewGuid()));

                return Ok(result);
            }
           ;
        }

        [HttpGet]
        [Route("UserEntries")]
        [Authorize]
        public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserId.Value;

            var result = await mediator.Send(new GetUserEntriesQuery(page, pageSize, userName, userId));

            return Ok(result);
        }

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
        {
            try
            {
                var entries = await mediator.Send(new GetMainPageEntriesQuery(UserId.Value, page, pageSize));
                //var entries = await mediator.Send(new GetMainPageEntriesQuery(User.GetUserId(), page, pageSize));
                return Ok(entries);
            }
            catch (Exception)
            {

                var entries = await mediator.Send(new GetMainPageEntriesQuery(Guid.NewGuid(), page, pageSize));

                return Ok(entries);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var entries = await mediator.Send(query);

            return Ok(entries);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }


        [HttpPost]
        [Route("CreateEntry")]
        //[Authorize]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        [Authorize]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await mediator.Send(command);

            return Ok(result);
        }


    }
}
