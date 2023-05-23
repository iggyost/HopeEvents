using HopeEvents.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HopeEvents
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HopeDBEntities context = new HopeDBEntities();
        public static Users enteredUser = new Users();
        public static int categories;
        public static Events currentEvent;
    }
}
