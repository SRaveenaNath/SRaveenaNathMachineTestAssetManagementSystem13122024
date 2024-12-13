using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SRaveenaNathMachineTestAssetManagementSystem.Model;

public partial class MachineTestDbContext : DbContext
{
    public MachineTestDbContext()
    {
    }

    public MachineTestDbContext(DbContextOptions<MachineTestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetDefinition> AssetDefinitions { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =DESKTOP-9KHVLG5\\SQLEXPRESS; Initial Catalog = MachineTestDB; Integrated Security = True;\nTrusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Assets__434923728099D405");

            entity.HasIndex(e => e.SerialNumber, "UQ__Assets__048A0008B8E85A4D").IsUnique();

            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.AssetDefinitionId).HasColumnName("AssetDefinitionID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");
            entity.Property(e => e.SerialNumber).HasMaxLength(100);

            entity.HasOne(d => d.AssetDefinition).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AssetDefinitionId)
                .HasConstraintName("FK__Assets__AssetDef__4CA06362");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.Assets)
                .HasForeignKey(d => d.PurchaseOrderId)
                .HasConstraintName("FK__Assets__Purchase__4D94879B");
        });

        modelBuilder.Entity<AssetDefinition>(entity =>
        {
            entity.HasKey(e => e.AssetDefinitionId).HasName("PK__AssetDef__02A2EFA52AF43403");

            entity.Property(e => e.AssetDefinitionId).HasColumnName("AssetDefinitionID");
            entity.Property(e => e.AssetTypeId).HasColumnName("AssetTypeID");
            entity.Property(e => e.DefinitionName).HasMaxLength(50);

            entity.HasOne(d => d.AssetType).WithMany(p => p.AssetDefinitions)
                .HasForeignKey(d => d.AssetTypeId)
                .HasConstraintName("FK__AssetDefi__Asset__412EB0B6");
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.HasKey(e => e.AssetTypeId).HasName("PK__AssetTyp__FD33C222A984B95E");

            entity.HasIndex(e => e.TypeName, "UQ__AssetTyp__D4E7DFA8ACB17C8F").IsUnique();

            entity.Property(e => e.AssetTypeId).HasColumnName("AssetTypeID");
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PK__Purchase__036BAC44639E2DCA");

            entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");
            entity.Property(e => e.AssetDefinitionId).HasColumnName("AssetDefinitionID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.VendorId).HasColumnName("VendorID");

            entity.HasOne(d => d.AssetDefinition).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.AssetDefinitionId)
                .HasConstraintName("FK__PurchaseO__Asset__47DBAE45");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK__PurchaseO__Vendo__46E78A0C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A462F0438");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160A951E7A0").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE2D808B0");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4B547F0CF").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__3A81B327");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendors__FC8618D3351C91F4");

            entity.HasIndex(e => e.VendorName, "UQ__Vendors__7320A35765774D45").IsUnique();

            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.VendorName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
