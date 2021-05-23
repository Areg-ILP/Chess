using Chess.Data.Models;
using Chess.Data.Services.Implementations;
using Chess.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Logic.Data_Managment
{
    public static class UserManager
    {
        private static IBaseService<User> _userService;

        static UserManager()
        {
            _userService = new BaseService<User>();
        }

        public static UserViewModel SignIn(string login, string password)
        {
            var users = _userService.GetAll();

            foreach (var user in users)
            {
                if (user.Login == login && user.Password == password)
                {
                    return new UserViewModel()
                    {
                        Id = user.Id,
                        Name = user.Login,
                        PartyCount = user.PartyCount,
                        WinCount = user.WinCount,
                        LoseCount = user.LoseCount,
                        DrawCount = user.DrawCount,
                        CreationDate = user.CreationDate
                    };
                }
            }

            throw new Exception("Incorrect login or password!");
        }

        public static UserViewModel Registration(string login, string password)
        {
            var users = _userService.GetAll();

            foreach (var user in users)
            {
                if (user.Login == login)
                    throw new Exception("Busy login");
            }

            var creationDate = DateTime.Now;
            var newUser = new User()
            {
                Id = users.Count,
                Login = login,
                Password = password,
                CreationDate = creationDate
            };

            _userService.Create(newUser);

            return new UserViewModel { Name = login, CreationDate = creationDate };
        }

        public static void UpdateProfile(UserViewModel user)
        {
            //mb
        }
    }
}
