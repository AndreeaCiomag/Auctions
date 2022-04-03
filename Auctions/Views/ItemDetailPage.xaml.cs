using System.ComponentModel;
using Xamarin.Forms;
using Auctions.Models;
using System;

namespace Auctions.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        //event handler pentru butonul de Delete
        async void OnDelete(object sender, EventArgs e)
        {
            var item = (Item)BindingContext;
            await App.Database.DeleteItemAsync(item);
            await Navigation.PopAsync();
        }

        void OnBid(object sender, EventArgs e)
        {
            eBName.IsVisible = true;
            eBAmount.IsVisible = true;
            bBidAdd.IsVisible = true;
        }

        async void OnBidAdd(object sender, EventArgs e)
        {
            var item = (Item)BindingContext;
            int bid = int.Parse(eBAmount.Text);
            int Bid = int.Parse(lBidA.Text);
            if(bid <= item.Price)
            {
                await DisplayAlert("Error", "The bid must be greater than the initial price!", "OK");
            }
            else if(bid > item.Price && bid <= Bid)
            {
                await DisplayAlert("Error", "The new bid must be greater than the previous one!", "OK");
            }
            else
            {
                await App.Database.SaveItemAsync(item);
                eBName.IsVisible = false;
                eBAmount.IsVisible = false;
                bBidAdd.IsVisible = false;
            }
        }
    }
}
