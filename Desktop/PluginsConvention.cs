using ProductBusinessLogic.BusinessLogic;
using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using COP_V6.NoVisualComponent.Model;
using COP_V6.NoVisualComponent;
using PluginsConventionLibrary.Plugins;
using RebornComponent.VisualComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductContract.ViewModels;
using Components.NonVisualComponents.HelperModels;
using Components.NonVisualComponents;
using ComponentProgramming.NonVisualComponents.HelperModels;
using ComponentProgramming.NonVisualComponents;

namespace Desktop
{
	public class PluginsConvention : IPluginsConvention
	{
		private IProductLogic _productLogic;
		private IUnitLogic _unitLogic;
		private UserListBox productList;

		public PluginsConvention()
		{
			_productLogic = new ProductLogic();
			_unitLogic = new UnitLogic();

			productList = new UserListBox();
			var menu = new ContextMenuStrip();

			var unitMenuItem = new ToolStripMenuItem("Единицы измерения");
			menu.Items.Add(unitMenuItem);

			unitMenuItem.Click += (sender, e) =>
			{
				var formUnit = new FormUnit(_unitLogic);
				formUnit.ShowDialog();
			};

			productList.ContextMenuStrip = menu;
			ReloadData();
		}

		/// Название плагина
		string IPluginsConvention.PluginName => PluginName();
		public string PluginName()
		{
			return "Продукты";
		}

		public UserControl GetControl => productList;

		PluginsConventionElement IPluginsConvention.GetElement => GetElement();

		public PluginsConventionElement GetElement()
		{
			var product = productList.GetSelected<MainPluginConventionElement>();
			if (product == null)
			{
				return null;
			}

			MainPluginConventionElement element = new MainPluginConventionElement
			{
				Id = product.Id,
				Name = product.Name,
				Unit = product.Unit,
				Country = product.Country,
				Suppliers = product.Suppliers
			};

			return new PluginsConventionElement { Id = element.Id };
		}

		public bool CreateChartDocument(PluginsConventionSaveDocument saveDocument)
		{
			try
			{
				string filePath = "";

				using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
				{
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						filePath = dialog.FileName.ToString();
						MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}

				string documentTitle = "Отчет";
				string chartTitle = "Количество продуктов";

				var products = _productLogic.Read(null);

				var seriesData = new Dictionary<string, List<int>>();

				var productsGroupedByUnit = products.GroupBy(product => product.Unit);

				foreach (var group in productsGroupedByUnit)
				{
					var seriesName = group.Key;
					var seriesValues = new List<int>();

					var countries = group.GroupBy(product => product.Country);
					foreach (var country in countries)
					{
						seriesValues.Add(country.Count());
					}

					seriesData[seriesName] = seriesValues;
				}

				var chartConfig = new LineChartConfig
				{
					Filepath = filePath,
					Header = documentTitle,
					ChartTitle = chartTitle,
					LegendPosition = LegendPosition.Bottom,
					Values = seriesData
				};

				DiagramComponent diagramComponent = new DiagramComponent();
				diagramComponent.CreateDocument(chartConfig);

				MessageBox.Show("Excel-отчет с диаграммой создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		public bool CreateSimpleDocument(PluginsConventionSaveDocument saveDocument)
		{
			try
			{
				string filePath = "";
				string fileName = "";

				using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
				{
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						filePath = dialog.FileName;
						MessageBox.Show($"Путь: {filePath}\nИмя файла: {fileName}", "Путь и имя файла установлены!", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						MessageBox.Show("Выбор файла отменен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return false;
					}
				}

				var products = _productLogic.Read(null);
				var tablesData = new List<string[]>();

				foreach (var product in products)
				{
					var suppliers = product.Suppliers?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
					var row = suppliers.Concat(Enumerable.Repeat(string.Empty, 3 - suppliers.Count)).ToArray();
					tablesData.Add(row);
				}

				DocumentTableModel documentTableModel = new DocumentTableModel(filePath, fileName, new List<string[][]> { tablesData.ToArray() });

				DocWithTable docWithTable1 = new DocWithTable();
				docWithTable1.CreateDocumentWithTable(documentTableModel);

				MessageBox.Show("Документ успешно создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		public bool CreateTableDocument(PluginsConventionSaveDocument saveDocument)
		{
			try
			{
				string filePath = "";

				using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
				{
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						filePath = dialog.FileName.ToString();
						MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}

				string documentTitle = "Отчет по всем продуктам";

				var products = _productLogic.Read(null);

				var headers = new List<(string, string, int)>
				{
					("Id", "Идентификатор", 12),
					("Name", "Название", 12),
					("Unit", "Единица измерения", 10),
					("Country", "Страна-производитель", 10)
				};

				var merges = new List<(int, int)> { (2, 3) };

				var data = products.Select(product => new ProductDataForPdfWithFields
				{
					Id = product.Id.ToString(),
					Name = product.Name,
					Unit = product.Unit,
					Country = product.Country
				}).ToList();

				var dataForPDF = new DataForTable<ProductDataForPdfWithFields>(
					filePath: filePath,
					documentTitle: documentTitle,
					merges: merges,
					headers: headers,
					data: data
				);

				PdfTable pdfTable = new PdfTable();
				bool success = pdfTable.createTable(dataForPDF);

				if (success)
				{
					MessageBox.Show("PDF создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Ошибка при создании PDF", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public bool DeleteElement(PluginsConventionElement element)
		{
			try
			{
				_productLogic.Delete(new ProductBindingModel { Id = element.Id });
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			return true;
		}

		public Form GetForm(PluginsConventionElement element)
		{
			var formProduct = new FormProduct(_productLogic, _unitLogic);

			if (element != null)
			{
				formProduct.Id = element.Id;
			}

			return formProduct;
		}

		public Form GetThesaurus()
		{
			return new FormUnit(_unitLogic);
		}

		public void ReloadData()
		{
			productList.SetTemplate("{", "}", "Страна {Country} Идентификатор {Id} Название {Name} Единица измерений {Unit}");
			var products = _productLogic.Read(null);
			foreach (var product in products)
			{
				var bindingModel = new ProductBindingModel
				{
					Id = product.Id,
					Unit = product.Unit,
					Name = product.Name,
					Country = product.Country,
					Suppliers = product.Suppliers
				};

				productList.Add(bindingModel);
			}
		}
	}
}
