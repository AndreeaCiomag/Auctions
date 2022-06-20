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
using System.Collections.ObjectModel;
using System.IO;
using Plugin.Media.Abstractions;
using Plugin.Media;

namespace Auctions.Views
{
    public partial class NewItemPage : ContentPage
    {

        public Item Item { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Owner { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public DateTime DateFin { get; set; }

        private readonly HttpClient _client = new HttpClient();
        private const string Url = "https://myapiauct.azurewebsites.net/api/items";

        public User currentUser { get; set; }
        private const string Url2 = "https://myapiauct.azurewebsites.net/api/users";

        public NewItemPage(int idc, string uname)
        {
            currentUser = new User
            {
                Id = idc,
                UserName = uname
            };
            InitializeComponent();
            BindingContext = this;
        }
        public string img;

        async void OnBrowse(object sender, EventArgs e)
        {
            var image = await MediaPicker.PickPhotoAsync();
            if (image != null)
            {
                var newFile = Path.Combine("/Users/andreeaciomag/NewFolder", image.FileName);

                using (var stream = await image.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                img = image.FileName;
            }
        }

        //event handler pentru butonul de Save
        async void OnSave(object sender, EventArgs e)
        {
            Item = new Item
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Owner = currentUser.UserName,
                Image = img,
                Category = this.Category,
                Date = DateTime.UtcNow,
                DateFin = this.DateFin
            };
            var serial = JsonConvert.SerializeObject(Item);
            StringContent content = new StringContent(serial, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(Url, content);
            await Navigation.PopModalAsync();
        }
        
        //event handler pentru butonul de Cancel
        async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
