using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using NativeMedia;

using Auctions.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Auctions.Views
{
    public partial class RegistrationPage : ContentPage
    {
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DataNasterii { get; set; }

        private readonly HttpClient client = new HttpClient();
        private const string Url = "https://myapiauct.azurewebsites.net/api/users";

        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public bool PassLenValid(string pass)
        {
            bool valid = true;
            if (pass.Length < 8) valid = false;
            return valid;
        }

        public bool PassMajValid(string pass)
        {
            bool valid = pass.Any(c => char.IsUpper(c));
            return valid;
        }

        public bool PassSymbValid(string pass)
        {
            bool valid = pass.Any(c => char.IsSymbol(c) || char.IsPunctuation(c));
            return valid;
        }
        public bool PassNrValid(string pass)
        {
            bool valid = pass.Any(c => char.IsNumber(c));
            return valid;
        }
        public bool EmailValid(string email)
        {
            bool valid = true;
            Regex emailRegex = new Regex(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
            if (!emailRegex.IsMatch(email)) valid = false;
            return valid;
        }

        async void OnRegister(object sender, EventArgs e)
        {
            if (!PassLenValid(this.Password))
                await DisplayAlert("ERROR", "Password must have at least 8 characters", "ok");
            else if(!PassMajValid(this.Password))
                await DisplayAlert("ERROR", "Password must have at least 1 uppercase", "ok");
            else if(!PassSymbValid(this.Password))
                await DisplayAlert("ERROR", "Password must have at least 1 symbol", "ok");
            else if(!PassNrValid(this.Password))
                await DisplayAlert("ERROR", "Password must have at least 1 number", "ok");
            else if (!EmailValid(this.Email))
                await DisplayAlert("ERROR", "Email does not have the correct format", "ok");
            else
            {
                User = new User
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    UserName = this.UserName,
                    Email = this.Email,
                    Password = this.Password,
                    DataNasterii = this.DataNasterii
                };
                var serial = JsonConvert.SerializeObject(User);
                StringContent content = new StringContent(serial, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(Url, content);
                await Navigation.PopModalAsync();
            } 
            
        }
        async void OnBack(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
