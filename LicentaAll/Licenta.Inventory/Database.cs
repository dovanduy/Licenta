﻿

// ------------------------------------------------------------------------------------------------
// This code was generated by EntityFramework Reverse POCO Generator (http://www.reversepoco.com/).
// Created by Simon Hughes (https://about.me/simon.hughes).
//
// Do not make changes directly to this file - edit the template instead.
//
// The following connection settings were used to generate this file:
//     Configuration file:     "Licenta.Inventory\App.config"
//     Connection String Name: "Database"
//     Connection String:      "Data Source=STEFAN-LT\SQLEXPRESS;Initial Catalog=LicentaInventory;Integrated Security=True"
// ------------------------------------------------------------------------------------------------
// Database Edition       : Express Edition (64-bit)
// Database Engine Edition: Express

// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Licenta.Inventory
{
    using Newtonsoft.Json;

    #region Unit of work

    public interface IInventoryDbContext : EntityFramework.UnitOfWork.Interfaces.IDbContext
    {
        System.Data.Entity.DbSet<Product> Products { get; set; } // Product

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
    }

    #endregion

    #region Database context

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public class InventoryDbContext : System.Data.Entity.DbContext, IInventoryDbContext
    {
        public System.Data.Entity.DbSet<Product> Products { get; set; } // Product

        static InventoryDbContext()
        {
            System.Data.Entity.Database.SetInitializer<InventoryDbContext>(null);
        }

        public InventoryDbContext()
            : base("Name=Database")
        {
        }

        public InventoryDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public InventoryDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        public InventoryDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public InventoryDbContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public bool IsSqlParameterNull(System.Data.SqlClient.SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as System.Data.SqlTypes.INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == System.DBNull.Value);
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ProductConfiguration());
        }

        public static System.Data.Entity.DbModelBuilder CreateModel(System.Data.Entity.DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration(schema));
            return modelBuilder;
        }
    }
    #endregion

    #region POCO classes

    // Product
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public class Product: EntityFramework.IMaintainableEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public int Items { get; set; } // Items
        public int RowVersion { get; set; } // Row_Version
        public System.DateTime? DateDeleted { get; set; } // Date_Deleted

        public Product()
        {
            RowVersion = 1;
        }
    }

    #endregion

    #region POCO Configuration

    // Product
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public class ProductConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
            : this("dbo")
        {
        }

        public ProductConfiguration(string schema)
        {
            ToTable("Product", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Items).HasColumnName(@"Items").IsRequired().HasColumnType("int");
            Property(x => x.RowVersion).HasColumnName(@"Row_Version").IsRequired().HasColumnType("int");
            Property(x => x.DateDeleted).HasColumnName(@"Date_Deleted").IsOptional().HasColumnType("date");
        }
    }

    #endregion

}
// </auto-generated>

