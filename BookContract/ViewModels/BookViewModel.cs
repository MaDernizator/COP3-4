using System;

namespace ProductContract.ViewModels
{
	public class ProductViewModel
	{
		public int? Id { get; set; }
		public string Name { get; set; } = string.Empty; // Название продукта
		public string Unit { get; set; } = string.Empty; // Единицы измерения поставок
		public string Country { get; set; } = string.Empty; // Страна-производитель
		public string Suppliers { get; set; } = string.Empty; // Список поставщиков
	}
}
