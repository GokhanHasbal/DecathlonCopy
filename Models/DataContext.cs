using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Decathlon.Models
{
    public class DataContext:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DataContext() : base("dataContext") { }

        public DbSet<Address> Address { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Campaing> Campaing { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<ContactMail> ContactMail { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<NewsGet> NewsGet { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductComment> ProductComment { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Seo> Seo { get; set; }
        public DbSet<SiteComment> SiteComment { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Support> Support { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
    }
}