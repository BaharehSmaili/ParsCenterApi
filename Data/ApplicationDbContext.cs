using Common.Utilities;
using Entities.Models.Basic;
using Entities.Models.BasicInformation;
using Entities.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        ////ساخت کاناستراکتور و سازنده برای استفاده ار استارت آپ پروژه اصلی
        //public ApplicationDbContext(DbContextOptions options) :base(options)
        //{

        //}

        //در صورتی که از این حالت برای ایجاد پرووایدر استفاده کنیم نباید از کاناستراکتور و استارت اپ استفاده نماییم.
        private const string connectionsString = "Data Source=.;Initial Catalog=ParsCenter;User ID=sa;password=sa@123456;MultipleActiveResultSets=true;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False;Persist Security Info=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionsString);
            base.OnConfiguring(optionsBuilder);
        }

        //وظیفه این متد ساختن جدول از روی مدل هاست
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);

            //روش قدیم برای ادد کردن کانفینگ ها
            //modelBuilder.ApplyConfiguration<CityConfiguration>(IEntityTypeConfiguration<IEntity> configuration);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);

            //برای اینکه جلوگیری کینم از حذف وابستگی ها در جداول از این متد استفاده می کنیم
            modelBuilder.AddRestrictDeleteBehaviorConvention();

            //برای این که نوع داده جیوایدی قابل ایندکس گذاری باشه از متد زیر استفاده می کنیم
            modelBuilder.AddSequentialGuidForIdConvention();

            //برای تعریف مقدار اولیه در دیتابیس از حالت زیر به عنوان مثال می توانیم استفاده کنیم
            modelBuilder.Entity<Country>().Property(p => p.Id).UseIdentityColumn(1, 1);
            modelBuilder.Entity<State>().Property(p => p.Id).UseIdentityColumn(1, 1);
            modelBuilder.Entity<City>().Property(p => p.Id).UseIdentityColumn(1, 1);

            ////برای ایجاد جدول ها به صورت اسم به صورت منفرد
            //modelBuilder.AddSingularizingTableNameConvention();

            ////برای ایجاد جدول ها به صورت اسم به صورت جمع
            //modelBuilder.AddPluralizingTableNameConvention();
        }

        //نحوه تعریف قدیمی به جای این از اورلود آن مدل کریت استفاده می کنیم
        //public DbSet<CountryModel> Country { get; set; }
        //public DbSet<StateModel> State { get; set; }
        //public DbSet<CityModel> City { get; set; }
        //public DbSet<UserModel> User { get; set; }

        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
    }
}
