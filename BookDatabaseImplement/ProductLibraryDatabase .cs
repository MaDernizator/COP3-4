using ProductDatabaseImplement.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace ProductDatabaseImplement
{
	public class ProductDatabase : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured == false)
			{
				optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=ProductStore_db;Username=postgres;Password=password");
			}
			base.OnConfiguring(optionsBuilder);
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
		}

		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Unit> Units { get; set; }
	}
}
