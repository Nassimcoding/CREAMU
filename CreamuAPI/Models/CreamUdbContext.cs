using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CreamuAPI.Models;

public partial class CreamUdbContext : DbContext
{
    public CreamUdbContext()
    {
    }

    public CreamUdbContext(DbContextOptions<CreamUdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CombineDetail> CombineDetails { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<CreditcardInfo> CreditcardInfos { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Orderimg> Orderimgs { get; set; }

    public virtual DbSet<PostAddress> PostAddresses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TempOrderDetail> TempOrderDetails { get; set; }

    public virtual DbSet<TrackingList> TrackingLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=45.32.31.135;port=33060;database=CreamUDB;user=CreamU;password=creamu133", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.EmailId).HasName("PRIMARY");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("Category");

            entity.HasIndex(e => e.CategoryId, "CategoryID").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Category1)
                .HasMaxLength(30)
                .HasColumnName("Category");
        });

        modelBuilder.Entity<CombineDetail>(entity =>
        {
            entity.HasKey(e => e.CombineId).HasName("PRIMARY");

            entity.ToTable("CombineDetail");

            entity.HasIndex(e => e.Cbody, "FK_CombineDetail_CBody_idx");

            entity.HasIndex(e => e.Chead, "FK_CombineDetail_CHead_idx");

            entity.HasIndex(e => e.Clfoot, "FK_CombineDetail_CLFoot_idx");

            entity.HasIndex(e => e.Clhand, "FK_CombineDetail_CLHand_idx");

            entity.HasIndex(e => e.Crfoot, "FK_CombineDetail_CRFoot_idx");

            entity.HasIndex(e => e.Crhand, "FK_CombineDetail_CRHand_idx");

            entity.Property(e => e.CombineId).HasColumnName("CombineID");
            entity.Property(e => e.Cbody).HasColumnName("CBody");
            entity.Property(e => e.Chead).HasColumnName("CHead");
            entity.Property(e => e.Clfoot).HasColumnName("CLFoot");
            entity.Property(e => e.Clhand).HasColumnName("CLHand");
            entity.Property(e => e.Crfoot).HasColumnName("CRFoot");
            entity.Property(e => e.Crhand).HasColumnName("CRHand");
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.CbodyNavigation).WithMany(p => p.CombineDetailCbodyNavigations)
                .HasForeignKey(d => d.Cbody)
                .HasConstraintName("FK_CombineDetail_CBody");

            entity.HasOne(d => d.CheadNavigation).WithMany(p => p.CombineDetailCheadNavigations)
                .HasForeignKey(d => d.Chead)
                .HasConstraintName("FK_CombineDetail_CHead");

            entity.HasOne(d => d.ClfootNavigation).WithMany(p => p.CombineDetailClfootNavigations)
                .HasForeignKey(d => d.Clfoot)
                .HasConstraintName("FK_CombineDetail_CLFoot");

            entity.HasOne(d => d.ClhandNavigation).WithMany(p => p.CombineDetailClhandNavigations)
                .HasForeignKey(d => d.Clhand)
                .HasConstraintName("FK_CombineDetail_CLHand");

            entity.HasOne(d => d.CrfootNavigation).WithMany(p => p.CombineDetailCrfootNavigations)
                .HasForeignKey(d => d.Crfoot)
                .HasConstraintName("FK_CombineDetail_CRFoot");

            entity.HasOne(d => d.CrhandNavigation).WithMany(p => p.CombineDetailCrhandNavigations)
                .HasForeignKey(d => d.Crhand)
                .HasConstraintName("FK_CombineDetail_CRHand");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PRIMARY");

            entity.ToTable("Component");

            entity.HasIndex(e => e.MaterialId, "FK_Component_Material");

            entity.HasIndex(e => e.ModelId, "FK_Component_Models");

            entity.Property(e => e.ComponentId)
                .ValueGeneratedNever()
                .HasColumnName("ComponentID");
            entity.Property(e => e.ComFileName).HasMaxLength(50);
            entity.Property(e => e.IsSupply).HasColumnName("isSupply");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.ModelType).HasMaxLength(20);

            entity.HasOne(d => d.Material).WithMany(p => p.Components)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Component_Material");

