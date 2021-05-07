using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UFX.ExcelIE.Domain.Models;

#nullable disable

namespace UFX.ExcelIE.Domain
{
    public partial class UFX_MASTERContext : DbContext
    {
        public UFX_MASTERContext()
        {
        }

        public UFX_MASTERContext(DbContextOptions<UFX_MASTERContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AmTenant> AmTenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<AmTenant>(entity =>
            {
                entity.HasKey(e => e.TntCode)
                    .HasName("PK_WMS_Tenant");

                entity.ToTable("AM_Tenant");

                entity.Property(e => e.TntCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("企业号");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("创建人");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("修改人");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasComment("RowVersion");

                entity.Property(e => e.TntContact)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("联系人");

                entity.Property(e => e.TntDbStr).HasMaxLength(200);

                entity.Property(e => e.TntDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("企业描述");

                entity.Property(e => e.TntEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("邮箱");

                entity.Property(e => e.TntExpire)
                    .HasColumnType("datetime")
                    .HasComment("到期日");

                entity.Property(e => e.TntName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("企业名称");

                entity.Property(e => e.TntPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("电话");

                entity.Property(e => e.TntPrice).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
