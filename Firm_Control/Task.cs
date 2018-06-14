using System;
using System.Xml.Serialization;

namespace Firm_Control
{
    [Serializable]
    [XmlType("Task")]
    [XmlInclude(typeof(Task))]
    public class Task
    {
        public String name { get; set; }
        public Customer customer { get; set; }
        public Employee employeeResponsible { get; set; }
        public string howMuchDone { get; set; }
        public int? howMuchTime { get; set; }
        public DateTime completionTime { get; set; }
        public string priority { get; set; }

        public Task()
        {
            name = "Brak nazwy";
            this.customer = new Customer();
            employeeResponsible = new Employee();
            howMuchDone = "Nie rozpoczęte";
            priority = "Niski";
        }
        public Task(String name, Customer customer, Employee employeeResponsible, DateTime completionTime, string priority)
        {
            this.name = name;
            if (customer == null)
            {
                this.customer = new Customer();
                this.customer.login = "Zadanie wewnętrzne";
            }
            else
            {
                this.customer = customer;
            }
            this.employeeResponsible = employeeResponsible;
            howMuchDone = "Nie rozpoczęte";
            this.completionTime = completionTime;
            this.priority = priority;
        }
    }
}
