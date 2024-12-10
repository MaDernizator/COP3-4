using PluginsConventionLibrary.Plugins;
using System.Reflection;

namespace DesktopPlugins
{
	public partial class FormMain : Form
	{
		private readonly Dictionary<string, IPluginsConvention> _plugins;

		private string _selectedPlugin;

		public FormMain()
		{
			InitializeComponent();
			_plugins = LoadPlugins();

			_selectedPlugin = string.Empty;
		}

		// Загрузка плагинов
		private Dictionary<string, IPluginsConvention> LoadPlugins()
		{
			var plugins = new Dictionary<string, IPluginsConvention>();

			// Ищем в директории плагины, указан путь к каталогу
			string pluginsDir = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + "\\plugins";

			// Получает файлы dll
			string[] dllFiles = Directory.GetFiles(pluginsDir, "*.dll", SearchOption.AllDirectories);

			foreach (string dllFile in dllFiles)
			{
				try
				{
					/// Загрузка каждого dll 
					Assembly assembly = Assembly.LoadFrom(dllFile);
					Type[] types = assembly.GetTypes();

					foreach (var type in types)
					{
						// Поиск классов, которые реализуют интерфейс плагина
						if (typeof(IPluginsConvention).IsAssignableFrom(type) && !type.IsInterface)
						{
							var plugin = (IPluginsConvention)Activator.CreateInstance(type)!;
							plugins.Add(plugin.PluginName, plugin);
							CreateToolStripMenuItem(plugin.PluginName);
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Ошибка при загрузке сборки {dllFile}: {ex.Message}");
				}
			}

			return plugins;
		}

		/// Создать ToolStripMenuItem для плагина (создание элементов меню)
		private void CreateToolStripMenuItem(string pluginName)
		{
			var menuItem = new ToolStripMenuItem(pluginName);
			menuItem.Click += (object? sender, EventArgs e) =>
			{
				_selectedPlugin = pluginName;

				// Начало обработки выбранногоо плагина
				IPluginsConvention plugin = _plugins![pluginName];

				// Get Control возвращает пользовательский элемент
				UserControl userControl = plugin.GetControl;

				if (userControl != null)
				{
					// panelControl добавляет контейнер на котором происходит отображение интерфейса
					panelControl.Controls.Clear();
					plugin.ReloadData();
					userControl.Dock = DockStyle.Fill;
					panelControl.Controls.Add(userControl);
				}
			};

			ControlsStripMenuItem.DropDownItems.Add(menuItem);
		}

		private void FormMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Control)
			{
				return;
			}

			switch (e.KeyCode)
			{
				case Keys.I:
					ShowThesaurus();
					break;
				case Keys.A:
					AddNewElement();
					break;
				case Keys.U:
					UpdateElement();
					break;
				case Keys.D:
					DeleteElement();
					break;
				case Keys.S:
					CreateSimpleDoc();
					break;
				case Keys.T:
					CreateTableDoc();
					break;
				case Keys.C:
					CreateChartDoc();
					break;
			}
		}

		/// Отобразить форму для работы со справочником
		private void ShowThesaurus()
		{
			if (string.IsNullOrEmpty(_selectedPlugin) || !_plugins.ContainsKey(_selectedPlugin))
			{
				MessageBox.Show("Плагин не выбран или отсутствует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			_plugins[_selectedPlugin].GetThesaurus()?.Show();
		}


		private void AddNewElement()
		{
			var form = _plugins[_selectedPlugin].GetForm(null);

			if (form != null && form.ShowDialog() == DialogResult.OK)
			{
				_plugins[_selectedPlugin].ReloadData();
			}
		}

		private void UpdateElement()
		{
			var element = _plugins[_selectedPlugin].GetElement;

			if (element == null)
			{
				MessageBox.Show("Нет выбранного элемента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var form = _plugins[_selectedPlugin].GetForm(element);
			if (form != null && form.ShowDialog() == DialogResult.OK)
			{
				_plugins[_selectedPlugin].ReloadData();
			}
		}

		private void DeleteElement()
		{
			if (MessageBox.Show("Удалить выбранный элемент", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}

			var element = _plugins[_selectedPlugin].GetElement;

			if (element == null)
			{
				MessageBox.Show("Нет выбранного элемента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (_plugins[_selectedPlugin].DeleteElement(element))
			{
				_plugins[_selectedPlugin].ReloadData();
			}
		}

		/// Создать простой документ
		private void CreateSimpleDoc()
		{
			if (_plugins[_selectedPlugin].CreateSimpleDocument(new PluginsConventionSaveDocument()))
			{
				MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Ошибка при создании документа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// Создать документ с настраиваемой таблицей
		private void CreateTableDoc()
		{
			if (_plugins[_selectedPlugin].CreateTableDocument(new PluginsConventionSaveDocument()))
			{
				MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Ошибка при создании документа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// Создать документ с диаграммой
		private void CreateChartDoc()
		{
			if (_plugins[_selectedPlugin].CreateChartDocument(new PluginsConventionSaveDocument()))
			{
				MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Ошибка при создании документа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// Отобразить форму для работы со справочником
		private void ThesaurusToolStripMenuItem_Click(object sender, EventArgs e) => ShowThesaurus();

		/// Создать элемент
		private void AddElementToolStripMenuItem_Click(object sender, EventArgs e) => AddNewElement();

		/// Редактировать элемент
		private void UpdElementToolStripMenuItem_Click(object sender, EventArgs e) => UpdateElement();

		/// Удалить элемент
		private void DelElementToolStripMenuItem_Click(object sender, EventArgs e) => DeleteElement();

		/// Создать простой документ
		private void SimpleDocToolStripMenuItem_Click(object sender, EventArgs e) => CreateSimpleDoc();

		/// Создать документ с настраиваемой таблицей
		private void TableDocToolStripMenuItem_Click(object sender, EventArgs e) => CreateTableDoc();

		/// Создать документ с диаграммой
		private void ChartDocToolStripMenuItem_Click(object sender, EventArgs e) => CreateChartDoc();
	}
}
