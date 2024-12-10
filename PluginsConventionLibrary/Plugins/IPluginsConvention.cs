using System.Windows.Forms;

namespace PluginsConventionLibrary.Plugins
{
	public interface IPluginsConvention
	{
		/// Название плагина
		string PluginName { get; }

		/// Получение контрола для вывода набора данных
		UserControl GetControl { get; }

		/// Получение элемента, выбранного в контроле    
		PluginsConventionElement GetElement { get; }

		/// Получение формы для создания/редактирования объекта       
		Form GetForm(PluginsConventionElement element);

		/// Получение формы для работы со справочником
		Form GetThesaurus();

		/// Удаление элемента
		bool DeleteElement(PluginsConventionElement element);

		/// Обновление набора данных в контроле
		void ReloadData();

		/// Создание простого документа
		bool CreateSimpleDocument(PluginsConventionSaveDocument saveDocument);

		/// Создание документа таблицы
		bool CreateTableDocument(PluginsConventionSaveDocument saveDocument);

		/// Создание документа диаграммы
		bool CreateChartDocument(PluginsConventionSaveDocument saveDocument);


	}
}
