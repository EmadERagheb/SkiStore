﻿namespace SkiStore.Domain.Models
{
    public class BasketItem
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string PictureURL { get; set; }

        public string Type { get; set; }

    }
}