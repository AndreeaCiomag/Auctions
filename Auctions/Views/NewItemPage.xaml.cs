using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Auctions.Models;

namespace Auctions.Views
{
    public partial class NewItemPage : ContentPage
    {

        public NewItemPage()
        {
            InitializeComponent();
            //BindingContext = new NewItemViewModel();
        }

        //event handler pentru butonul de Save
        async void OnSave(object sender, EventArgs e)
        {
            var item = (Item)BindingContext;
            await App.Database.SaveItemAsync(item);
            await Navigation.PopAsync();
        }

        //event handler pentru butonul de Cancel
        async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
