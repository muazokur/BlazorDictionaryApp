﻿using BlazorDictionary.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}