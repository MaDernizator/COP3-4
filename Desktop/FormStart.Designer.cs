namespace Desktop
{
	partial class FormStart
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
			contextMenuStrip1 = new ContextMenuStrip(components);
			добавитьToolStripMenuItem = new ToolStripMenuItem();
			редактироватьToolStripMenuItem = new ToolStripMenuItem();
			удалитьToolStripMenuItem = new ToolStripMenuItem();
			формаПродуктовToolStripMenuItem = new ToolStripMenuItem();
			вордToolStripMenuItem = new ToolStripMenuItem();
			PDFToolStripMenuItem = new ToolStripMenuItem();
			excelToolStripMenuItem = new ToolStripMenuItem();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			contextMenuStrip1.ImageScalingSize = new Size(20, 20);
			contextMenuStrip1.Items.AddRange(new ToolStripItem[] { добавитьToolStripMenuItem, редактироватьToolStripMenuItem, удалитьToolStripMenuItem, формаПродуктовToolStripMenuItem, вордToolStripMenuItem, PDFToolStripMenuItem, excelToolStripMenuItem });
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new Size(233, 172);
			// 
			// добавитьToolStripMenuItem
			// 
			добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
			добавитьToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
			добавитьToolStripMenuItem.Size = new Size(232, 24);
			добавитьToolStripMenuItem.Text = "Добавить";
			добавитьToolStripMenuItem.Click += добавитьToolStripMenuItem_Click;
			// 
			// редактироватьToolStripMenuItem
			// 
			редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
			редактироватьToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U;
			редактироватьToolStripMenuItem.Size = new Size(232, 24);
			редактироватьToolStripMenuItem.Text = "Редактировать";
			редактироватьToolStripMenuItem.Click += редактироватьToolStripMenuItem_Click;
			// 
			// удалитьToolStripMenuItem
			// 
			удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			удалитьToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
			удалитьToolStripMenuItem.Size = new Size(232, 24);
			удалитьToolStripMenuItem.Text = "Удалить";
			удалитьToolStripMenuItem.Click += удалитьToolStripMenuItem_Click;
			// 
			// формаПродуктовToolStripMenuItem
			// 
			формаПродуктовToolStripMenuItem.Name = "формаПродуктовToolStripMenuItem";
			формаПродуктовToolStripMenuItem.Size = new Size(232, 24);
			формаПродуктовToolStripMenuItem.Text = "Форма продуктов";
			формаПродуктовToolStripMenuItem.Click += формаЕдиницToolStripMenuItem_Click;
			// 
			// вордToolStripMenuItem
			// 
			вордToolStripMenuItem.Name = "вордToolStripMenuItem";
			вордToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
			вордToolStripMenuItem.Size = new Size(232, 24);
			вордToolStripMenuItem.Text = "Ворд";
			вордToolStripMenuItem.Click += вордToolStripMenuItem_Click;
			// 
			// PDFToolStripMenuItem
			// 
			PDFToolStripMenuItem.Name = "PDFToolStripMenuItem";
			PDFToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.T;
			PDFToolStripMenuItem.Size = new Size(232, 24);
			PDFToolStripMenuItem.Text = "PDF";
			PDFToolStripMenuItem.Click += PDFToolStripMenuItem_Click;
			// 
			// excelToolStripMenuItem
			// 
			excelToolStripMenuItem.Name = "excelToolStripMenuItem";
			excelToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
			excelToolStripMenuItem.Size = new Size(232, 24);
			excelToolStripMenuItem.Text = "Excel";
			excelToolStripMenuItem.Click += excelToolStripMenuItem_Click;
			// 
			// FormStart
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(914, 600);
			Margin = new Padding(3, 4, 3, 4);
			Name = "FormStart";
			Text = "Учет продуктов";
			Load += FormStart_Load;
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem добавитьToolStripMenuItem;
		private ToolStripMenuItem удалитьToolStripMenuItem;
		private ToolStripMenuItem редактироватьToolStripMenuItem;
		private Components.VisualComponents.UserListBox productList;
		private ToolStripMenuItem формаПродуктовToolStripMenuItem;
		private ToolStripMenuItem вордToolStripMenuItem;
		private ToolStripMenuItem PDFToolStripMenuItem;
		private ToolStripMenuItem excelToolStripMenuItem;
	}
}
