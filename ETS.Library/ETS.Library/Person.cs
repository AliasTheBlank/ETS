using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    public abstract class Person
    {
        private string _firstName;
        private string _lastName;

        public Person()
        {
            _firstName = "";
            _lastName = "";
        }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }

        public override string ToString()
        {
            return $"{_firstName},{_lastName}";
        }

        public virtual string DisplayData()
        {
            return $"Name: {_firstName} {_lastName}";
        }
    }
}
