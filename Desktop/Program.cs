using ProductBusinessLogic.BusinessLogic;
using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using ProductContract.StorageContracts;
using ProductDatabaseImplement.Implements;
using Unity;
using Unity.Lifetime;

namespace Desktop
{
	internal static class Program
	{
		private static IUnityContainer container = null;

		public static IUnityContainer Container
		{
			get
			{
				if (container == null)
				{
					container = BuildUnityContainer();
				}
				return container;
			}
		}

		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(Container.Resolve<FormStart>());
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var currentContainer = new UnityContainer();

			currentContainer.RegisterType<IProductStorage, ProductStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IUnitStorage, UnitStorage>(new HierarchicalLifetimeManager());

			currentContainer.RegisterType<IProductLogic, ProductLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IUnitLogic, UnitLogic>(new HierarchicalLifetimeManager());

			return currentContainer;
		}
	}
}
