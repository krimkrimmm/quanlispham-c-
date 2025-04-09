using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMDT.Models;

namespace TMDT.Data
{
    public class TMDTDbContext : DbContext
    {
        public TMDTDbContext (DbContextOptions<TMDTDbContext> options)
            : base(options)
        {
        }
        public DbSet<TMDT.Models.Cart> Cart { get; set; } = default!;
        public DbSet<TMDT.Models.Categories> Categories { get; set; } = default!;
        public DbSet<TMDT.Models.Products> Products { get; set; } = default!;
        public DbSet<TMDT.Models.ProductOrders> ProductOrders { get; set; } = default!;
        public DbSet<TMDT.Models.ProductReviews> ProductReviews { get; set; } = default!;
        public DbSet<TMDT.Models.PaymentInformation> PaymentInformation { get; set; } = default!;
        public DbSet<TMDT.Models.PaymentResult> PaymentResult { get; set; } = default!;
        public DbSet<TMDT.Models.Order> Order { get; set; } = default!;
        public DbSet<TMDT.Models.Whitelist> Whilelist { get; set; } = default!;
        public DbSet<TMDT.Models.PaymentInformationViewModel> PaymentInformationViewModel { get; set; } = default!;
        public DbSet<TMDT.Models.Donhang> Donhang { get; set; } = default!;
        public DbSet<TMDT.Models.CustomerBehaviors> CustomerBehaviors { get; set; } = default!;
        public DbSet<TMDT.Models.Voucher> Voucher { get; set; } = default!;
        public DbSet<TMDT.Models.City> Cities { get; set; } = default!;
        public DbSet<TMDT.Models.Districts> Districts { get; set; } = default!;

    }
}
