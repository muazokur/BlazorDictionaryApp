using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Events.EntryComment;
using BlazorDictionary.Common.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Projections.VoteService.Services
{
    public class VoteService
    {
        private readonly string connectionString;

        public VoteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryVote(CreateEntryVoteEvent vote)
        {
            await DeleteEntryVote(vote.EntryId, vote.CreatedBy);

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO ENTRYVOTE (Id,CreateDate,EntryId,VoteType,CratedById) " +
                "VALUES (@Id,GETDATE(),@EntryId,@VoteType,@CreatedById)", new
                {
                    Id = Guid.NewGuid(),
                    EntryId = vote.EntryId,
                    CreatedById = vote.CreatedBy,
                    VoteType = (int)vote.VoteType,
                });
        }

        public async Task DeleteEntryVote(Guid entryId, Guid userId)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryVote WHERE EntryId=@EntryId AND CRATEDBYID=@UserId", new
            {
                Id = Guid.NewGuid(),
                EntryId = entryId,
                UserId = userId
            });
        }

        public async Task CreateEntryCommentVote(CreateEntryCommentVoteEvent vote)
        {
            await DeleteEntryCommentVote(vote.EntryCommentId, vote.CreatedBy);

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO ENTRYCOMMENTVOTE (Id,CreateDate,EntryCommentId,VoteType,CratedById) " +
                "VALUES (@Id,@CreateDate,@EntryCommentId,@VoteType,@CratedById)", new
                {
                    Id = Guid.NewGuid(),
                    EntryCommentId = vote.EntryCommentId,
                    CratedById = vote.CreatedBy,
                    VoteType = (int)vote.VoteType,
                    CreateDate = DateTime.Now
                });
        }

        public async Task DeleteEntryCommentVote(Guid entryId, Guid userId)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM ENTRYCOMMENTVOTE WHERE EntryCommentId=@EntryCommentId AND CRATEDBYID=@UserId", new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = entryId,
                UserId = userId
            });
        }
    }
}
