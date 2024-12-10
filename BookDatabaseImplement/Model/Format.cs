using System;
using System.ComponentModel.DataAnnotations;

namespace ProductDatabaseImplement.Model
{
	public class Unit
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; } = string.Empty; // Название единицы измерения
	}
}
