﻿namespace mylittle_project.Application.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
