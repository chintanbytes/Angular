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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

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
            .WithOne(u => u.Customer)
            .HasForeignKey<Customer>(c => c.ApplicationUserId);

            entity.HasOne(c => c.Address)
            .WithOne(a => a.Customer)
            .HasForeignKey<Customer>(c => c.AddressId);

            entity.HasOne(c => c.PhoneNumber)
            .WithOne(a => a.Customer)
            .HasForeignKey<Customer>(c => c.PhoneNumberId);

        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("EmployeeId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            entity.HasOne(e => e.ApplicationUser)
            .WithOne(u => u.Employee)
            .HasForeignKey<Employee>(e => e.ApplicationUserId);

            entity.HasOne(e => e.Address)
            .WithOne(a => a.Employee)
            .HasForeignKey<Employee>(e => e.AddressId);

            entity.HasOne(e => e.PhoneNumber)
            .WithOne(a => a.Employee)
            .HasForeignKey<Employee>(e => e.PhoneNumberId);

        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(o => o.Id)
                .HasColumnName("OrderId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            entity.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

            entity.HasOne(o => o.Employee)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.EmployeeId);

            entity.HasOne(o => o.ShippingAddress)
            .WithOne(a => a.Order)
            .HasForeignKey<Order>(o => o.ShippingAddressId);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(od => od.Id)
                .HasColumnName("OrderDetailId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

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