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
using Auctions.Data;
using SQLite;

namespace Auctions.Views
{
    public partial class ItemsPage : ContentPage
    {

        public ItemsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            itemsListView.ItemsSource = await App.Database.GetItemsAsync();
        }

        //event handler pentru apasarea ToolbarItem; trimitere la adaugarea unui produs
        async void OnAddItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage
            {
                BindingContext = new Item()
            });
        }

        //event handler pentru selectarea unui produs din lista; trimitere la pagina cu detaliile produsului
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ItemDetailPage
                {
                    BindingContext = e.SelectedItem as Item
                });
            }
        }

        //event handler pentru bara de cautare
        async void OnChange(object sender, EventArgs e)
        {
            string searchBar = SearchB.Text;
            itemsListView.ItemsSource = await App.Database.GetItems(searchBar);
        }

        async void OnCatgSelect(object sender, EventArgs e)
        {
            var pick = (Picker)sender;
            string catg = pick.SelectedItem.ToString();
            if (catg.Equals("All"))
            {
                itemsListView.ItemsSource = await App.Database.GetItemsAsync();
            }
            else
            {
                itemsListView.ItemsSource = await App.Database.GetCategory(catg);
            }
        }
    }
}
