using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Desktop
{
	public partial class FormUnit : Form
	{
		private readonly IUnitLogic _unitLogic;

		BindingList<UnitBindingModel> list;

		public FormUnit(IUnitLogic unitLogic)
		{
			InitializeComponent();

			_unitLogic = unitLogic;
			dataGridView1.AllowUserToAddRows = true;
			list = new BindingList<UnitBindingModel>();
		}

		private void FormUnit_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			try
			{
				var list1 = _unitLogic.Read(null);
				list.Clear();

				foreach (var item in list1)
				{
					list.Add(new UnitBindingModel
					{
						Id = item.Id,
						Name = item.Name,
					});
				}

				if (list != null)
				{
					dataGridView1.DataSource = list;
					dataGridView1.Columns[0].Visible = false;
					dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var unitName = (string)dataGridView1.CurrentRow.Cells[1].Value;

			if (!string.IsNullOrEmpty(unitName))
			{
				if (dataGridView1.CurrentRow.Cells[0].Value != null)
				{
					_unitLogic.CreateOrUpdate(new UnitBindingModel()
					{
						Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),
						Name = (string)dataGridView1.CurrentRow.Cells[1].EditedFormattedValue
					});
				}
				else
				{
					_unitLogic.CreateOrUpdate(new UnitBindingModel()
					{
						Name = (string)dataGridView1.CurrentRow.Cells[1].EditedFormattedValue
					});
				}
			}
			else
			{
				MessageBox.Show("Введена пустая строка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			LoadData();
		}

		private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Insert)
			{
				if (dataGridView1.Rows.Count == 0)
				{
					list.Add(new UnitBindingModel());
					dataGridView1.DataSource = new List<UnitBindingModel>(list);
					dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];

					return;
				}

				if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value != null)
				{
					list.Add(new UnitBindingModel());
					dataGridView1.DataSource = new List<UnitBindingModel>(list);
					dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];

					return;
				}
			}

			if (e.KeyData == Keys.Delete)
			{
				if (dataGridView1.SelectedRows.Count == 1)
				{
					if (MessageBox.Show("Удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

						try
						{
							_unitLogic.Delete(new UnitBindingModel { Id = id });

							dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

							LoadData();
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "Ошибка при удалении записи", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}
	}
}
