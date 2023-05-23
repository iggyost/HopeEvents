using HopeEvents.Model;
using HopeEvents.View.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace HopeEvents.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для TheatresPage.xaml
    /// </summary>
    public partial class TheatresPage : Page
    {
        public TheatresPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Disabilities> disabilities = new List<Disabilities>();
            disabilities = App.context.Disabilities.ToList();
            Disabilities allDisabilities = new Disabilities()
            {
                name = "Все категории",
            };
            disabilities.Add(allDisabilities);
            FilterCb.ItemsSource = disabilities.ToList();
            FilterCb.SelectedIndex = 5;
            if (App.enteredUser.id != 1)
            {
                AddEventButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                AddEventButton.Visibility = Visibility.Visible;
            }
            Events events = new Events();
            TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.category_id.Equals(5)).ToList();
            try
            {
                System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(events.image_of_event));
            }
            catch (Exception)
            {

            }
        }


        private void SearchFieldTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FilterCb.SelectedIndex == 5)
            {
                TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.category_id == 5 && ev.title.Contains(SearchFieldTb.Text) == true).ToList();
            }
            else
            {
                TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.disability_id == FilterCb.SelectedIndex + 1 && ev.category_id == 5 && ev.title.Contains(SearchFieldTb.Text) == true).ToList();
            }
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            App.categories = 5;
            AddEventWindow addEventWindow = new AddEventWindow();
            addEventWindow.ShowDialog();
            if (addEventWindow.DialogResult == true)
            {
                if (FilterCb.SelectedIndex != -1 && FilterCb.SelectedIndex != 5)
                {
                    TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.disability_id == FilterCb.SelectedIndex + 1 && ev.category_id == 5).ToList().Union(App.context.Events.Where(ev => ev.disability_id == 5 && ev.category_id == 5).ToList());
                }
                else if (FilterCb.SelectedIndex == 5)
                {
                    TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.category_id == 5).ToList();
                }
            }
        }

        private void EditEventBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Tag.ToString());
            var currentItem = App.context.Events.Where(ev => ev.id == index).FirstOrDefault();
            AddEventWindow editEventWindow = new AddEventWindow(currentItem);
            editEventWindow.ShowDialog();
            if (editEventWindow.DialogResult == true)
            {
                if (FilterCb.SelectedIndex != -1 && FilterCb.SelectedIndex != 5)
                {
                    TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.disability_id == FilterCb.SelectedIndex + 1 && ev.category_id == 5).ToList().Union(App.context.Events.Where(ev => ev.disability_id == 5 && ev.category_id == 5).ToList());
                }
                else if (FilterCb.SelectedIndex == 5)
                {
                    TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.category_id == 5).ToList();
                }
            }
        }

        private void DeleteEventBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Tag.ToString());
            var currentItem = App.context.Events.Where(ev => ev.id == index).FirstOrDefault();
            App.context.Events.Remove(currentItem);
            App.context.SaveChanges();
            TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.category_id.Equals(5)).ToList();
        }

        private void EditDeleteBtns_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int useridIndex = int.Parse((string)stackPanel.Tag.ToString());
            if (useridIndex != App.enteredUser.id)
            {
                stackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                stackPanel.Visibility = Visibility.Visible;
            }
        }

        private void FilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchFieldTb.Text != string.Empty)
            {
                if (FilterCb.SelectedIndex == 5)
                {
                    TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.category_id == 5 && ev.title.Contains(SearchFieldTb.Text) == true).ToList();
                }
                else
                {
                    TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.disability_id == FilterCb.SelectedIndex + 1 && ev.category_id == 5 && ev.title.Contains(SearchFieldTb.Text) == true).ToList();
                }
            }
            else
            {
                if (FilterCb.SelectedIndex != -1 && FilterCb.SelectedIndex != 5)
                {
                    TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.disability_id == FilterCb.SelectedIndex + 1 && ev.category_id == 5).ToList().Union(App.context.Events.Where(ev => ev.disability_id == 5 && ev.category_id == 5).ToList());
                }
                else if (FilterCb.SelectedIndex == 5)
                {
                    TheatresLv.ItemsSource = App.context.Events.Where(ev => ev.category_id == 5).ToList();
                }
            }
        }

        private void disabilityTbl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            int index = int.Parse(textBlock.Tag.ToString());
            var currentItem = App.context.Events.Where(ev => ev.id == index).FirstOrDefault();
            if (currentItem.id == index)
            {
                textBlock.Text = App.context.Disabilities.Where(d => d.id == currentItem.disability_id).Select(d => d.name).FirstOrDefault();
            }
        }
    }
}
