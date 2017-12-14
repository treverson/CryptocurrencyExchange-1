﻿using AutoMapper;
using Stage2HW.Business.Dtos;
using Stage2HW.DataAccess.Models;
using System.Collections.Generic;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.DataAccess.Repositories.Interfaces;

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
            var userEntity = _iMapper.Map<UserDto, User>(newUser);
            _userRepository.AddUser(userEntity);
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
        //-----uniwersal
        public void RegisterTransaction(TransactionDto transaction)
        {
            var transactionEntity = _iMapper.Map<TransactionDto, Transaction>(transaction);

            _userRepository.RegisterTransaction(transactionEntity);
        }
        //uniwersasl
        //public void RegisterDeposit(TransactionDto deposit)
        //{
        //    var depositEntity = _iMapper.Map<TransactionDto, Transaction>(deposit);

        //    _userRepository.RegisterDeposit(depositEntity);
        //}

        //public void RegisterWithdrawal(TransactionDto withdrawal)
        //{
        //    var withdrawalEntity = _iMapper.Map<TransactionDto, Transaction>(withdrawal);

        //    _userRepository.RegisterWithdrawal(withdrawalEntity);
        //}

        public List<TransactionDto> GetTransactionHistory(int activeUserId)
        {
            var transactionsHistoryEntity = _userRepository.GetTransactionsHistory(activeUserId);

            var transactionsHistory = new List<TransactionDto>();
            
            foreach (var transaction in transactionsHistoryEntity)
            {
                var temp = _iMapper.Map<Transaction, TransactionDto>(transaction);

                transactionsHistory.Add(temp);
            }

            return transactionsHistory;
        }

     
    }
}