using ProductContract.BindingModels;
using ProductContract.ViewModels;
using System.Collections.Generic;

namespace ProductContract.BusinessLogicContracts
{
	public interface IUnitLogic
	{
		List<UnitViewModel> Read(UnitBindingModel model);

		void CreateOrUpdate(UnitBindingModel model);

		void Delete(UnitBindingModel model);
	}
}
