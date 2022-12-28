using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ЭКЗTHREE
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void inSign_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors =   new StringBuilder();
            using (var db = new testingUsersEntities())
            {
                var login = db.Users.AsNoTracking().FirstOrDefault(u => u.login == inputLogin.Text && u.login == inputLogin.Text);
                var pass = db.Users.AsNoTracking().FirstOrDefault(u => u.password == inputPassword.Password);

                if (inputLogin.Text.Length == 0)
                {
                    MessageBox.Show("Вы не ввели логин");
                }
                else
                {
                    if (inputPassword.Password.Length == 0)
                    {
                        MessageBox.Show("Вы неввели пароль");
                    }
                    else
                    {
                        if(pass == null)
                        {
                            MessageBox.Show("Не верный пароль");
                        }
                        else
                        {
                            if(pass.isAdmin == true)
                            {
                                string inpCod = inputCode.Text.ToString();
                                string code = RandomCode1.RC.ToString();

                                if(inpCod == code)
                                {
                                    Admin ad = new Admin();
                                    ad.Show();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Не верный код");
                                }
                            }

                            else if (pass.isAdmin == false)
                            {
                                string inputCod = inputCode.Text.ToString();
                                string rancode = RandomCode1.RC.ToString();

                                if (inputCod == rancode)
                                {
                                    NoAdmin no = new NoAdmin();
                                    no.Show();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Не верный код");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Code_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new testingUsersEntities())
            {
                var login = db.Users.AsNoTracking().FirstOrDefault(u => u.login == inputLogin.Text && u.login == inputLogin.Text);
                var pass = db.Users.AsNoTracking().FirstOrDefault(u => u.password == inputPassword.Password);

                if(login == null)
                {
                    MessageBox.Show("Пользователь не найден");
                    inputCode.IsEnabled = false;
                    inputCode.Clear();
                    inputPassword.IsEnabled = false;
                    inputPassword.Clear();
                }
                else
                {
                    if (pass == null)
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                    else if (pass != null)
                    {
                        RandomCode1 random = new RandomCode1();
                        random.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
            }
        }

        private void inputLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(inputLogin.Text.Length > 0)
                {
                    inputPassword.IsEnabled = true;
                    inputPassword.Focus();
                }
                else
                {
                    MessageBox.Show("Вы не ввели логин");
                }
            }
        }

        private void inputPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (inputPassword.Password.Length > 0)
                {
                    Code.IsEnabled = true;
                    inSign.IsEnabled = true;
                    inputCode.IsEnabled = true;
                    inputCode.Focus();
                }
                else
                {
                    MessageBox.Show("Вы не ввели пароль!");
                }
            }
        }
    }
}
