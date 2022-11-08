using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    [Flags]
    enum Permit : short
    {
        None = 0,
        Create = 1,
        Delete = 2,
        Manage = 4,
        Master = 8
    }

    public class User
    {
        private string _username;
        private string _password;
        private Permit _permit;
        

        public User()
        {
            _username = "";
            _password = "";
           _permit = Permit.None;
        }

        public User(string username, string password) : this()
        {
            _username = username;
            _password = password;
        }

        public User(string username, string password, int permits) : this(username, password)
        {
            _permit = (Permit) permits;
        }

        public User(string username, string password, bool create, bool delete, bool manage) : this(username, password)
        {
            if (create)
                _permit |= Permit.Create;
            if (delete)
                _permit |= Permit.Delete;
            if (manage)
                _permit |= Permit.Manage;
        }

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        internal Permit Permit { get => _permit; set => _permit = value; }

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

                    User user = new User(strArr[0], strArr[1], Convert.ToInt32(strArr[2]));

                    users.Add(user);
                }
            }

            return users;
        }

        public override string ToString()
        {
            return $"{_username},{_password},{(int)_permit}";
        }

        public string DetailedData()
        {
            return $"{_username},{_password},{_permit.HasFlag(Permit.Create)},{_permit.HasFlag(Permit.Delete)},{_permit.HasFlag(Permit.Manage)}";
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
