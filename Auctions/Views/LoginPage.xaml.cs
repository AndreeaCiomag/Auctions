using System.ComponentModel;
using Xamarin.Forms;
using Auctions.Models;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Auctions.Views
{
    public partial class LoginPage : ContentPage
    {
        public User User { get; set; }
        private readonly HttpClient client = new HttpClient();
        private const string Url = "https://myapiauct.azurewebsites.net/api/users";
        private ObservableCollection<User> user;

        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnLogIn(object sender, EventArgs e)
        {
            string response = await client.GetStringAsync(Url);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(response);
            user = new ObservableCollection<User>(users);
            bool ok = false;
            foreach(User u in user)
            {
                if(u.UserName.Equals(UserN.Text.ToString()) && u.Password.Equals(UserP.Text.ToString()))
                {
                    ok = true;
                    await Navigation.PushModalAsync(new NavigationPage(new ItemsPage(u.Id, u.UserName)));
                }
            }
            if (ok == false) await DisplayAlert("ERROR", "No user found", "ok");
        }
        async void OnNew(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RegistrationPage()));
        }
    }
}
