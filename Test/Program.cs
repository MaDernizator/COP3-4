using ProductBusinessLogic.BusinessLogic;
using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using ProductContract.StorageContracts;
using ProductContract.ViewModels;
using ProductDatabaseImplement.Implements;
using Microsoft.Extensions.DependencyInjection;

class Program
{
	static void Main(string[] args)
	{
		// Настройка DI (если используете)
		var serviceProvider = new ServiceCollection()
			.AddSingleton<IProductStorage, ProductStorage>()
			.AddSingleton<IProductLogic, ProductLogic>()
			.AddSingleton<IUnitLogic, UnitLogic>()
			.AddSingleton<IUnitStorage, UnitStorage>()
			.BuildServiceProvider();

		var productLogic = serviceProvider.GetService<IProductLogic>();
		var unitLogic = serviceProvider.GetService<IUnitLogic>();

		var product1 = new ProductBindingModel
		{
			Id = 1,
			Name = "Молоко",
			Unit = "Литры",
			Country = "Россия",
			Suppliers = "Поставщик1, Поставщик2, Поставщик3"
		};
		// productLogic.CreateOrUpdate(product1);

		var unit1 = new UnitBindingModel { Name = "Упаковки" };
		var unit2 = new UnitBindingModel { Name = "Литры" };
		var unit3 = new UnitBindingModel { Name = "Килограммы" };

		List<string> allSuppliers = GetSuppliersList(product1);

		foreach (var supplier in allSuppliers)
		{
			Console.WriteLine(supplier);
		}

		// unitLogic.CreateOrUpdate(unit1);
		// unitLogic.CreateOrUpdate(unit2);
		// unitLogic.CreateOrUpdate(unit3);

		List<UnitViewModel> units = unitLogic.Read(null);

		if (units != null)
		{
			foreach (UnitViewModel unit in units)
			{
				Console.WriteLine($"{unit.Name}");
			}
		}

		// Вывод всех продуктов
		DisplayAllProducts(productLogic);
	}

	private static void DisplayAllProducts(IProductLogic productLogic)
	{
		try
		{
			var products = productLogic.Read(null);
			if (products.Count == 0)
			{
				Console.WriteLine("Нет доступных продуктов.");
				return;
			}

			foreach (var product in products)
			{
				Console.WriteLine($"ID: {product.Id}, Название: {product.Name}, Единица измерения: {product.Unit}, Страна: {product.Country}");
				Console.WriteLine($"Поставщики: {product.Suppliers}");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Ошибка: {ex.Message}");
		}
	}

	private static List<string> GetSuppliersList(ProductBindingModel product)
	{
		if (product == null)
		{
			throw new ArgumentNullException(nameof(product), "Продукт не должен быть пустым.");
		}

		// Извлекаем строку Suppliers, разделяем по запятым, удаляем пробелы и добавляем в список
		var suppliersList = product.Suppliers
			.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(supplier => supplier.Trim()) // Удаляем пробелы вокруг каждого элемента
			.Where(supplier => !string.IsNullOrWhiteSpace(supplier)) // Исключаем пустые строки
			.ToList();

		return suppliersList;
	}
}
