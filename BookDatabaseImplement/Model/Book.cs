using System;
using System.ComponentModel.DataAnnotations;

namespace ProductDatabaseImplement.Model
{
	public class Product
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; } = string.Empty; // Название продукта

		[Required]
		public string Unit { get; set; } = string.Empty; // Единицы измерения поставок

		[Required]
		public string Country { get; set; } = string.Empty; // Страна-производитель

		[Required]
		public string Suppliers { get; set; } = string.Empty; // Список поставщиков
	}
}
