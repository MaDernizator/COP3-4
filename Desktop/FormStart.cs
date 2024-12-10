using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using ProductContract.ViewModels;
using ProductDatabaseImplement.Model;
using ComponentProgramming.NonVisualComponents.HelperModels;
using ComponentProgramming.NonVisualComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Components.NonVisualComponents.HelperModels;
using Components.NonVisualComponents;
using RebornComponent.VisualComponents;
using COP_V6.NoVisualComponent.Model;
using COP_V6.NoVisualComponent;
using static System.Reflection.Metadata.BlobBuilder;

namespace Desktop
{
	public partial class FormStart : Form
	{
		private IProductLogic _productLogic;
		private IUnitLogic _unitLogic;
		UserListBox _productList = new UserListBox();

		private bool _isDataLoaded = false;

		public FormStart(IProductLogic productLogic, IUnitLogic unitLogic)
		{
			InitializeComponent();
			_productLogic = productLogic;
			_unitLogic = unitLogic;

			_productList.SetTemplate("{", "}", "Идентификатор {Id} Единица измерения {Unit} Название {Name} {Country} Страна-производитель");
		}

		private void FormStart_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			try
			{
				if (this.Controls.Contains(_productList))
				{
					this.Controls.Remove(_productList);
					_productList.Dispose();
				}

				_productList = new UserListBox
				{
					Dock = DockStyle.Fill,
					ContextMenuStrip = contextMenuStrip1
				};

				_productList.SetTemplate("{", "}", "Идентификатор {Id} Единица измерения {Unit} Название {Name} {Country} Страна-производитель");

				this.Controls.Add(_productList);

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

					_productList.Add(bindingModel);
				}

				_isDataLoaded = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = Program.Container.Resolve<FormProduct>();
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

		private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = Program.Container.Resolve<FormProduct>();
			var product = _productList.GetSelected<Product>();
			form.Id = Convert.ToInt32(product.Id);

			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				var product = _productList.GetSelected<Product>();

				try
				{
					_productLogic.Delete(new ProductBindingModel { Id = product.Id });
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				LoadData();
			}
		}

		private void формаЕдиницToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = Program.Container.Resolve<FormUnit>();

			form.ShowDialog();
		}

		private void вордToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string filePath = "C:\\3curse";
			string fileName = "123.docx";

			using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					MessageBox.Show("Путь и имя файла установлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Выбор файла отменен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}

			var products = _productLogic.Read(null);
			var tablesData = new List<string[]>();

			if (products != null)
			{
				foreach (var product in products)
				{
					var suppliersList = GetSuppliersList(product);

					var productRow = suppliersList.Concat(Enumerable.Repeat(string.Empty, 3 - suppliersList.Count)).Take(3).ToArray();
					tablesData.Add(productRow);
				}
			}

			DocumentTableModel documentTableModel = new DocumentTableModel(filePath, fileName, new List<string[][]> { tablesData.ToArray() });

			DocWithTable docWithTable1 = new DocWithTable();
			docWithTable1.CreateDocumentWithTable(documentTableModel);

			MessageBox.Show("Документ успешно создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void excelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string filePath = "";


			using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					filePath = dialog.FileName;
				}
				else
				{
					return;
				}
			}
			var annotationRanges = new List<(int Min, int Max)>
			{
				(10, 20),
				(20, 30),
				(30, 40),
				(40, 50),
				(50, 60)
			};

			string documentTitle = "Отчет по продуктам";
			string chartTitle = "Распределение продуктов по странам";

			var products = _productLogic.Read(null);

			var seriesData = new Dictionary<string, List<int>>();

			var booksGroupedByType = products.GroupBy(products => products.Unit);

			foreach (var group in booksGroupedByType)
			{
				var seriesName = group.Key;
				var seriesValues = new List<int>();

				foreach (var range in annotationRanges)
				{
					int countInRange = group.Count(book =>
						book.Country.Length >= range.Min && book.Country.Length < range.Max);
					seriesValues.Add(countInRange);
				}

				seriesData[seriesName] = seriesValues;
			}

			// Конфигурация диаграммы
			var chartConfig = new LineChartConfig
			{
				Filepath = filePath,
				Header = documentTitle,
				ChartTitle = chartTitle,
				LegendPosition = LegendPosition.Bottom,
				Values = seriesData
			};

			try
			{
				// Создание Excel-документа
				var diagramComponent = new DiagramComponent();
				diagramComponent.CreateDocument(chartConfig);

				MessageBox.Show("Excel-отчет с диаграммой создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при создании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void PDFToolStripMenuItem_Click(object sender, EventArgs e)
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
				("Unit", "Единица измерения", 12),
				("Country", "Страна-производитель", 10)
			};

			var data = products.Select(product => new ProductDataForPdfWithFields
			{
				Id = product.Id.ToString(),
				Name = product.Name,
				Unit = product.Unit,
				Country = product.Country
			}).ToList();
			var merges = new List<(int, int)> { (1, 2) };

			var dataForPDF = new DataForTable<ProductDataForPdfWithFields>(
	filePath: filePath,
	documentTitle: documentTitle,
	merges: merges, // Пустой список слияний
	headers: headers,
	data: data
);
			Console.WriteLine($"Headers count: {headers.Count}");
			Console.WriteLine($"Data count: {data.Count}");
			PdfTable pdfTable = new PdfTable();
			try
			{
				bool success = pdfTable.createTable(dataForPDF);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}", "Исключение");
			}
			

/*			if (success)
			{
				MessageBox.Show("PDF создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Ошибка при создании PDF", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}*/
		}

		private List<string> GetSuppliersList(ProductViewModel product)
		{
			if (product == null)
			{
				throw new ArgumentNullException(nameof(product), "Продукт не должен быть пустым.");
			}

			var suppliersList = product.Suppliers
				.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(supplier => supplier.Trim())
				.Where(supplier => !string.IsNullOrWhiteSpace(supplier))
				.ToList();

			return suppliersList;
		}
	}

	public class ProductDataForPdfWithFields
	{
		public string Id;
		public string Name;
		public string Unit;
		public string Country;
	}
}
