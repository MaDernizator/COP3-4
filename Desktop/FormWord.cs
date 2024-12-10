using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using ProductContract.ViewModels;
using COP_V6.NoVisualComponent;
using COP_V6.NoVisualComponent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Desktop
{
	public partial class FormWord : Form
	{
		private IProductLogic _productLogic;
		private IUnitLogic _unitLogic;

		public FormWord(IProductLogic productLogic, IUnitLogic unitLogic)
		{
			InitializeComponent();
			docWithTable1 = new DocWithTable();
			_productLogic = productLogic;
			_unitLogic = unitLogic;
		}

		private void create_Click(object sender, EventArgs e)
		{
			string filePath = "C:/Users/salih/OneDrive/Рабочий стол/testCop";
			string fileName = "test";

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

			List<string> allSuppliers = GetAllSuppliers();
			var tablesData = allSuppliers.Select(supplier => new string[] { supplier }).ToArray();

			DocumentTableModel documentTableModel = new DocumentTableModel(filePath, fileName, new List<string[][]> { tablesData });

			docWithTable1.CreateDocumentWithTable(documentTableModel);

			MessageBox.Show("Документ успешно создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		private List<string> GetAllSuppliers()
		{
			var allSuppliers = new List<string>();

			var products = _productLogic.Read(null);
			if (products != null)
			{
				foreach (var product in products)
				{
					var suppliersList = GetSuppliersList(product);
					allSuppliers.AddRange(suppliersList);
				}
			}

			return allSuppliers.Skip(Math.Max(0, allSuppliers.Count - 3)).ToList();
		}
	}
}
