using System;
using System.Xml.Serialization;

namespace Firm_Control
{
    [Serializable]
    [XmlType("Employee")]
    [XmlInclude(typeof(Employee))]

    public class Employee : Person
    {
        public int permissions;
        public double? salary { get; set; }
        public int phone { get; set; }
        public string position { get; set; }

        public Employee()
        { }
        public Employee(string login, string password)
        {
            base.login = login;
            base.password = password;
        }

        public Employee(string login, string password, string firstName, string lastName,
          string email, string position, double? salary)
          : base(firstName, lastName, login, password, email)
        {
            this.position = position;
            this.salary = salary;
        }
        public override string ToString()
        {
            return firstNameAndLastName;
        }
    }
}