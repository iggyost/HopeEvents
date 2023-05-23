using HopeEvents.Model;
using HopeEvents.View.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HopeEvents.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Users _enteredUser;
        public MainWindow()
        {
            InitializeComponent();
            usernameTbl.Text = App.enteredUser.login;
        }

        bool inDrag = false;
        Point anchorPoint;

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void ResizeBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    Width = 1600;
                    Height = 900;
                    break;
                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    break;
            }
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

        private void HolidaysRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            HolidaysPage holidaysPage = new HolidaysPage();
            pagesFrm.Navigate(holidaysPage);
        }

        private void MuseumsRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            MuseumsPage museums = new MuseumsPage();
            pagesFrm.Navigate(museums);
        }

        private void ExcursionsRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            ExcursionsPage excursionsPage = new ExcursionsPage();
            pagesFrm.Navigate(excursionsPage);
        }

        private void ConcertsRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            ConcertsPage concerts = new ConcertsPage();
            pagesFrm.Navigate(concerts);

        }

        private void TheatresRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            TheatresPage theatres = new TheatresPage();
            pagesFrm.Navigate(theatres);

        }
        private void ContactsRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            ContactsPage contactsPage = new ContactsPage();
            pagesFrm.Navigate(contactsPage);
        }

        private void pagesFrm_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (pagesFrm.IsLoaded == true)
            {
                var ta = new ThicknessAnimation();
                ta.Duration = TimeSpan.FromSeconds(0.5);
                ta.DecelerationRatio = 0.8;
                ta.To = new Thickness(0, 0, 0, 0);
                if (e.NavigationMode == NavigationMode.New)
                {
                    ta.From = new Thickness(0, 0, 1920, 0);
                }
                else if (e.NavigationMode == NavigationMode.Back)
                {
                    ta.From = new Thickness(0, 0, 0, 1920);
                }
                 (e.Content as Page).BeginAnimation(MarginProperty, ta);
            }
            else
            {
                TimeSpan timeSpan = new TimeSpan(0, 0, 3);
                if (timeSpan.TotalSeconds == 0)
                {
                    var ta = new ThicknessAnimation();
                    ta.Duration = TimeSpan.FromSeconds(0.5);
                    ta.DecelerationRatio = 0.8;
                    ta.To = new Thickness(0, 0, 0, 0);
                    if (e.NavigationMode == NavigationMode.New)
                    {
                        ta.From = new Thickness(0, 0, 1920, 0);
                    }
                    else if (e.NavigationMode == NavigationMode.Back)
                    {
                        ta.From = new Thickness(0, 0, 0, 1920);
                    }
                 (e.Content as Page).BeginAnimation(MarginProperty, ta);
                }
            }
        }

        bool stateCategories = true;
        private GridLength fullState;
        private GridLength minState;
        private void CategoriesShowBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            fullState = new GridLength(320);
            minState = new GridLength(88);
            if (stateCategories == true)
            {
                CategoriesDfnt.Width = minState;
                stateCategories = false;
                CategoriesTbl.Visibility = Visibility.Collapsed;
                CategoriesShowBtn.Margin = new Thickness(32, 12, 0, 12);
            }
            else
            {
                CategoriesDfnt.Width = fullState;
                stateCategories = true;
                CategoriesTbl.Visibility = Visibility.Visible;
                CategoriesShowBtn.Margin = new Thickness(96, 12, 0, 12);
            }
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }

        private void pagesFrm_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
