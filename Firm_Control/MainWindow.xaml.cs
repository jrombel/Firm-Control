using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Firm_Control
{
    public partial class MainWindow : MetroWindow
    {
        Company company;
        Employee logged;

        bool? receivedChecked;
        bool? sentChecked;
        public MainWindow(Company tmp, Employee logged)
        {
            this.logged = logged;
            InitializeComponent();
            company = tmp;
            nemail_tbs_l.Content = logged.messagesToRead;
            logged_l.Content = logged.firstNameAndLastName;
            tasks_lv.ItemsSource = company.tasks;
            customers_lv.ItemsSource = company.customers;
            employees_lv.ItemsSource = company.employees;
            receivedChecked = received_cb.IsChecked;
            sentChecked = sent_cb.IsChecked;
            Messages();
        }

        private void NewTask_Click(object sender, RoutedEventArgs e)
        {
            (new NewTask(ref company)).Show();
        }

        private void DoneTask_Click(object sender, RoutedEventArgs e)
        {
            if (tasks_lv.SelectedItem != null)
            {
                if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "Nie rozpoczęte")
                {
                    company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone = "W realizacji";
                    CollectionViewSource.GetDefaultView(company.tasks).Refresh();
                    Company.SerializeToXml(company);
                }
                else if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "W realizacji")
                {
                    company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone = "Wykonane";
                    CollectionViewSource.GetDefaultView(company.tasks).Refresh();
                    Company.SerializeToXml(company);
                }
                else if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "Wykonane")
                {
                    company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone = "W realizacji";
                    CollectionViewSource.GetDefaultView(company.tasks).Refresh();
                    Company.SerializeToXml(company);
                }

                if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "Nie rozpoczęte")
                {
                    done_btn.Content = "Rozpocznij";
                }
                else if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "W realizacji")
                {
                    done_btn.Content = "Wykonane";
                }
                else if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "Wykonane")
                {
                    done_btn.Content = "Wzów";
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (tasks_lv.Items.IndexOf(tasks_lv.SelectedItem) >= 0)
            {
                (new NewTask(ref company, tasks_lv.Items.IndexOf(tasks_lv.SelectedItem))).Show();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (tasks_lv.Items.IndexOf(tasks_lv.SelectedItem) >= 0)
            {
                company.tasks.RemoveAt(tasks_lv.Items.IndexOf(tasks_lv.SelectedItem));
                Company.SerializeToXml(company);
            }
        }

        private void tasks_lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tasks_lv.SelectedItem != null)
            {
                if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "Nie rozpoczęte")
                {
                    done_btn.Content = "Rozpocznij";
                }
                else if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "W realizacji")
                {
                    done_btn.Content = "Wykonane";
                }
                else if (company.tasks[tasks_lv.Items.IndexOf(tasks_lv.SelectedItem)].howMuchDone == "Wykonane")
                {
                    done_btn.Content = "Wzów";
                }
            }
        }

        private void NewCustomer_Click(object sender, RoutedEventArgs e)
        {
            (new NewCustomer(ref company)).Show();
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (customers_lv.Items.IndexOf(customers_lv.SelectedItem) >= 0)
            {
                (new NewCustomer(ref company, customers_lv.Items.IndexOf(customers_lv.SelectedItem))).Show();
            }
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (customers_lv.Items.IndexOf(customers_lv.SelectedItem) >= 0)
            {
                company.customers.RemoveAt(customers_lv.Items.IndexOf(customers_lv.SelectedItem));
                Company.SerializeToXml(company);
            }
        }

        private void NewEmployee_Click(object sender, RoutedEventArgs e)
        {
            (new NewEmployee(ref company)).Show();
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (employees_lv.Items.IndexOf(employees_lv.SelectedItem) >= 0)
            {
                (new NewEmployee(ref company, employees_lv.Items.IndexOf(employees_lv.SelectedItem))).Show();
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (employees_lv.Items.IndexOf(employees_lv.SelectedItem) >= 0)
            {
                company.employees.RemoveAt(employees_lv.Items.IndexOf(employees_lv.SelectedItem));
                Company.SerializeToXml(company);
            }
        }

        private void Nemail_tb_Click(object sender, RoutedEventArgs e)
        {
            (new NewMail(ref company, logged)).Show();
        }

        private void OpenMail_Click(object sender, RoutedEventArgs e)
        {
            if (messages_lv.Items.IndexOf(messages_lv.SelectedItem) >= 0)
            {
                (new NewMail(ref company, logged, messages_lv.Items.IndexOf(messages_lv.SelectedItem), true)).Show();
            }
        }

        private void ReplyMail_Click(object sender, RoutedEventArgs e)
        {
            if (messages_lv.Items.IndexOf(messages_lv.SelectedItem) >= 0)
            {
                (new NewMail(ref company, logged, messages_lv.Items.IndexOf(messages_lv.SelectedItem), false)).Show();
            }
        }

        private void DeleteMail_Click(object sender, RoutedEventArgs e)
        {
            if (messages_lv.Items.IndexOf(messages_lv.SelectedItem) >= 0)
            {
                logged.messages.RemoveAt(messages_lv.Items.IndexOf(messages_lv.SelectedItem));
                Company.SerializeToXml(company);
            }
        }

        private void Messages()
        {
            messages_lv.Items.Clear();
            foreach (Mail i in logged.messages)
            {
                if (receivedChecked == true)
                {
                    if (i.to == logged.firstNameAndLastName)
                    {
                        messages_lv.Items.Insert(0, i);
                    }
                    if (!i.seen)
                    {
                        logged.messagesToRead++;
                    }
                }
                if (sentChecked == true)
                {
                    if (i.from == logged.firstNameAndLastName)
                    {
                        messages_lv.Items.Insert(0, i);
                    }
                }
            }
        }

        private void received_Checked(object sender, RoutedEventArgs e)
        {
            receivedChecked = received_cb.IsChecked;
            Messages();
        }

        private void sent_Checked(object sender, RoutedEventArgs e)
        {
            sentChecked = sent_cb.IsChecked;
            Messages();
        }

        private void received_Unchecked(object sender, RoutedEventArgs e)
        {
            receivedChecked = received_cb.IsChecked;
            Messages();
        }

        private void sent_Unchecked(object sender, RoutedEventArgs e)
        {
            sentChecked = sent_cb.IsChecked;
            Messages();
        }
    }
}
