﻿// <auto-generated />
using System;
using EFCoreTestApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreTestApi.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    partial class MySqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EFCoreTestApi.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasComment("姓名");

                    b.Property<Guid?>("UserDetailId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Username")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasComment("用户名");

                    b.HasKey("Id");

                    b.HasIndex("UserDetailId");

                    b.ToTable("SysUser");
                });

            modelBuilder.Entity("EFCoreTestApi.UserDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasComment("邮箱");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)")
                        .HasComment("手机");

                    b.HasKey("Id");

                    b.ToTable("SysUserDetail");
                });

            modelBuilder.Entity("EFCoreTestApi.User", b =>
                {
                    b.HasOne("EFCoreTestApi.UserDetail", "UserDetail")
                        .WithMany()
                        .HasForeignKey("UserDetailId");

                    b.Navigation("UserDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
