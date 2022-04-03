using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Auctions.Views;
using Auctions.Data;
using System.IO;

namespace Auctions
{
    public partial class App : Application
    {
        static ItemsData database;

        //calea spre fisierul de tip baza de date
        public static ItemsData Database
        {
            get
            {
                if (database == null)
                {
                    database = new ItemsData(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Items.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ItemsPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
