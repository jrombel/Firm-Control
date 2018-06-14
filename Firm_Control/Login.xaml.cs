using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Security.Cryptography;
using MahApps.Metro.Controls;

namespace Firm_Control
{
    public partial class Login : MetroWindow
    {
        bool firstRun;

        public Login(string odczyt)
        {
            InitializeComponent();

            if (odczyt == "empty")
            {
                notification_l.Content = "Utwórz konto administratora";
                firstRun = true;
            }
            else
            {
                firstRun = false;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Company company = new Company();
            if (firstRun)
            {
                if (login_tb.Text == "")
                {
                    MessageBox.Show("Pole Login jest puste!", "Firm Control", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if ((password_pb.Password).Length < 8)
                {
                    MessageBox.Show("Zbyt krótkie hasło!", "Firm Control", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Konto zostało utworzone!", "Firm Control", MessageBoxButton.OK, MessageBoxImage.Information);

                    company.positions.Add("Szef");
                    Employee admin = new Employee(login_tb.Text, Login.Encrypt(password_pb.Password), login_tb.Text, "", "", company.positions[0], null);
                    (company.employees).Add(admin);

                    Company.SerializeToXml(company);

                    (new MainWindow(company, admin)).Show();
                    this.Close();
                }
            }
            else
            {
                company = Company.DeserializeFromXml();
                bool logged = false;
                bool exists = false;
                foreach (Employee i in company.employees)
                {
                    if (login_tb.Text == i.login && password_pb.Password != Login.Decrypt(i.password))
                    {
                        exists = true;
                    }
                    else if (login_tb.Text == i.login && password_pb.Password == Login.Decrypt(i.password))
                    {
                        company.Update();
                        (new MainWindow(company, i)).Show();
                        logged = true;
                        this.Close();
                        break;
                    }
                }
                if (!logged)
                {
                    if (exists)
                    {
                        MessageBox.Show("Hasło nieprawidłowe!", "Firm Control", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Brak takiego użytkownika w bazie!", "Firm Control", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        public static string Encrypt(string input)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(input)));
        }

        private static byte[] Encrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("hjiweykaksd", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }

        public static string Decrypt(string input)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(input)));
        }

        private static byte[] Decrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("hjiweykaksd", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
    }
}
