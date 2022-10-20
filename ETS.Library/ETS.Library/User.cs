using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    public class User
    {
        private string _username;
        private string _password;

        public User()
        {
            _username = "";
            _password = "";
        }

        public User(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }

        public static List<User> ReadUsers()
        {
            string path = @"./users.txt";

            List<User> users = new List<User>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string str = sr.ReadLine();
                    string[] strArr = str.Split(',');

                    User user = new User(strArr[0], strArr[1]);

                    users.Add(user);
                }
            }

            return users;
        }

        public string GetUserName()
        {
            return Username;
        }

        public string GetPassword()
        {
            return Password;
        }
    }
}
