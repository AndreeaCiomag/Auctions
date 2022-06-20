using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Auctions.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auctions.Views
{
    public partial class AboutPage : ContentPage
    {
        public User currentUser { get; set; }

        private readonly HttpClient client = new HttpClient();
        private const string Url = "https://myapiauct.azurewebsites.net/api/users";

        private ObservableCollection<User> user;

        public AboutPage(int id)
        {
            currentUser = new User
            {
                Id = id
            };
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            
            string response = await client.GetStringAsync(Url);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(response);
            user = new ObservableCollection<User>(users);

            foreach(User u in user)
            {
                if(u.Id == currentUser.Id)
                {
                    currentUser.FirstName = u.FirstName;
                    currentUser.LastName = u.LastName;
                    currentUser.UserName = u.UserName;
                    currentUser.Email = u.Email;
                    currentUser.Password = u.Password;
                    currentUser.DataNasterii = u.DataNasterii;
                }
            }
            fname_ent.Text = currentUser.FirstName;
            lname_ent.Text = currentUser.LastName;
            uname_ent.Text = currentUser.UserName;
            base.OnAppearing();
        }
        async void OnLogout(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }
        async void OnBack(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        void OnEdit(object sender, EventArgs e)
        {
            save_btn.IsVisible = true;
            fname_ent.IsReadOnly = false;
            lname_ent.IsReadOnly = false;
            uname_ent.IsReadOnly = false;
        }
        async void OnSave(object sender, EventArgs e)
        {
            currentUser = new User
            {
                Id = currentUser.Id,
                FirstName = fname_ent.Text,
                LastName = lname_ent.Text,
                UserName = uname_ent.Text,
                Email = currentUser.Email,
                Password = currentUser.Password,
                DataNasterii = currentUser.DataNasterii
            };
            var serial = JsonConvert.SerializeObject(currentUser);
            StringContent content = new StringContent(serial, Encoding.UTF8, "application/json");
            var result = await client.PutAsync($"{Url}/{currentUser.Id}", content);
            save_btn.IsVisible = false;
            fname_ent.IsReadOnly = true;
            lname_ent.IsReadOnly = true;
            uname_ent.IsReadOnly = true;
        }
        async void OnDeleteAcc(object sender, EventArgs e)
        {
            var id = currentUser.Id;
            int i = int.Parse(id.ToString());
            var response = await client.DeleteAsync($"{Url}/{i}");
            await Navigation.PopToRootAsync();
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }
    }
}
