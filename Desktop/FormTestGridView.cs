using ProductContract.BusinessLogicContracts;
using ProductContract.ViewModels;
using COP_V6;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop
{
	public partial class FormTestGridView : Form
	{
		private readonly IProductLogic _productLogic;
		private readonly IUnitLogic _unitLogic;

		public FormTestGridView(IProductLogic productLogic, IUnitLogic unitLogic)
		{
			InitializeComponent();
			_productLogic = productLogic;
			_unitLogic = unitLogic;

			List<Parameters> parameters = new List<Parameters>()
			{
				new Parameters("Идентификатор", 100, true, "Id"),
				new Parameters("Единица измерения", 100, true, "Unit"),
				new Parameters("Название", 100, true, "Name"),
				new Parameters("Страна-производитель", 150, true, "Country")
			};
			controlDataGridView1.CreateColumns(parameters);
		}

		private void FormTestGridView_Load(object sender, EventArgs e)
		{
			LoadMain();
		}

		private void LoadMain()
		{
			controlDataGridView1.ClearCol();
			try
			{
				var products = _productLogic.Read(null);
				if (products == null)
				{
					return;
				}

				foreach (var product in products)
				{
					controlDataGridView1.SetObject<ProductViewModel>(product);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
