using System.Windows;
using System.Windows.Data;
using MahApps.Metro.Controls;

namespace Firm_Control
{
    public partial class NewMail : MetroWindow
    {
        Company company;
        Employee logged;
        bool edit;
        bool read;
        int index;

        public NewMail(ref Company company, Employee logged)
        {
            InitializeComponent();
            this.company = company;
            this.logged = logged;
            edit = false;
            index = 0;
        }

        public NewMail(ref Company tmp, Employee logged, int index, bool read)
        {
            InitializeComponent();
            company = tmp;
            this.logged = logged;
            this.read = read;
            edit = true;
            index = 0;

            reply_tb.Visibility = Visibility.Visible;
            mailToEmployee_cb.Visibility = Visibility.Collapsed;
            mailTo_cb.Visibility = Visibility.Collapsed;
            reply_tb.Text = logged.messages[index].to;

            if (!read)
            {
                content_tb.Visibility = Visibility.Collapsed;
                subject_tb.Visibility = Visibility.Collapsed;
                wTresc.Visibility = Visibility.Visible;
                subject_tb.Visibility = Visibility.Visible;
                subject_tb.Text = "Re: " + logged.messages[index].subject;
                wTresc.Text = "\n\n\n----------------------------------------------------------------------------------------------------------------------------\n\n" + logged.messages[index].content;
            }
            else
            {
                wTresc.Visibility = Visibility.Collapsed;
                subject_tb.Visibility = Visibility.Collapsed;
                content_tb.Visibility = Visibility.Visible;
                subject_tb.Visibility = Visibility.Visible;
                subject_tb.Text = logged.messages[index].subject;
                content_tb.Text = logged.messages[index].content;
                sendMail_btn.Visibility = Visibility.Collapsed;
                logged.messages[index].seen = true;
            }
        }

        private void mailToEmployees_cbi(object sender, RoutedEventArgs e)
        {
            mailToEmployee_cb.ItemsSource = company.employees;
            mailToEmployee_cb.Visibility = Visibility.Visible;
            mailTo_cb.Visibility = Visibility.Collapsed;
        }

        private void mailToCustomers_cbi(object sender, RoutedEventArgs e)
        {
            mailToCustomer_cb.ItemsSource = company.customers;
            mailToCustomer_cb.Visibility = Visibility.Visible;
            mailTo_cb.Visibility = Visibility.Collapsed;
        }

        private void SendMail_Click(object sender, RoutedEventArgs e)
        {
            if ((mailToEmployee_cb.SelectedItem != null || mailToCustomer_cb.SelectedItem != null) && subject_tb.Text != "" && wTresc.Text != "" && !edit)
            {
                Mail temp = new Mail(company.employees[company.employees.IndexOf((Employee)mailToEmployee_cb.SelectedItem)].firstNameAndLastName, logged.firstNameAndLastName, subject_tb.Text, wTresc.Text);
                temp.Send((Person)(mailToEmployee_cb.SelectedItem));
                temp.Send((Person)(logged));
                CollectionViewSource.GetDefaultView(logged.messages).Refresh();
                Company.SerializeToXml(company);
                this.Close();
            }
            else if (subject_tb.Text != "" && wTresc.Text != "" && edit)
            {
                Mail temp = new Mail(reply_tb.Text, logged.firstNameAndLastName, subject_tb.Text, wTresc.Text);

                foreach (Employee i in company.employees)
                {
                    if (i.firstNameAndLastName == logged.messages[index].from)
                    {
                        temp.Send((Person)(i));
                    }
                }
                temp.Send((Person)(logged));
                CollectionViewSource.GetDefaultView(logged.messages).Refresh();
                Company.SerializeToXml(company);
                this.Close();
            }
            else
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
            }
        }
    }
}
