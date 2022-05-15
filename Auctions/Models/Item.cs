using System;
using SQLite;

namespace Auctions.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Owner { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateFin { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime DataNasterii { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class Bid
    {
        public int Id { get; set; }
        public int BidVal { get; set; }
        public string BidN { get; set; }
        public int ItemId { get; set; }
        public DateTime DateAdd { get; set; }
    }
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int BidId { get; set; }
    }
}
