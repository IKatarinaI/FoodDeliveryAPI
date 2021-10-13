using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using FoodDelivery.Services.Interfaces;
using System;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net.BCrypt;

namespace FoodDelivery.Services
{
    public class UserService : IUserService
    {
        private readonly IReadUserRepository _readRepository;
        private readonly IWriteUserRepository _writeRepository;

        public UserService(IReadUserRepository readRepository, IWriteUserRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public void AddUser(User user)
        {
            var usernameExists = IsUsernameUnique(user.Username);

            if (usernameExists)
                throw new AppException("Username is already taken");

            user.Password = BCryptNet.HashPassword(user.Password);
            _writeRepository.AddUser(user);
        }

        public User Authenticate(User user)
        {
            var authenticatedUser = _readRepository.GetUserByUsername(user.Username);
            
            if (authenticatedUser is null) 
                throw new AppException("User not found");

            if(!BCryptNet.Verify(user.Password, authenticatedUser.Password))
                throw new AppException("Password is not correct");

            return authenticatedUser;
        }

        public User GetUser(Guid userId)
        {
            var user = _readRepository.GetUser(userId);

            if (user is null) 
                throw new KeyNotFoundException("User not found");

            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _readRepository.GetUsers();
        }

        public bool IsUsernameUnique(string username)
        {
            return _readRepository.IsUsernameUnique(username);
        }

        public bool Save()
        {
            return _writeRepository.Save();
        }
    }
}
