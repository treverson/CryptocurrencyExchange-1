﻿using System;
using AutoMapper;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Stage2HW.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _iMapper;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
            });
            _iMapper = config.CreateMapper();

            _userRepository = userRepository;
        }

        public List<UserDto> GetExistingUsers()
        {
            var existingUsersFromDb = _userRepository.GetExistingUsers();

            var existingUsers = new List<UserDto>();

            foreach (var user in existingUsersFromDb)
            {
                var temp = _iMapper.Map<User, UserDto>(user);
                existingUsers.Add(temp);
            }

            return existingUsers;
        }

        public void AddUser(UserDto newUser)
        {
            if (CheckIfUserAlreadyExists(newUser.Login))
            {
                throw new Exception("Login already taken!");
            }

            var userEntity = _iMapper.Map<UserDto, User>(newUser);
            _userRepository.AddUser(userEntity);
        }

        private bool CheckIfUserAlreadyExists(string newUserLogin)
        {
            var user = _userRepository.GetUserByLogin(newUserLogin);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public UserDto GetUser(string userNickName, string userPassword)
        {
            var userEntity = _userRepository.GetUser(userNickName, userPassword);
            if (userEntity == null)
            {
                return null;
            }
            var userDto = _iMapper.Map<User, UserDto>(userEntity);
            return userDto;
        }

        public bool CheckUserPassword(UserDto userDto)
        {
            var userEntity = _iMapper.Map<UserDto, User>(userDto);
            return _userRepository.CheckUserPassword(userEntity);
        }
    
    }
}