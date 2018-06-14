using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Firm_Control
{
    [Serializable]
    [XmlRoot("Company_Database")]
    public class Company
    {
        [XmlElement("Fields")]
        public String companyName;

        [NonSerialized]
        public static string pathToBase;

        [XmlArray("Employees_List")]
        [XmlArrayItem("Employee")]
        public ObservableCollection<Employee> employees;

        [XmlArray("Tasks_List")]
        [XmlArrayItem("Task")]
        public ObservableCollection<Task> tasks;

        [XmlArray("Clients_List")]
        [XmlArrayItem("Customer")]
        public ObservableCollection<Customer> customers;

        [XmlArray("Positions")]
        public ObservableCollection<String> positions;

        public Company()
        {
            companyName = "";
            employees = new ObservableCollection<Employee>();
            tasks = new ObservableCollection<Task>();
            customers = new ObservableCollection<Customer>();
            positions = new ObservableCollection<string>();
        }

        public Company(string name)
        {
            companyName = name;
            employees = new ObservableCollection<Employee>();
            tasks = new ObservableCollection<Task>();
            customers = new ObservableCollection<Customer>();
            positions = new ObservableCollection<string>();
        }

        public void Update()
        {
            DateTime now = DateTime.Now;
            foreach (Task task in tasks)
            {
                if ((task.completionTime.DayOfYear - now.DayOfYear) >= 0)
                {
                    task.howMuchTime = task.completionTime.DayOfYear - now.DayOfYear;
                }
                else
                {
                    task.howMuchTime = null;
                }
            }
        }

        public static void SerializeToXml(Company p)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Company));
            TextWriter textWriter = new StreamWriter(pathToBase);
            serializer.Serialize(textWriter, p);
            textWriter.Close();
        }
        public static Company DeserializeFromXml()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Company));
            TextReader textReader = new StreamReader(pathToBase);
            Company company;
            company = (Company)deserializer.Deserialize(textReader);
            textReader.Close();
            return company;
        }
    }
}