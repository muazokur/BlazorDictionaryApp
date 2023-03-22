using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.FavExcahangeName, exchangeType: DictionaryConstants.DefaultExchangeType, queueName: DictionaryConstants.DeleteEntryFavQueueName,obj: new DeleteEntryFavEvent()
            {
                CreatedBy=request.UserId,
                EntryId=request.EntryId
            });

            return await Task.FromResult(true);
        }
    }
}
