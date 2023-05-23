using HopeEvents.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        public AddEventWindow()
        {
            InitializeComponent();
            EditImageBtn.Visibility = Visibility.Collapsed;
            AddImageBtn.Visibility = Visibility.Visible;
            EditEventBtn.Visibility = Visibility.Collapsed;
            AddEventBtn.Visibility = Visibility.Visible;
            DisabilitesCb.ItemsSource = App.context.Disabilities.ToList();
            DisabilitesCb.SelectedItem = App.context.Disabilities.FirstOrDefault();
        }
        public AddEventWindow(Events currentItem)
        {
            App.currentEvent = currentItem;
            InitializeComponent();
            AddEventBtn.Visibility = Visibility.Collapsed;
            AddImageBtn.Visibility = Visibility.Collapsed;
            EditEventBtn.Visibility = Visibility.Visible;
            if (App.currentEvent.image_of_event != null)
            {
                byte[] image_bytes_context = currentItem.image_of_event.ToArray();
                MemoryStream byteStream = new MemoryStream(image_bytes_context);
                BitmapImage imageEdit = new BitmapImage();
                imageEdit.BeginInit();
                imageEdit.StreamSource = byteStream;
                imageEdit.EndInit();
                newImage.Source = imageEdit;
            }
            else
            {

            }
            AddDateOfEventDP.SelectedDate = currentItem.date_of_event;
            AddDescriptionTb.Text = currentItem.description;
            AddTitleTb.Text = currentItem.title;
            AddCityTb.Text = currentItem.city;
            AddMetroTb.Text = currentItem.metro;
            AddStreetTb.Text = currentItem.street;
            DisabilitesCb.ItemsSource = App.context.Disabilities.ToList();
            DisabilitesCb.SelectedIndex = (int)(currentItem.disability_id - 1);
        }
        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
        private void EditEventBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (totalValidateCity == true &&
                    totalValidateMetro == true &&
                    totalValidateStreet == true &&
                    isValidatedTitle1 == true &&
                    isValidatedDescription1 == true)
                {
                    App.currentEvent.date_of_event = (DateTime)AddDateOfEventDP.SelectedDate;
                    App.currentEvent.description = AddDescriptionTb.Text;
                    App.currentEvent.title = AddTitleTb.Text;
                    App.currentEvent.city = AddCityTb.Text;
                    App.currentEvent.metro = AddMetroTb.Text;
                    App.currentEvent.street = AddStreetTb.Text;
                    App.currentEvent.disability_id = DisabilitesCb.SelectedIndex + 1;
                    if (newImage.Source != null)
                    {
                        App.currentEvent.image_of_event = getJPGFromImageControl(newImage.Source as BitmapImage);
                    }
                    else
                    {

                    }
                    App.context.Entry(App.currentEvent).State = System.Data.EntityState.Modified;
                    App.context.SaveChanges();

                    MessageBox.Show("Мероприятие успешно изменено!");
                    App.context.Entry(App.currentEvent).State = System.Data.EntityState.Modified;
                    DialogResult = true;
                    this.Close();
                }
            }
            catch (Exception)
            {
                Exception exc = new Exception();
                MessageBox.Show("Не удалось изменить мероприятие" + "\\n" + exc);
            }
            //App.context.Entry().State = System.Data.EntityState.Modified;
        }
        private void EditImageBtn_Click(object sender, RoutedEventArgs e)
        {
            ofdPicture.Filter =
                "Image files|*.jpg|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
                newImage.Source =
                    new BitmapImage(new Uri(ofdPicture.FileName));
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        bool inDrag = false;
        Point anchorPoint;
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
        string validationCity1;
        string validationCity2;
        string validationCity3;
        string validationMetro1;
        string validationMetro2;
        string validationMetro3;
        string validationStreet1;
        string validationStreet2;

        string errorInCityMessage;
        string errorInMetroMessage;
        string errorInStreetMessage;

        bool isValidatedCity1;
        bool isValidatedCity2;
        bool isValidatedCity3;
        bool totalValidateCity = false;

        bool isValidatedMetro1;
        bool isValidatedMetro2;
        bool isValidatedMetro3;
        bool totalValidateMetro = false;

        bool isValidatedStreet1;
        bool isValidatedStreet2;
        bool totalValidateStreet = false;

        bool isValidatedTitle1 = false;

        bool isValidatedDescription1 = false;
        private void AddTitleTb_TextChanged(object sender, TextChangedEventArgs e)
        {

            string validationTitle1;

            if (AddTitleTb.Text.Length < 2 || AddTitleTb.Text.Length > 64)
            {
                validationTitle1 = "В названии должно быть не менее 2 и не более 64 символов! ";
                isValidatedTitle1 = false;
            }
            else
            {
                validationTitle1 = string.Empty;
                isValidatedTitle1 = true;
            }
            ValidationTitleTbl.Text = validationTitle1;
        }

        private void AddDescriptionTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string validationDescription1;

            if (AddDescriptionTb.Text.Length < 10 || AddDescriptionTb.Text.Length > 1000)
            {
                validationDescription1 = "В описании должно быть не менее 10 и не более 1000 символов! ";
                isValidatedDescription1 = false;
            }
            else
            {
                validationDescription1 = string.Empty;
                isValidatedDescription1 = true;
            }
            validationDescriptionTbl.Text = validationDescription1;
        }

        private void AddCityTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasLatin = new Regex(@"\p{IsBasicLatin}+");

            if (hasLatin.IsMatch(AddCityTb.Text))
            {
                validationCity1 = "Только кириллица! ";
                isValidatedCity1 = false;
            }
            else
            {
                validationCity1 = string.Empty;
                isValidatedCity1 = true;
            }
            if (AddCityTb.Text.Length < 2 || AddCityTb.Text.Length > 21)
            {
                validationCity2 = "От 2 до 21 символов в названии города! ";
                isValidatedCity2 = false;
            }
            else
            {
                validationCity2 = string.Empty;
                isValidatedCity2 = true;
            }
            if (hasNumber.IsMatch(AddCityTb.Text))
            {
                validationCity3 = "Без цифр! ";
                isValidatedCity3 = false;
            }
            else
            {
                validationCity3 = string.Empty;
                isValidatedCity3 = true;
            }
            if (isValidatedCity1 == false || isValidatedCity2 == false || isValidatedCity3 == false)
            {
                errorInCityMessage = "Ошибка в поле 'Город': ";
                totalValidateCity = false;
            }
            else
            {
                errorInCityMessage = string.Empty;
                totalValidateCity = true;
            }
            validationLocationTbl.Text = errorInCityMessage + validationCity1 + validationCity2 + validationCity3 +
                                         errorInMetroMessage + validationMetro1 + validationMetro2 + validationMetro3 +
                                         errorInStreetMessage + validationStreet1 + validationStreet2;
        }

        private void AddMetroTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasLatin = new Regex(@"\p{IsBasicLatin}+");

            if (hasLatin.IsMatch(AddMetroTb.Text))
            {
                validationMetro1 = "Только кириллица! ";
                isValidatedMetro1 = false;
            }
            else
            {
                validationMetro1 = string.Empty;
                isValidatedMetro1 = true;
            }
            if (AddMetroTb.Text.Length < 2 || AddMetroTb.Text.Length > 30)
            {
                validationMetro2 = "От 2 до 30 символов в названии метро! ";
                isValidatedMetro2 = false;
            }
            else
            {
                validationMetro2 = string.Empty;
                isValidatedMetro2 = true;
            }
            if (hasNumber.IsMatch(AddMetroTb.Text))
            {
                validationMetro3 = "Без цифр! ";
                isValidatedMetro3 = false;
            }
            else
            {
                validationMetro3 = string.Empty;
                isValidatedMetro3 = true;
            }
            if (isValidatedMetro1 == false || isValidatedMetro2 == false || isValidatedMetro3 == false)
            {
                errorInMetroMessage = "Ошибка в поле 'Метро': ";
                totalValidateMetro = false;
            }
            else
            {
                errorInMetroMessage = string.Empty;
                totalValidateMetro = true;
            }
            validationLocationTbl.Text = errorInCityMessage + validationCity1 + validationCity2 + validationCity3 +
                                         errorInMetroMessage + validationMetro1 + validationMetro2 + validationMetro3 +
                                         errorInStreetMessage + validationStreet1 + validationStreet2;
        }

        private void AddStreetTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hasCyrillic = new Regex(@"\p{IsCyrillic}+");

            if (!hasCyrillic.IsMatch(AddStreetTb.Text))
            {
                validationStreet1 = "Только кириллица! ";
                isValidatedStreet1 = false;
            }
            else
            {
                validationStreet1 = string.Empty;
                isValidatedStreet1 = true;
            }
            if (AddStreetTb.Text.Length < 2 || AddStreetTb.Text.Length > 50)
            {
                validationStreet2 = "От 2 до 50 символов в названии улицы! ";
                isValidatedStreet2 = false;
            }
            else
            {
                validationStreet2 = string.Empty;
                isValidatedStreet2 = true;
            }
            if (isValidatedStreet1 == false || isValidatedStreet2 == false)
            {
                errorInStreetMessage = "Ошибка в поле 'Улица': ";
                totalValidateStreet = false;
            }
            else
            {
                errorInStreetMessage = string.Empty;
                totalValidateStreet = true;
            }
            validationLocationTbl.Text = errorInCityMessage + validationCity1 + validationCity2 + validationCity3 +
                                         errorInMetroMessage + validationMetro1 + validationMetro2 + validationMetro3 +
                                         errorInStreetMessage + validationStreet1 + validationStreet2;
        }

        byte[] image_bytes;
        OpenFileDialog ofdPicture = new OpenFileDialog();

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            ofdPicture.Filter =
                "Image files|*.jpg|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
            {
                newImage.Source = new BitmapImage(new Uri(ofdPicture.FileName));
                image_bytes = File.ReadAllBytes(ofdPicture.FileName);
            }
        }
        private void AddEventBtn_Click(object sender, RoutedEventArgs e)
        {
            if (totalValidateCity == true &&
                totalValidateMetro == true &&
                totalValidateStreet == true &&
                isValidatedTitle1 == true &&
                isValidatedDescription1 == true)
            {
                Events events = new Events()
                {
                    title = AddTitleTb.Text,
                    description = AddDescriptionTb.Text,
                    city = AddCityTb.Text,
                    metro = AddMetroTb.Text,
                    street = AddStreetTb.Text,
                    date_of_event = (DateTime)AddDateOfEventDP.SelectedDate,
                    date_of_publication = DateTime.Now,
                    image_of_event = image_bytes,
                    category_id = App.categories,
                    user_id = App.enteredUser.id,
                    disability_id = DisabilitesCb.SelectedIndex + 1
                };
                App.context.Events.Add(events);
                App.context.SaveChanges();
                MessageBox.Show("Мероприятие успешно добавлено!");
                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены верно!");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
