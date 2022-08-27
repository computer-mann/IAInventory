using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IAInventory.Models;

namespace IAInventory.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<IAInventory.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<IAInventory.Models.Bill> Bill { get; set; }

        public DbSet<IAInventory.Models.BillType> BillType { get; set; }

        public DbSet<IAInventory.Models.Branch> Branch { get; set; }

        public DbSet<IAInventory.Models.CashBank> CashBank { get; set; }

        public DbSet<IAInventory.Models.Currency> Currency { get; set; }

        public DbSet<IAInventory.Models.Customer> Customer { get; set; }

        public DbSet<IAInventory.Models.CustomerType> CustomerType { get; set; }

        public DbSet<IAInventory.Models.GoodsReceivedNote> GoodsReceivedNote { get; set; }

        public DbSet<IAInventory.Models.Invoice> Invoice { get; set; }

        public DbSet<IAInventory.Models.InvoiceType> InvoiceType { get; set; }

        public DbSet<IAInventory.Models.NumberSequence> NumberSequence { get; set; }

        public DbSet<IAInventory.Models.PaymentReceive> PaymentReceive { get; set; }

        public DbSet<IAInventory.Models.PaymentType> PaymentType { get; set; }

        public DbSet<IAInventory.Models.PaymentVoucher> PaymentVoucher { get; set; }

        public DbSet<IAInventory.Models.Product> Product { get; set; }

        public DbSet<IAInventory.Models.ProductType> ProductType { get; set; }

        public DbSet<IAInventory.Models.PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<IAInventory.Models.PurchaseOrderLine> PurchaseOrderLine { get; set; }

        public DbSet<IAInventory.Models.PurchaseType> PurchaseType { get; set; }

        public DbSet<IAInventory.Models.SalesOrder> SalesOrder { get; set; }

        public DbSet<IAInventory.Models.SalesOrderLine> SalesOrderLine { get; set; }

        public DbSet<IAInventory.Models.SalesType> SalesType { get; set; }

        public DbSet<IAInventory.Models.Shipment> Shipment { get; set; }

        public DbSet<IAInventory.Models.ShipmentType> ShipmentType { get; set; }

        public DbSet<IAInventory.Models.UnitOfMeasure> UnitOfMeasure { get; set; }

        public DbSet<IAInventory.Models.Vendor> Vendor { get; set; }

        public DbSet<IAInventory.Models.VendorType> VendorType { get; set; }

        public DbSet<IAInventory.Models.Warehouse> Warehouse { get; set; }

        public DbSet<IAInventory.Models.UserProfile> UserProfile { get; set; }
    }
}
