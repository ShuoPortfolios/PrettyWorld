﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrettyWorld.Models;

#nullable disable

namespace PrettyWorld.Migrations
{
    [DbContext(typeof(PrettyWorldContext))]
    partial class PrettyWorldContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PrettyWorld.Models.MyProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("ID");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Ajaxlevel")
                        .HasColumnType("int")
                        .HasColumnName("AJAXLevel");

                    b.Property<int>("BootstrapLevel")
                        .HasColumnType("int");

                    b.Property<int>("CsharpLevel")
                        .HasColumnType("int")
                        .HasColumnName("CSharpLevel");

                    b.Property<int>("Csslevel")
                        .HasColumnType("int")
                        .HasColumnName("CSSLevel");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FacebookUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("GitLevel")
                        .HasColumnType("int");

                    b.Property<string>("GithubUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Htmllevel")
                        .HasColumnType("int")
                        .HasColumnName("HTMLLevel");

                    b.Property<string>("InstagramUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Introduction")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("JavaLevel")
                        .HasColumnType("int");

                    b.Property<int>("JavascriptLevel")
                        .HasColumnType("int");

                    b.Property<string>("LiveIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<int>("Mssqllevel")
                        .HasColumnType("int")
                        .HasColumnName("MSSQLLevel");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("ProfilePicture")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PythonLevel")
                        .HasColumnType("int");

                    b.Property<string>("TwitterUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("WebsiteUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("MyProfile", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
