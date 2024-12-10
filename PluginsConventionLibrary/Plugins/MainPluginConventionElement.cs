using System;

namespace PluginsConventionLibrary.Plugins
{
	public class MainPluginConventionElement : PluginsConventionElement
	{
		public string Name { get; set; } // Название продукта

		public string Unit { get; set; } // Единица измерения

		public string Country { get; set; } // Страна-производитель

		public string Suppliers { get; set; } // Список поставщиков
	}
}
