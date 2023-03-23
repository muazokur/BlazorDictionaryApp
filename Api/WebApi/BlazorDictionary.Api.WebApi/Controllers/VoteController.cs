using BlazorDictionary.Api.Application.Features.Commands.Entry.DeleteVote;
using BlazorDictionary.Api.Application.Features.Commands.EntryComment.DeleteVote;
using BlazorDictionary.Common.Models.RequestModels;
using BlazorDictionary.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDictionary.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        private readonly IMediator mediator;

        public VoteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId,VoteType voteType = VoteType.UpVote)
        {
            var result=await mediator.Send(new CreateEntryVoteCommand(entryId, UserId.Value,voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, UserId.Value, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            await mediator.Send(new DeleteEntryVoteCommand(entryId,UserId.Value));

            return Ok();
        }

        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryId)
        {
            await mediator.Send(new DeleteEntryCommentVoteCommand(entryId, UserId.Value));

            return Ok();
        }
    }
}
