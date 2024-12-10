using ProductContract.BindingModels;
using ProductContract.StorageContracts;
using ProductContract.ViewModels;
using ProductDatabaseImplement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductDatabaseImplement.Implements
{
	public class ProductStorage : IProductStorage
	{
		private Product CreateModel(ProductBindingModel model, Product product)
		{
			product.Name = model.Name;
			product.Unit = model.Unit;
			product.Country = model.Country;
			product.Suppliers = model.Suppliers;
			return product;
		}

		private ProductViewModel CreateModel(Product product)
		{
			return new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				Unit = product.Unit,
				Country = product.Country,
				Suppliers = product.Suppliers
			};
		}

		public List<ProductViewModel> GetFullList()
		{
			using var context = new ProductDatabase();
			return context.Products
				.ToList()
				.Select(CreateModel)
				.ToList();
		}

		public List<ProductViewModel> GetFilterList(ProductBindingModel model)
		{
			using var context = new ProductDatabase();
			return context.Products
				.Where(product => product.Unit.Contains(model.Unit))
				.ToList()
				.Select(CreateModel)
				.ToList();
		}

		public ProductViewModel? GetElement(ProductBindingModel model)
		{
			using var context = new ProductDatabase();
			var product = context.Products
				.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
			return product != null ? CreateModel(product) : null;
		}

		public void Insert(ProductBindingModel model)
		{
			using var context = new ProductDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				var product = CreateModel(model, new Product());
				context.Products.Add(product);
				context.SaveChanges();
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public void Update(ProductBindingModel model)
		{
			using var context = new ProductDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				var product = context.Products.FirstOrDefault(rec => rec.Id == model.Id);
				if (product == null)
				{
					throw new Exception("Продукт не найден");
				}
				CreateModel(model, product);
				context.SaveChanges();
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public void Delete(ProductBindingModel model)
		{
			using var context = new ProductDatabase();
			var product = context.Products.FirstOrDefault(rec => rec.Id == model.Id);

			if (product != null)
			{
				context.Products.Remove(product);
				context.SaveChanges();
			}
			else
			{
				throw new Exception("Продукт не найден");
			}
		}
	}
}
