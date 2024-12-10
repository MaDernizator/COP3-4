using ProductContract.BindingModels;
using ProductContract.ViewModels;
using System.Collections.Generic;

namespace ProductContract.BusinessLogicContracts
{
	public interface IProductLogic
	{
		List<ProductViewModel> Read(ProductBindingModel model);

		void CreateOrUpdate(ProductBindingModel model);

		void Delete(ProductBindingModel model);
	}
}
