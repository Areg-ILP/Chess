using Chess.Data;
using Chess.Data.Models;
using Chess.Data.RepositoryPatterns;
using System;

namespace Chess.Logic.Data_Managment
{
    public static class UserManager
    {
        private static IBaseRepository<User> _userRepository;

        static UserManager()
        {
            _userRepository = RepositoryHelper.GetRepository<User>();
        }

        public static UserViewModel SignIn(string login, string password)
        {
            var users = _userRepository.GetAll();

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
            var users = _userRepository.GetAll();

            foreach (var user in users)
            {
                if (user.Login == login)
                    throw new Exception("Busy login");
            }

            var newUser = new User()
            {
                Id = users.Count,
                Login = login,
                Password = password,
                CreationDate = DateTime.Today
            };

            _userRepository.Create(newUser);

            return new UserViewModel
            {
                Id = newUser.Id,
                Name = login,
                CreationDate = newUser.CreationDate
            };
        }

        public static void UpdateProfile(UserViewModel user)
        {
            //mb
        }
    }
}
