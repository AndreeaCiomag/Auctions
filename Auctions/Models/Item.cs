using System;
using SQLite;

namespace Auctions.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Owner { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public int Bid { get; set; }
        public string BidN { get; set; }
    }
}
