using BlazorDictionary.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Models.RequestModels
{
    public class CreateEntryCommentVoteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid CreatedBy { get; set; }
        public VoteType VoteType { get; set; }

        public CreateEntryCommentVoteCommand(Guid entryId, Guid createdBy, VoteType voteType)
        {
            EntryCommentId = entryId;
            CreatedBy = createdBy;
            VoteType = voteType;
        }
    }
}
