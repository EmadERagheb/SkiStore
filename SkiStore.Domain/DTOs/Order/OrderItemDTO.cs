﻿namespace SkiStore.Domain.DTOs.Order
{
    public class OrderItemDTO
    {
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}