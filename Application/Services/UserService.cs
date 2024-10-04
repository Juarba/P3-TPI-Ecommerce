﻿using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;

        public UserService (IUserRepository repository)
        {
            _repository = repository;
        }

        public List<User> GetAll()
        {
            return _repository.Get();
        }


    }
}
