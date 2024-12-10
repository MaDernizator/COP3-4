namespace Desktop
{
	partial class FormWord
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			docWithTable1 = new COP_V6.NoVisualComponent.DocWithTable(components);
			create = new Button();
			SuspendLayout();
			// 
			// create
			// 
			create.Location = new Point(12, 13);
			create.Margin = new Padding(3, 4, 3, 4);
			create.Name = "create";
			create.Size = new Size(233, 31);
			create.TabIndex = 2;
			create.Text = "Создать документ";
			create.UseVisualStyleBackColor = true;
			create.Click += create_Click;
			// 
			// FormWord
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(261, 57);
			Controls.Add(create);
			Margin = new Padding(3, 4, 3, 4);
			Name = "FormWord";
			Text = "Создание Word-документа";
			ResumeLayout(false);
		}

		#endregion

		private COP_V6.NoVisualComponent.DocWithTable docWithTable1;
		private Button create;
	}
}
