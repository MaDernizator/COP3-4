using ProductContract.BindingModels;
using ProductContract.BusinessLogicContracts;
using ProductContract.StorageContracts;
using ProductContract.ViewModels;
using ProductDatabaseImplement.Implements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductBusinessLogic.BusinessLogic
{
	public class ProductLogic : IProductLogic
	{
		private readonly IProductStorage _productStorage;

		public ProductLogic()
		{
			_productStorage = new ProductStorage();
		}

		public ProductLogic(IProductStorage productStorage)
		{
			_productStorage = productStorage;
		}

		public void CreateOrUpdate(ProductBindingModel model)
		{
			var element = _productStorage.GetElement(new ProductBindingModel { Name = model.Name });
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Продукт с таким названием уже существует");
			}
			if (model.Id.HasValue)
			{
				_productStorage.Update(model);
			}
			else
			{
				_productStorage.Insert(model);
			}
		}

		public void Delete(ProductBindingModel model)
		{
			var element = _productStorage.GetElement(new ProductBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Продукт с таким названием не найден");
			}
			_productStorage.Delete(model);
		}

		public List<ProductViewModel> Read(ProductBindingModel model)
		{
			if (model == null)
				return _productStorage.GetFullList();
			if (model.Id.HasValue)
			{
				return new List<ProductViewModel> {
					_productStorage.GetElement(model)
				};
			}
			return _productStorage.GetFullList();
		}
	}
}