            entity.HasOne(d => d.Model).WithMany(p => p.Components)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Component_Models");
        });

        modelBuilder.Entity<CreditcardInfo>(entity =>
        {
            entity.HasKey(e => e.CreditCardId).HasName("PRIMARY");

            entity.ToTable("CreditcardInfo");

            entity.HasIndex(e => e.MemberId, "fk_CreditcardInfo_MemberID_to_MemberID");

            entity.Property(e => e.CreditCardId).HasColumnName("CreditCardID");
            entity.Property(e => e.CardNumber).HasMaxLength(50);
            entity.Property(e => e.CardType).HasMaxLength(50);
            entity.Property(e => e.CardholderName)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Cvv)
                .HasMaxLength(10)
                .HasColumnName("CVV");
            entity.Property(e => e.IsDefault).HasColumnType("bit(1)");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Member).WithMany(p => p.CreditcardInfos)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_CreditcardInfo_MemberID_to_MemberID");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.HasIndex(e => e.EmailId, "EmailID");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Telephone).HasMaxLength(20);
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Email).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Employees_ibfk_1");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PRIMARY");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId)
                .ValueGeneratedNever()
                .HasColumnName("MaterialID");
            entity.Property(e => e.AddTime).HasColumnType("datetime");
            entity.Property(e => e.Info)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IsSupply).HasColumnName("isSupply");
            entity.Property(e => e.MaterialName).HasMaxLength(20);
            entity.Property(e => e.MtrlFileName).HasMaxLength(50);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PRIMARY");

            entity.HasIndex(e => e.EmailId, "EmailID");

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Telephone).HasMaxLength(20);

            entity.HasOne(d => d.Email).WithMany(p => p.Members)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Members_ibfk_1");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PRIMARY");

            entity.HasIndex(e => e.EmployeeId, "EmployeeID");

            entity.HasIndex(e => e.MemberId, "MemberID");

            entity.Property(e => e.MessageId)
                .ValueGeneratedNever()
                .HasColumnName("MessageID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IsReply).HasColumnName("isReply");
            entity.Property(e => e.IsShow).HasColumnName("isShow");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.MessageContext)
                .HasMaxLength(500)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.MessageTime).HasColumnType("datetime");
            entity.Property(e => e.ReplyContext)
                .HasMaxLength(500)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ReplyTime).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Messages)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("Messages_ibfk_2");

            entity.HasOne(d => d.Member).WithMany(p => p.Messages)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("Messages_ibfk_1");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PRIMARY");

            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasColumnName("ModelID");
            entity.Property(e => e.AddTime).HasColumnType("datetime");
            entity.Property(e => e.Info)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IsSupply).HasColumnName("isSupply");
            entity.Property(e => e.ModelFileName).HasMaxLength(50);
            entity.Property(e => e.ModelName).HasMaxLength(20);
            entity.Property(e => e.ModelType).HasMaxLength(20);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PRIMARY");

            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Notes1)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Notes2)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.EmployeeId, "fk_Order_EmployeeID_to_EmployeeID");

            entity.HasIndex(e => e.MemberId, "fk_Order_MemberID_to_MemberID");

            entity.HasIndex(e => e.ShippingAddressId, "fk_Order_ShippingAddressID_to_ShippingAddressID");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNotes)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ShippingAddressId).HasColumnName("ShippingAddressID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("fk_Order_EmployeeID_to_EmployeeID");

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_Order_MemberID_to_MemberID");

            entity.HasOne(d => d.ShippingAddress).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShippingAddressId)
                .HasConstraintName("fk_Order_ShippingAddressID_to_ShippingAddressID");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PRIMARY");

            entity.ToTable("OrderDetail");

            entity.HasIndex(e => e.OrderId, "fk_OrderDetail_OrderID_to_OrderID");

            entity.HasIndex(e => e.ProductId, "fk_OrderDetail_ProductID_to_ProductID");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Qty).HasColumnName("QTY");
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_OrderDetail_OrderID_to_OrderID");
        });

        modelBuilder.Entity<Orderimg>(entity =>
        {
            entity.HasKey(e => e.ImgId).HasName("PRIMARY");

            entity.ToTable("Orderimg");

            entity.HasIndex(e => e.OrderId, "fk_Orderimg_OrderID_to_OrderID");

            entity.Property(e => e.ImgId).HasColumnName("ImgID");
            entity.Property(e => e.FPath)
                .HasMaxLength(200)
                .HasColumnName("fPath");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderimgs)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_Orderimg_OrderID_to_OrderID");
        });

        modelBuilder.Entity<PostAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("PostAddress");

            entity.HasIndex(e => e.MemberId, "fk_PostAddress_MemberID_to_MemberID");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AddressType).HasMaxLength(20);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IsDefault).HasColumnType("bit(1)");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.RecipientName)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Member).WithMany(p => p.PostAddresses)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_PostAddress_MemberID_to_MemberID");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "fk_product_category");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Descript)
                .HasMaxLength(500)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProductStatus)
                .HasMaxLength(30)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ReleaseDate)
                .HasMaxLength(30)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate)
                .HasMaxLength(30)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_product_category");
        });

        modelBuilder.Entity<TempOrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PRIMARY");

            entity.ToTable("TempOrderDetail");

            entity.HasIndex(e => e.MemberId, "fk_TempOrderDetail_MemberID_to_MemberID");

            entity.HasIndex(e => e.ProductId, "fk_TempOrderDetail_ProductID_to_ProductID");

            entity.Property(e => e.OrderDetailId)
                .ValueGeneratedNever()
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Qty).HasColumnName("QTY");

            entity.HasOne(d => d.Member).WithMany(p => p.TempOrderDetails)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_TempOrderDetail_MemberID_to_MemberID");

            entity.HasOne(d => d.Product).WithMany(p => p.TempOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_TempOrderDetail_ProductID_to_ProductID");
        });

        modelBuilder.Entity<TrackingList>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.ProductId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("TrackingList");

            entity.HasIndex(e => e.ProductId, "fk_tracking_product");

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.TrackingDate).HasMaxLength(30);

            entity.HasOne(d => d.Member).WithMany(p => p.TrackingLists)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tracking_member");

            entity.HasOne(d => d.Product).WithMany(p => p.TrackingLists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tracking_product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
