namespace Desktop
{
	partial class FormProduct
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			userTextBox1 = new ComponentProgramming.UserTextBox();
			unitList = new COP_V6.ControlListBox();
			productName = new TextBox();
			suppliers = new TextBox();
			saveButton = new Button();
			closeButton = new Button();
			SuspendLayout();
			// 
			// userTextBox1
			// 
			userTextBox1.Location = new Point(511, 11);
			userTextBox1.Margin = new Padding(3, 2, 3, 2);
			userTextBox1.MaxValue = null;
			userTextBox1.MinValue = null;
			userTextBox1.Name = "userTextBox1";
			userTextBox1.Size = new Size(277, 46);
			userTextBox1.TabIndex = 0;
			userTextBox1.Load += userTextBox1_Load;
			// 
			// unitList
			// 
			unitList.Location = new Point(511, 62);
			unitList.Name = "unitList";
			unitList.SelectedItem = "";
			unitList.Size = new Size(146, 126);
			unitList.TabIndex = 1;
			// 
			// productName
			// 
			productName.Location = new Point(225, 25);
			productName.Name = "productName";
			productName.Size = new Size(133, 23);
			productName.TabIndex = 2;
			// 
			// suppliers
			// 
			suppliers.Location = new Point(225, 62);
			suppliers.Name = "suppliers";
			suppliers.Size = new Size(280, 23);
			suppliers.TabIndex = 3;
			// 
			// saveButton
			// 
			saveButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			saveButton.Location = new Point(683, 209);
			saveButton.Name = "saveButton";
			saveButton.Size = new Size(105, 42);
			saveButton.TabIndex = 4;
			saveButton.Text = "Save";
			saveButton.UseVisualStyleBackColor = true;
			saveButton.Click += saveButton_Click;
			// 
			// closeButton
			// 
			closeButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			closeButton.Location = new Point(556, 209);
			closeButton.Name = "closeButton";
			closeButton.Size = new Size(101, 42);
			closeButton.TabIndex = 5;
			closeButton.Text = "Close";
			closeButton.UseVisualStyleBackColor = true;
			closeButton.Click += closeButton_Click;
			// 
			// FormProduct
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(closeButton);
			Controls.Add(saveButton);
			Controls.Add(suppliers);
			Controls.Add(productName);
			Controls.Add(unitList);
			Controls.Add(userTextBox1);
			Name = "FormProduct";
			Text = "Продукты";
			Load += FormProduct_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComponentProgramming.UserTextBox userTextBox1;
		private COP_V6.ControlListBox unitList;
		private TextBox productName;
		private TextBox suppliers;
		private Button saveButton;
		private Button closeButton;
	}
}
