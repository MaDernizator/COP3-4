using ProductContract.BindingModels;
using ProductContract.StorageContracts;
using ProductContract.ViewModels;
using ProductDatabaseImplement.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductDatabaseImplement.Implements
{
	public class UnitStorage : IUnitStorage
	{
		private Unit CreateModel(UnitBindingModel model, Unit unit)
		{
			unit.Name = model.Name;
			return unit;
		}

		private UnitViewModel CreateModel(Unit unit)
		{
			return new UnitViewModel
			{
				Id = unit.Id,
				Name = unit.Name
			};
		}

		public List<UnitViewModel> GetFullList()
		{
			using var context = new ProductDatabase();
			return context.Units.ToList().Select(CreateModel).ToList();
		}

		public List<UnitViewModel> GetFilterList(UnitBindingModel model)
		{
			using var context = new ProductDatabase();
			return context.Units
				.Where(unit => unit.Name.Contains(model.Name))
				.ToList()
				.Select(CreateModel)
				.ToList();
		}

		public UnitViewModel? GetElement(UnitBindingModel model)
		{
			using var context = new ProductDatabase();
			var unit = context.Units.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
			return unit != null ? CreateModel(unit) : null;
		}

		public void Insert(UnitBindingModel model)
		{
			using var context = new ProductDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				context.Units.Add(CreateModel(model, new Unit()));
				context.SaveChanges();
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public void Update(UnitBindingModel model)
		{
			using var context = new ProductDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				var unit = context.Units.FirstOrDefault(rec => rec.Id == model.Id);
				if (unit == null)
				{
					throw new Exception("Единица измерения не найдена");
				}
				CreateModel(model, unit);
				context.SaveChanges();
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public void Delete(UnitBindingModel model)
		{
			using var context = new ProductDatabase();
			var unit = context.Units.FirstOrDefault(rec => rec.Id == model.Id);
			if (unit != null)
			{
				context.Units.Remove(unit);
				context.SaveChanges();
			}
			else
			{
				throw new Exception("Единица измерения не найдена");
			}
		}
	}
}
