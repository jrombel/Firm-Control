using System.Windows;
using System.Windows.Data;
using MahApps.Metro.Controls;

namespace Firm_Control
{
    public partial class NewCustomer : MetroWindow
    {
        Company company;
        bool edit;
        int index;
        public NewCustomer()
        { }
        public NewCustomer(ref Company tmp)
        {
            InitializeComponent();
            company = tmp;
            edit = false;
            newCustomer_btn.Content = "Dodaj";
        }
        public NewCustomer(ref Company tmp, int index)
        {
            edit = true;
            this.index = index;
            newCustomer_btn.Content = "Edytuj";

            firstName_tb.Text = (company.customers)[index].firstName;
            lastName_tb.Text = (company.customers)[index].lastName;
            mail_tb.Text = (company.customers)[index].email;
        }

        private void NewCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (firstName_tb.Text != "" && lastName_tb.Text != "" && mail_tb.Text != "" && !edit)
            {
                company.customers.Add(new Customer(firstName_tb.Text, lastName_tb.Text, mail_tb.Text));
                Company.SerializeToXml(company);
                this.Close();
            }
            else if (firstName_tb.Text != "" && lastName_tb.Text != "" && mail_tb.Text != "" && edit)
            {
                (company.customers)[index].firstName = firstName_tb.Text;
                (company.customers)[index].lastName = lastName_tb.Text;
                (company.customers)[index].email = mail_tb.Text;
                CollectionViewSource.GetDefaultView(company.tasks).Refresh();
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