using BlazorDictionary.Common;
using BlazorDictionary.Common.Event.EntryComment;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.FavExcahangeName,exchangeType:DictionaryConstants.DefaultExchangeType,queueName:DictionaryConstants.CreateEntryCommentFavQueueName, obj:new  CreateEntryCommentFavEvent()
            {
                EntryCommentId= request.EntryCommentId,
                CreatedBy=request.UserId
            });

            return await Task.FromResult(true);
        }
    }
}
