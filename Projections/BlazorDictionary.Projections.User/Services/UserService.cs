using BlazorDictionary.Common.Event.User;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Projections.UserService.Services
{
    public class UserService
    {
        private string connStr;

        public UserService(IConfiguration configuration)
        {
            //var connStr = configuration.GetConnectionString("SqlServer");
           // connStr = "Server=DESKTOP-G7KHF4G\\SQLEXPRESS;Initial Catalog=BlazorDictionaryDB;Integrated security=true";

        }

        public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
        {
            connStr = "Server=DESKTOP-G7KHF4G\\SQLEXPRESS;Initial Catalog=BlazorDictionaryDB;Integrated security=true";

            var guid = Guid.NewGuid();

            using var connection = new SqlConnection(connStr);

            await connection.ExecuteAsync("INSERT INTO emailconfirmation (Id,CreateDate,OldEmailAdress,NewEmailAdress)" +
                "VALUES (@Id,GETDATE(),@OldEmailAdress,@NewEmailAdress)", new
                {
                    Id = guid,
                    OldEmailAdress = @event.OldEmailAdress,
                    NewEmailAdress = @event.NewEmailAdress,
                });

            return guid;
        }
    }
}
