using System;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Firm_Control
{
    [Serializable]
    [XmlType("Person")]
    [XmlInclude(typeof(Person))]
    public class Person
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public int ID;
        public String login { get; set; }
        public String password { get; set; }
        public String email { get; set; }

        [XmlArray("Lista_Wiadomosci")]
        [XmlArrayItem("Mail")]
        public ObservableCollection<Mail> messages;

        public int messagesToRead;
        public String firstNameAndLastName { get; set; }

        public Person()
        {
        }
        public Person(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
        public Person(string firstName, string lastName, string login, string password, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.login = login;
            this.password = password;
            this.email = email;

            messages = new ObservableCollection<Mail>();

            firstNameAndLastName = this.firstName + " " + this.lastName;
        }
        public Person(string firstName, string lastName, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;

            messages = new ObservableCollection<Mail>();

            firstNameAndLastName = this.firstName + " " + this.lastName;
        }
    }
}
