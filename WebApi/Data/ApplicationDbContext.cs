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
        // Disable lazy loading
        //optionsBuilder.UseLazyLoadingProxies(false);

        // Enable eager loading for all navigation properties
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

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

            // entity.HasOne(c => c.ApplicationUser)
            //    .WithOne()
            //    .HasForeignKey<Customer>(c => c.ApplicationUser);

            // entity.HasOne(c => c.Address)
            //     .WithOne()
            //     .HasForeignKey<Customer>(c => c.Address);

            // entity.HasOne(c => c.PhoneNumber)
            //    .WithOne()
            //    .HasForeignKey<Customer>(c => c.PhoneNumber);

            // entity.HasMany(c => c.Orders)
            //   .WithOne(o => o.Customer)
            //   .HasForeignKey(o => o.Customer);
        });


        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("EmployeeId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            // entity.HasOne(e => e.ApplicationUser)
            //      .WithOne()
            //      .HasForeignKey<Employee>(e => e.ApplicationUser);

            // entity.HasOne(c => c.Address)
            //     .WithOne()
            //     .HasForeignKey<Employee>(c => c.Address);

            // entity.HasOne(c => c.PhoneNumber)
            //     .WithOne()
            //     .HasForeignKey<Employee>(c => c.PhoneNumber);

            // entity.HasMany(c => c.Orders)
            //   .WithOne(o => o.Employee)
            //   .HasForeignKey(o => o.Employee);

            // entity.HasOne(d => d.ReportsTo)
            //     .WithMany(p => p.InverseReportsToNavigation)
            //     .HasForeignKey(d => d.ReportsTo);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(o => o.Id)
                .HasColumnName("OrderId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            // entity.HasOne(c => c.ShippingAddress)
            //     .WithOne()
            //     .HasForeignKey<Order>(o => o.ShippingAddress);

            // entity.HasOne(d => d.Customer)
            //     .WithMany(p => p.Orders)
            //     .HasForeignKey(d => d.Customer);

            // entity.HasOne(d => d.Employee)
            //     .WithMany(p => p.Orders)
            //     .HasForeignKey(d => d.Employee);

            // entity.HasMany(o => o.OrderDetails)
            //     .WithOne(od => od.Order)
            //     .HasForeignKey(od => od.Order);
        });


        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(od => od.Id)
                .HasColumnName("OrderDetailId")
                .HasColumnType("BIGINT")
                .UseIdentityColumn();

            // entity.HasOne(od => od.Product)
            //    .WithMany(p => p.OrderDetails)
            //    .HasForeignKey(od => od.Product);

            // entity.HasOne(od => od.Order)
            //     .WithMany(o => o.OrderDetails)
            //     .HasForeignKey(od => od.Order);
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