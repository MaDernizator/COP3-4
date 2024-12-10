using System;

namespace ProductContract.BindingModels
{
	public class UnitBindingModel
	{
		public int? Id { get; set; }
		public string Name { get; set; } = string.Empty; // Название единицы измерения
	}
}
