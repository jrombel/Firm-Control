using System;
using System.Xml.Serialization;

namespace Firm_Control
{
    [Serializable]
    [XmlType("Customer")]
    [XmlInclude(typeof(Customer))]
    public class Customer : Person
    {
        public Customer()
        { }
        public Customer(string firstName, string lastName, string email) : base(firstName, lastName, email)
        { }
        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}
