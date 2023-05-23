using HopeEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HopeEvents.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        bool inDrag = false;
        Point anchorPoint;

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }


        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            anchorPoint = PointToScreen(e.GetPosition(this));
            inDrag = true;
            CaptureMouse();
            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (inDrag)
            {
                Point currentPoint = PointToScreen(e.GetPosition(this));
                this.Left = this.Left + currentPoint.X - anchorPoint.X;
                this.Top = this.Top + currentPoint.Y - anchorPoint.Y;
                anchorPoint = currentPoint;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (inDrag)
            {
                ReleaseMouseCapture();
                inDrag = false;
                e.Handled = true;
            }
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            LogOrRegGrid.Visibility = Visibility.Collapsed;
            LogGrid.Visibility = Visibility.Visible;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            LogOrRegGrid.Visibility = Visibility.Visible;
            LogGrid.Visibility = Visibility.Collapsed;
            RegGrid.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = App.context.Users.SingleOrDefault(u => u.login == LoginField.Text && u.password == PasswordField.Password);
            if (user != null)
            {
                App.enteredUser = user;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            LogOrRegGrid.Visibility = Visibility.Collapsed;
            RegGrid.Visibility = Visibility.Visible;
        }
        bool conditionLogin1;
        bool conditionLogin2;
        bool conditionLogin3;
        bool conditionLogin4;
        bool conditionPassword1;
        bool conditionPassword2;
        bool conditionPassword3;
        bool conditionPassword4;
        bool conditionComplete1;
        private void RegLoginField_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            var hasCyrillic = new Regex(@"\p{IsCyrillic}+");
            var hasWhiteSpace = new Regex(@"\s+");
            string validationMsg1;
            string validationMsg2;
            string validationMsg3;
            string validationMsg4;


            if (RegLoginField.Text.Length < 4 || RegLoginField.Text.Length > 21)
            {
                validationMsg1 = "Не менее 4 символов и не более 21! ";
                conditionLogin1 = false;
            }
            else
            {
                validationMsg1 = string.Empty;
                conditionLogin1 = true;
            }

            if (hasSymbols.IsMatch(RegLoginField.Text))
            {
                validationMsg2 = "Логин не должен содержать спецсимволы! ";
                conditionLogin2 = false;
            }
            else
            {
                validationMsg2 = string.Empty;
                conditionLogin2 = true;
            }

            if (hasCyrillic.IsMatch(RegLoginField.Text))
            {
                validationMsg3 = "Логин не должен содержать кириллицу! ";
                conditionLogin3 = false;
            }
            else
            {
                validationMsg3 = string.Empty;
                conditionLogin3 = true;
            }

            if (hasWhiteSpace.IsMatch(RegLoginField.Text))
            {
                validationMsg4 = "Без пробелов! ";
                conditionLogin4 = false;
            }
            else
            {
                validationMsg4 = string.Empty;
                conditionLogin4 = true;
            }
            validationRegLogin.Text = validationMsg1 + validationMsg2 + validationMsg3 + validationMsg4;
        }

        private void RegPasswordField_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasCyrillic = new Regex(@"\p{IsCyrillic}+");
            var hasWhiteSpace = new Regex(@"\s+");
            string validationMsg1;
            string validationMsg2;
            string validationMsg3;
            string validationMsg4;

            if (RegPasswordField.Password.Length < 4 || RegPasswordField.Password.Length > 21)
            {
                validationMsg1 = "Не менее 4 символов и не более 21! ";
                conditionPassword1 = false;
            }
            else
            {
                validationMsg1 = string.Empty;
                conditionPassword1 = true;
            }

            if (!hasNumber.IsMatch(RegPasswordField.Password))
            {
                validationMsg2 = "Пароль должен содержать хотя бы 1 цифру! ";
                conditionPassword2 = false;
            }

            else
            {
                validationMsg2 = string.Empty;
                conditionPassword2 = true;
            }
            if (hasCyrillic.IsMatch(RegPasswordField.Password))
            {
                validationMsg3 = "Пароль не должен содержать кириллицу! ";
                conditionPassword3 = false;
            }

            else
            {
                validationMsg3 = string.Empty;
                conditionPassword3 = true;
            }

            if (hasWhiteSpace.IsMatch(RegPasswordField.Password))
            {
                validationMsg4 = "Без пробелов! ";
                conditionPassword4 = false;
            }
            else
            {
                validationMsg4 = string.Empty;
                conditionPassword4 = true;
            }
            validationRegPassword.Text = validationMsg1 + validationMsg2 + validationMsg3 + validationMsg4;
        }

        private void RegCompletePasswordField_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string validationMsg1;
            if (RegPasswordField.Password != RegCompletePasswordField.Password)
            {
                validationMsg1 = "Пароли не совпадают! ";
                conditionComplete1 = false;
            }
            else
            {
                validationMsg1 = string.Empty;
                conditionComplete1 = true;
            }
            validationMatchPassword.Text = validationMsg1;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (conditionLogin1 == true &&
                conditionLogin2 == true &&
                conditionLogin3 == true &&
                conditionLogin4 == true &&
                conditionPassword1 == true &&
                conditionPassword2 == true &&
                conditionPassword3 == true &&
                conditionPassword4 == true &&
                conditionComplete1 == true)
            {
                Users user = new Users()
                {
                    login = RegLoginField.Text,
                    password = RegPasswordField.Password
                };
                var userEmpty = App.context.Users.SingleOrDefault(u => u.login == RegLoginField.Text);
                if (userEmpty == null)
                {
                    App.context.Users.Add(user);
                    App.context.SaveChanges();

                    MessageBox.Show("Регистрация прошла успешно");
                    RegGrid.Visibility = Visibility.Collapsed;
                    LogGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Пользователь с таким именем уже существует!");
                }
            }
            else
            {
                MessageBox.Show("Неправильно введены данные!");
            }
        }
    }
}
