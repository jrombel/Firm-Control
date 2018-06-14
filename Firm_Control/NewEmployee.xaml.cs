using System;
using System.Windows;
using System.Windows.Data;

using MahApps.Metro.Controls;

namespace Firm_Control
{
    public partial class NewEmployee : MetroWindow
    {
        Company company;
        bool edit;
        int index;
        public NewEmployee()
        { }

        public NewEmployee(ref Company tmp)
        {
            InitializeComponent();
            company = tmp;
            edit = false;
            positions_cb.ItemsSource = company.positions;
            newEmployee_btn.Content = "Dodaj";
            Wyb2.Visibility = Visibility.Collapsed;
            choosePosition_sp.Visibility = Visibility.Visible;
        }

        public NewEmployee(ref Company tmp, int index)
        {
            InitializeComponent();
            company = tmp;
            edit = true;
            this.index = index;
            positions_cb.ItemsSource = company.positions;
            newEmployee_btn.Content = "Edytuj";
            Wyb2.Visibility = Visibility.Collapsed;
            choosePosition_sp.Visibility = Visibility.Visible;

            login_tb.Text = (company.employees)[index].login;
            password_pb.Password = (company.employees)[index].password;
            firstName_tb.Text = (company.employees)[index].firstName;
            lastName_tb.Text = (company.employees)[index].lastName;
            mail_tb.Text = (company.employees)[index].email;
            positions_cb.SelectedIndex = company.positions.IndexOf((company.employees)[index].position);
            salary_tb.Text = ((company.employees)[index].salary).ToString();
            phone_tb.Text = ((company.employees)[index].phone).ToString();
        }

        private void newEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (login_tb.Text != "" && password_pb.Password != "" && firstName_tb.Text != "" && lastName_tb.Text != "" && mail_tb.Text != "" &&
              ((newPositions_tb.Text != " ")) && mail_tb.Text != "" && !edit)
            {
                Employee temp;
                if (fromListPosition_rb.IsChecked == true)
                {
                    if (salary_tb.Text != "")
                    {
                        temp = new Employee(login_tb.Text, Login.Encrypt(password_pb.Password), firstName_tb.Text, lastName_tb.Text, mail_tb.Text, (String)positions_cb.SelectedItem, Double.Parse(salary_tb.Text));
                    }
                    else
                    {
                        temp = new Employee(login_tb.Text, Login.Encrypt(password_pb.Password), firstName_tb.Text, lastName_tb.Text, mail_tb.Text, (String)positions_cb.SelectedItem, null);
                    }

                }
                else
                {
                    if (salary_tb.Text != "")
                    {
                        temp = new Employee(login_tb.Text, Login.Encrypt(password_pb.Password), firstName_tb.Text, lastName_tb.Text, mail_tb.Text, newPositions_tb.Text, Double.Parse(salary_tb.Text));
                    }
                    else
                    {
                        temp = new Employee(login_tb.Text, Login.Encrypt(password_pb.Password), firstName_tb.Text, lastName_tb.Text, mail_tb.Text, newPositions_tb.Text, null);
                    }
                    if (addPosition_cb.IsChecked == true)
                    {
                        company.positions.Add(newPositions_tb.Text);
                    }
                }
                company.employees.Add(temp);
                Company.SerializeToXml(company);
                this.Close();
            }
            else if (login_tb.Text != "" && password_pb.Password != "" && firstName_tb.Text != "" && lastName_tb.Text != "" && mail_tb.Text != "" &&
                ((newPositions_tb.Text != " ")) && mail_tb.Text != "" && edit)
            {
                (company.employees)[index].login = login_tb.Text;
                (company.employees)[index].password = password_pb.Password;
                (company.employees)[index].firstName = firstName_tb.Text;
                (company.employees)[index].lastName = lastName_tb.Text;
                (company.employees)[index].email = mail_tb.Text;
                (company.employees)[index].position = (String)positions_cb.SelectedItem;
                if (salary_tb.Text != "")
                {
                    (company.employees)[index].salary = Double.Parse(salary_tb.Text);
                }
                if (phone_tb.Text != "0")
                {
                    (company.employees)[index].phone = Int32.Parse(phone_tb.Text);
                }

                CollectionViewSource.GetDefaultView(company.employees).Refresh();
                Company.SerializeToXml(company);
                this.Close();
            }
            else
            {
                MessageBox.Show("Uzupełnij wymagane pola oznaczone gwiazdką *");
            }
        }

        private void fromListPosition_Checked(object sender, RoutedEventArgs e)
        {
            Wyb2.Visibility = Visibility.Collapsed;
            choosePosition_sp.Visibility = Visibility.Visible;
            addPosition_cb.Visibility = Visibility.Collapsed;
        }

        private void newPosition_Checked(object sender, RoutedEventArgs e)
        {
            choosePosition_sp.Visibility = Visibility.Collapsed;
            Wyb2.Visibility = Visibility.Visible;
            addPosition_cb.Visibility = Visibility.Visible;
        }
    }
}
