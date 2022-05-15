using System.ComponentModel;
using Xamarin.Forms;
using Auctions.Models;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Auctions.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        private readonly HttpClient _client = new HttpClient();

        public Item Item { get; set; }
        private const string Url = "https://myapiauct.azurewebsites.net/api/items";

        public User currentUser { get; set; }
        private const string Url2 = "https://myapiauct.azurewebsites.net/api/users";
        private ObservableCollection<User> user;

        public Bid Bid1 { get; set; }
        public int bidVal = 0;
        private const string Url3 = "https://myapiauct.azurewebsites.net/api/bids";
        private ObservableCollection<Bid> bid1;

        public ItemDetailPage(int id, string name, string description, int price, string owner, string category, int idc)
        {
            InitializeComponent();
            name_lbl.Text = name;
            desc_lbl.Text = description;
            price_lbl.Text = price.ToString();
            owner_lbl.Text = owner;
            categ_lbl.Text = category;
            currentUser = new User
            {
                Id = idc
            };
            Application.Current.Properties["id"] = id;
        }

        protected override async void OnAppearing()
        {
            string response = await _client.GetStringAsync(Url2);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(response);
            user = new ObservableCollection<User>(users);

            string resp = await _client.GetStringAsync(Url3);
            List<Bid> bids = JsonConvert.DeserializeObject<List<Bid>>(resp);
            bid1 = new ObservableCollection<Bid>(bids);

            var id = Application.Current.Properties["id"];
            int i = int.Parse(id.ToString());

            foreach (User u in user)
            {
                if (u.Id == currentUser.Id)
                {
                    currentUser.UserName = u.UserName;
                }
            }
            foreach(Bid b in bid1)
            {
                if (b.ItemId == i && b.BidVal > bidVal) bidVal = b.BidVal;
                else bidVal = int.Parse(price_lbl.Text);
            }
            lBid_lbl.Text = bidVal.ToString();
            base.OnAppearing();
        }
        //event handler pentru butonul de Delete
        async void OnDelete(object sender, EventArgs e)
        {
            var id = Application.Current.Properties["id"];
            int i = int.Parse(id.ToString());
            var response = await _client.DeleteAsync($"{Url}/{i}");
            await Navigation.PopModalAsync();
        }
        async void OnBack(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        
        async void OnBid(object sender, EventArgs e)
        {
            if (owner_lbl.Text.Equals(currentUser.UserName))
            {
                await DisplayAlert("ERROR", "You can't bid for your own item", "OK");
            }
            else
            {
                eBAmount.IsVisible = true;
                bBidAdd.IsVisible = true;
            }
        }

        async void OnBidAdd(object sender, EventArgs e)
        {
            
            int bid = int.Parse(eBAmount.Text);
            int Bid = int.Parse(lBid_lbl.Text);
            if(bid <= int.Parse(price_lbl.Text))
            {
                await DisplayAlert("Error", "The bid must be greater than the initial price!", "OK");
            }
            else if(bid > int.Parse(price_lbl.Text) && bid <= Bid && Bid !=0 )
            {
                await DisplayAlert("Error", "The new bid must be greater than the previous one!", "OK");
            }
            else 
            {
                var id = Application.Current.Properties["id"];
                int i = int.Parse(id.ToString());
                /*Item = new Item
                {
                    Id = i,
                    Name = name_lbl.Text,
                    Description = desc_lbl.Text,
                    Price = int.Parse(price_lbl.Text),
                    Owner = owner_lbl.Text,
                    Category = categ_lbl.Text,
                    Bid = bid,
                    BidN = currentUser.UserName
                };*/
                Bid1 = new Bid
                {
                    BidVal = bid,
                    BidN = currentUser.UserName,
                    ItemId = i,
                    DateAdd = DateTime.UtcNow
                };
                var serial = JsonConvert.SerializeObject(Bid1);
                StringContent content = new StringContent(serial, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync(Url3, content);
                lBid_lbl.Text = bid.ToString();
                eBAmount.IsVisible = false;
                bBidAdd.IsVisible = false;
            }
        }
    }
}
