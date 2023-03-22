using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {

        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.FavExcahangeName, exchangeType: DictionaryConstants.DefaultExchangeType, queueName: DictionaryConstants.CreateEntryFavQueueName, obj: new CreateEntryFavEvent()
            {
                EntryId=request.EntryId.Value,
                CreatedBy=request.UserId.Value
            });

            return Task.FromResult(true);
        }
    }
}
