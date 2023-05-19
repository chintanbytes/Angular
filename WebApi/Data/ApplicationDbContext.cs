using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyShop.WebApi.Data;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(c => c.Id)
                .HasColumnName("CustomerId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            entity.HasOne(c => c.ApplicationUser)
               .WithOne()
               .HasForeignKey<Customer>(c => c.ApplicationUserId);

            entity.HasOne(c => c.Address)
                .WithOne()
                .HasForeignKey<Customer>(c => c.AddressId);

            entity.HasOne(c => c.PhoneNumber)
               .WithOne()
               .HasForeignKey<Customer>(c => c.PhoneNumberId);

            entity.HasMany(c => c.Orders)
              .WithOne(o => o.Customer)
              .HasForeignKey(o => o.CustomerId);
        });


        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("EmployeeId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            entity.HasOne(e => e.ApplicationUser)
                 .WithOne()
                 .HasForeignKey<Employee>(e => e.ApplicationUserId);

            entity.HasOne(c => c.Address)
                .WithOne()
                .HasForeignKey<Employee>(c => c.AddressId);

            entity.HasOne(c => c.PhoneNumber)
                .WithOne()
                .HasForeignKey<Employee>(c => c.PhoneNumberId);

            entity.HasMany(c => c.Orders)
              .WithOne(o => o.Employee)
              .HasForeignKey(o => o.EmployeeId);

            entity.HasOne(d => d.ReportsToNavigation)
                .WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(o => o.Id)
                .HasColumnName("OrderId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            entity.HasOne(c => c.ShippingAddress)
                .WithOne()
                .HasForeignKey<Order>(o => o.ShippingAddressId);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId);

            entity.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);
        });


        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(od => od.Id)
                .HasColumnName("OrderDetailId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            entity.HasOne(od => od.Product)
               .WithMany(p => p.OrderDetails)
               .HasForeignKey(od => od.ProductId);

            entity.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Id)
                .HasColumnName("ProductId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();
        });


        modelBuilder.Entity<Address>(entity =>
        {

            entity.Property(a => a.Id)
                .HasColumnName("AddressId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();
        });

        modelBuilder.Entity<PhoneNumber>(entity =>
        {
            entity.Property(pn => pn.Id)
                .HasColumnName("PhoneNumberId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}