using ProductContract.BindingModels;
using ProductContract.ViewModels;
using System.Collections.Generic;

namespace ProductContract.StorageContracts
{
	public interface IProductStorage
	{
		List<ProductViewModel> GetFullList();
		List<ProductViewModel> GetFilterList(ProductBindingModel model);
		ProductViewModel GetElement(ProductBindingModel model);
		void Insert(ProductBindingModel model);
		void Update(ProductBindingModel model);
		void Delete(ProductBindingModel model);
	}
}
