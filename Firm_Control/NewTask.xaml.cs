using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Firm_Control
{
    public partial class NewTask : MetroWindow
    {
        Company company;
        bool edit;
        int index;

        public NewTask()
        {
            InitializeComponent();
        }

        public NewTask(ref Company tmp)
        {
            InitializeComponent();
            company = tmp;
            edit = false;
            customer_cb.ItemsSource = company.customers;
            employeeResponsible_cb.ItemsSource = company.employees;
            btnNewTask.Content = "Dodaj";
        }

        public NewTask(ref Company tmp, int index)
        {
            InitializeComponent();
            company = tmp;
            edit = true;
            this.index = index;

            btnNewTask.Content = "Edytuj";
            customer_cb.ItemsSource = company.customers;
            employeeResponsible_cb.ItemsSource = company.employees;

            name_tb.Text = ((company.tasks)[index]).name;
            completionTime.SelectedDate = (company.tasks)[index].completionTime;
            ((ComboBoxItem)priority_cb.SelectedItem).Content = (company.tasks)[index].priority;
            employeeResponsible_cb.SelectedIndex = company.employees.IndexOf((company.tasks)[index].employeeResponsible);
            customer_cb.SelectedIndex = 0; //To improve
        }

        private void NewTask_Click(object sender, RoutedEventArgs e)
        {
            if (name_tb.Text != "" && employeeResponsible_cb.SelectedItem != null && completionTime.SelectedDate != null && !edit)
            {
                if (customer_cb.SelectedItem != null)
                {
                    company.tasks.Add(new Task(name_tb.Text, company.customers[customer_cb.SelectedIndex], (Employee)employeeResponsible_cb.SelectedItem,
                      (DateTime)completionTime.SelectedDate, ((ComboBoxItem)priority_cb.SelectedItem).Content.ToString()));
                }
                else
                {
                    company.tasks.Add(new Task(name_tb.Text, null, (Employee)employeeResponsible_cb.SelectedItem, (DateTime)completionTime.SelectedDate,
                      ((ComboBoxItem)priority_cb.SelectedItem).Content.ToString()));
                }
                company.Update();
                Company.SerializeToXml(company);
                this.Close();
            }
            else if (name_tb.Text != "" && employeeResponsible_cb.SelectedItem != null && completionTime.SelectedDate != null && edit)
            {
                ((company.tasks)[index]).name = name_tb.Text;
                if (customer_cb.SelectedItem != null)
                {
                    (company.tasks)[index].customer = company.customers[customer_cb.SelectedIndex];
                }
                else
                {
                    (company.tasks)[index].customer = null;
                }
              (company.tasks)[index].employeeResponsible = (Employee)employeeResponsible_cb.SelectedItem;
                (company.tasks)[index].completionTime = (DateTime)completionTime.SelectedDate;
                (company.tasks)[index].priority = ((ComboBoxItem)priority_cb.SelectedItem).Content.ToString();
                CollectionViewSource.GetDefaultView(company.tasks).Refresh();
                company.Update();
                Company.SerializeToXml(company);
                this.Close();
            }
            else
            {
                MessageBox.Show("Uzupełnij wymagane pola oznaczone gwiazdką *");
            }
        }
    }
}
