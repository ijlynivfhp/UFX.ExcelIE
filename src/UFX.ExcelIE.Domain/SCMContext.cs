﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UFX.ExcelIE.Domain.Models;
using UFX.ExcelIE.Domain.Models;

#nullable disable

namespace UFX.ExcelIE.Domain
{
    public partial class SCMContext : DbContext
    {
        public SCMContext(DbContextOptions<SCMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CoExcelExportSql> CoExcelExportSqls { get; set; }
        public virtual DbSet<CoExcelExportSqllog> CoExcelExportSqllogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<CoExcelExportSql>(entity =>
            {
                entity.ToTable("CO_ExcelExportSQL");

                entity.HasComment("Excel导出SQL模板");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("主键");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasComment("创建人");

                entity.Property(e => e.ExecSql)
                    .HasMaxLength(4000)
                    .HasColumnName("ExecSQL")
                    .HasComment("执行SQL");

                entity.Property(e => e.IsDelete).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(50)
                    .HasComment("修改人");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasComment("行版本号");

                entity.Property(e => e.SourceType)
                    .HasDefaultValueSql("((0))")
                    .HasComment("来源类型");

                entity.Property(e => e.SourceUrl)
                    .HasMaxLength(500)
                    .HasComment("执行路径");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.TemplateCode)
                    .HasMaxLength(50)
                    .HasComment("模板编号");

                entity.Property(e => e.TemplateName)
                    .HasMaxLength(200)
                    .HasComment("模板名称");

                entity.Property(e => e.TemplateType).HasComment("模板类型");

                entity.Property(e => e.TemplateUrl)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("模板附件Url");
            });

            modelBuilder.Entity<CoExcelExportSqllog>(entity =>
            {
                entity.ToTable("CO_ExcelExportSQLLog");

                entity.HasComment("Excel导出日志");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("主键");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasComment("创建人");

                entity.Property(e => e.CreateUserId).HasComment("创建人Id");

                entity.Property(e => e.ExecCount).HasComment("执行次数");

                entity.Property(e => e.ExportDuration).HasComment("导出时间（秒）");

                entity.Property(e => e.ExportMsg)
                    .HasMaxLength(2000)
                    .HasComment("导出结果");

                entity.Property(e => e.ExportParameters)
                    .HasMaxLength(200)
                    .HasComment("导出参数");

                entity.Property(e => e.ExportSql)
                    .HasMaxLength(4000)
                    .HasColumnName("ExportSQL")
                    .HasComment("导出实际SQL");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(50)
                    .HasComment("修改人");

                entity.Property(e => e.ParentId).HasComment("模板Id");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasComment("行版本号");

                entity.Property(e => e.Status).HasComment("导出状态");

                entity.Property(e => e.TemplateSql)
                    .HasMaxLength(4000)
                    .HasColumnName("TemplateSQL")
                    .HasComment("导出模板SQL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
