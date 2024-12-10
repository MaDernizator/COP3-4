using DocumentFormat.OpenXml.Bibliography;
using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using ProductContract.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop
{
	public partial class FormProduct : Form
	{
		private int? id;
		public int Id { set { id = value; } }
		private IProductLogic _productLogic;
		private IUnitLogic _unitLogic;

		public FormProduct(IProductLogic productLogic, IUnitLogic unitLogic)
		{
			InitializeComponent();
			_productLogic = productLogic;
			_unitLogic = unitLogic;
		}

		private void userTextBox1_Load(object sender, EventArgs e)
		{
			userTextBox1.MinValue = 10;
			userTextBox1.MaxValue = 50;
		}

		private void FormProduct_Load(object sender, EventArgs e)
		{
			List<UnitViewModel> units = _unitLogic.Read(null);

			if (units != null)
			{
				foreach (UnitViewModel unit in units)
				{
					unitList.AddItem(unit.Name);
				}
			}

			if (id.HasValue)
			{
				try
				{
					ProductViewModel product = _productLogic.Read(new ProductBindingModel { Id = id.Value })?[0];

					if (product != null)
					{
						productName.Text = product.Name;
						suppliers.Text = product.Suppliers;
						unitList.SelectedItem = product.Unit;
						userTextBox1.TextBoxValue = product.Country;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			try
			{
				_productLogic.CreateOrUpdate(new ProductBindingModel
				{
					Id = id,
					Name = productName.Text,
					Suppliers = suppliers.Text,
					Unit = unitList.SelectedItem,
					Country = userTextBox1.TextBoxValue,
				});
				MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
