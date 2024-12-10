namespace Desktop
{
	partial class FormTestGridView
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
			controlDataGridView1 = new COP_V6.ControlDataGridView();
			SuspendLayout();
			// 
			// controlDataGridView1
			// 
			controlDataGridView1.Dock = DockStyle.Fill;
			controlDataGridView1.Location = new Point(0, 0);
			controlDataGridView1.Name = "controlDataGridView1";
			controlDataGridView1.Size = new Size(560, 331);
			controlDataGridView1.TabIndex = 0;
			// 
			// FormTestGridView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(560, 331);
			Controls.Add(controlDataGridView1);
			Name = "FormTestGridView";
			Text = "Продукты";
			Load += FormTestGridView_Load;
			ResumeLayout(false);
		}

		#endregion

		private COP_V6.ControlDataGridView controlDataGridView1;
	}
}
