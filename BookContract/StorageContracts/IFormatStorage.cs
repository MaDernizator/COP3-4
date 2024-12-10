using ProductContract.BindingModels;
using ProductContract.ViewModels;
using System.Collections.Generic;

namespace ProductContract.StorageContracts
{
	public interface IUnitStorage
	{
		List<UnitViewModel> GetFullList();
		List<UnitViewModel> GetFilterList(UnitBindingModel model);
		UnitViewModel GetElement(UnitBindingModel model);
		void Insert(UnitBindingModel model);
		void Update(UnitBindingModel model);
		void Delete(UnitBindingModel model);
	}
}
