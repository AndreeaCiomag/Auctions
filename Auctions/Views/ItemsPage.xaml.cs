using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Auctions.Models;
using Auctions.Views;
using SQLite;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Auctions.Views
{
    public partial class ItemsPage : ContentPage
    {
        public User currentUser { get; set; }
        private readonly HttpClient _client = new HttpClient();
        private const string Url2 = "https://myapiauct.azurewebsites.net/api/users";

        public ItemsPage(int idc, string uname)
        {
            currentUser = new User
            {
                Id = idc,
                UserName = uname
            };
            InitializeComponent();
        }

        private readonly HttpClient client = new HttpClient();
        private const string Url = "https://myapiauct.azurewebsites.net/api/items";
        private ObservableCollection<Item> item;

        protected override async void OnAppearing()
        { 
            string response = await client.GetStringAsync(Url);
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(response);
            item = new ObservableCollection<Item>(items);
            itemsListView.ItemsSource = item;
            base.OnAppearing();
        }
        
        //event handler pentru apasarea ToolbarItem; trimitere la adaugarea unui produs
        async void OnAddItem(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage(currentUser.Id, currentUser.UserName)));
        }
        async void OnAbout(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AboutPage(currentUser.Id)));
        }
        
        //event handler pentru selectarea unui produs din lista; trimitere la pagina cu detaliile produsului
        async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as Item;
            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(selected.Id, selected.Name, selected.Description, selected.Price, selected.Owner, selected.Image, selected.Category, selected.DateFin, currentUser.Id)));
            ((ListView)sender).SelectedItem = null;
        }
        
        //event handler pentru bara de cautare
        async void OnChange(object sender, TextChangedEventArgs e)
        {
            string response = await client.GetStringAsync(Url);
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(response);
            item = new ObservableCollection<Item>(items);

            itemsListView.ItemsSource = item.Where(i => i.Name.Contains(e.NewTextValue));
        }

        
        async void OnCatgSelect(object sender, EventArgs e)
        {
            var pick = (Picker)sender;
            string catg = pick.SelectedItem.ToString();

            string response = await _client.GetStringAsync(Url);
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(response);
            item = new ObservableCollection<Item>(items);

            if (catg.Equals("All"))
            {
                itemsListView.ItemsSource = item;
            }
            else
            {
                itemsListView.ItemsSource = item.Where(i => i.Category.Contains(catg));
            }
        }
        
    }
}
