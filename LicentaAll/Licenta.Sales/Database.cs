﻿

// ------------------------------------------------------------------------------------------------
// This code was generated by EntityFramework Reverse POCO Generator (http://www.reversepoco.com/).
// Created by Simon Hughes (https://about.me/simon.hughes).
//
// Do not make changes directly to this file - edit the template instead.
//
// The following connection settings were used to generate this file:
//     Configuration file:     "Licenta.Sales\App.config"
//     Connection String Name: "Database"
//     Connection String:      "Data Source=STEFAN-LT\SQLEXPRESS;Initial Catalog=LicentaSales;Integrated Security=True"
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

namespace Licenta.Sales
{
    using Newtonsoft.Json;

    #region Unit of work

    public interface ISalesDbContext : EntityFramework.UnitOfWork.Interfaces.IDbContext
    {
        System.Data.Entity.DbSet<Product> Products { get; set; } // Product
        System.Data.Entity.DbSet<Sale> Sales { get; set; } // Sale
        System.Data.Entity.DbSet<SaleStatus> SaleStatus { get; set; } // SaleStatusLookup

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
    }

    #endregion

    #region Database context

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public class SalesDbContext : System.Data.Entity.DbContext, ISalesDbContext
    {
        public System.Data.Entity.DbSet<Product> Products { get; set; } // Product
        public System.Data.Entity.DbSet<Sale> Sales { get; set; } // Sale
        public System.Data.Entity.DbSet<SaleStatus> SaleStatus { get; set; } // SaleStatusLookup

        static SalesDbContext()
        {
            System.Data.Entity.Database.SetInitializer<SalesDbContext>(null);
        }

        public SalesDbContext()
            : base("Name=Database")
        {
        }

        public SalesDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public SalesDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        public SalesDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public SalesDbContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
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
            modelBuilder.Configurations.Add(new SaleConfiguration());
            modelBuilder.Configurations.Add(new SaleStatusConfiguration());
        }

        public static System.Data.Entity.DbModelBuilder CreateModel(System.Data.Entity.DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration(schema));
            modelBuilder.Configurations.Add(new SaleConfiguration(schema));
            modelBuilder.Configurations.Add(new SaleStatusConfiguration(schema));
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
        public decimal Price { get; set; } // Price
        public int RowVersion { get; set; } // Row_Version
        public System.DateTime? DateDeleted { get; set; } // Date_Deleted

        public Product()
        {
            RowVersion = 1;
        }
    }

    // Sale
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public class Sale: EntityFramework.IMaintainableEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public int ProductId { get; set; } // ProductId
        public int Items { get; set; } // Items
        public decimal Price { get; set; } // Price
        public int? SpecialOfferId { get; set; } // SpecialOfferId
        public decimal? PercentageDiscount { get; set; } // PercentageDiscount
        public string UserId { get; set; } // UserId (length: 500)
        public int OrderId { get; set; } // OrderId
        public int StatusId { get; set; } // StatusId
        public System.DateTime DateCreated { get; set; } // Date_Created
        public System.DateTime? DateStatusChanged { get; set; } // Date_StatusChanged
        public System.DateTime? DateDeleted { get; set; } // Date_Deleted
        public int RowVersion { get; set; } // Row_Version

        // Foreign keys
        [JsonIgnore]
        public virtual SaleStatus SaleStatus { get; set; } // FK_Sale_SaleStatus

        public Sale()
        {
            StatusId = 1;
            DateCreated = System.DateTime.Now;
            RowVersion = 1;
        }
    }

    // SaleStatusLookup
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public class SaleStatus
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)

        // Reverse navigation
        public virtual System.Collections.Generic.ICollection<Sale> Sales { get; set; } // Sale.FK_Sale_SaleStatus

        public SaleStatus()
        {
            Sales = new System.Collections.Generic.List<Sale>();
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
            Property(x => x.Price).HasColumnName(@"Price").IsRequired().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.RowVersion).HasColumnName(@"Row_Version").IsRequired().HasColumnType("int");
            Property(x => x.DateDeleted).HasColumnName(@"Date_Deleted").IsOptional().HasColumnType("date");
        }
    }

    // Sale
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public class SaleConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Sale>
    {
        public SaleConfiguration()
            : this("dbo")
        {
        }

        public SaleConfiguration(string schema)
        {
            ToTable("Sale", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.ProductId).HasColumnName(@"ProductId").IsRequired().HasColumnType("int");
            Property(x => x.Items).HasColumnName(@"Items").IsRequired().HasColumnType("int");
            Property(x => x.Price).HasColumnName(@"Price").IsRequired().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.SpecialOfferId).HasColumnName(@"SpecialOfferId").IsOptional().HasColumnType("int");
            Property(x => x.PercentageDiscount).HasColumnName(@"PercentageDiscount").IsOptional().HasColumnType("decimal").HasPrecision(4,2);
            Property(x => x.UserId).HasColumnName(@"UserId").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(500);
            Property(x => x.OrderId).HasColumnName(@"OrderId").IsRequired().HasColumnType("int");
            Property(x => x.StatusId).HasColumnName(@"StatusId").IsRequired().HasColumnType("int");
            Property(x => x.DateCreated).HasColumnName(@"Date_Created").IsRequired().HasColumnType("date");
            Property(x => x.DateStatusChanged).HasColumnName(@"Date_StatusChanged").IsOptional().HasColumnType("date");
            Property(x => x.DateDeleted).HasColumnName(@"Date_Deleted").IsOptional().HasColumnType("date");
            Property(x => x.RowVersion).HasColumnName(@"Row_Version").IsRequired().HasColumnType("int");

            // Foreign keys
            HasRequired(a => a.SaleStatus).WithMany(b => b.Sales).HasForeignKey(c => c.StatusId).WillCascadeOnDelete(false); // FK_Sale_SaleStatus
        }
    }

    // SaleStatusLookup
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public class SaleStatusConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<SaleStatus>
    {
        public SaleStatusConfiguration()
            : this("dbo")
        {
        }

        public SaleStatusConfiguration(string schema)
        {
            ToTable("SaleStatusLookup", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName(@"Name").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
        }
    }

    #endregion

}
// </auto-generated>

