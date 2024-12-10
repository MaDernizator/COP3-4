using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using ProductContract.StorageContracts;
using ProductContract.ViewModels;
using ProductDatabaseImplement.Implements;
using System;
using System.Collections.Generic;

namespace ProductBusinessLogic.BusinessLogic
{
	public class UnitLogic : IUnitLogic
	{
		private readonly IUnitStorage _unitStorage;

		public UnitLogic()
		{
			_unitStorage = new UnitStorage();
		}

		public UnitLogic(IUnitStorage unitStorage)
		{
			_unitStorage = unitStorage;
		}

		public void CreateOrUpdate(UnitBindingModel model)
		{
			var element = _unitStorage.GetElement(new UnitBindingModel { Name = model.Name });
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Единица измерения с таким названием уже существует");
			}
			if (model.Id.HasValue)
			{
				_unitStorage.Update(model);
			}
			else
			{
				_unitStorage.Insert(model);
			}
		}

		public void Delete(UnitBindingModel model)
		{
			var element = _unitStorage.GetElement(new UnitBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Единица измерения с таким названием не найдена");
			}
			_unitStorage.Delete(model);
		}

		public List<UnitViewModel> Read(UnitBindingModel model)
		{
			if (model == null)
				return _unitStorage.GetFullList();
			if (model.Id.HasValue)
			{
				return new List<UnitViewModel> {
					_unitStorage.GetElement(model)
				};
			}
			return _unitStorage.GetFullList();
		}
	}
}
