using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RSSServer.Models;

namespace RSSServer.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RSSServer.Models.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CollectionId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("RSSServer.Models.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerLogin")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OwnerLogin");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("RSSServer.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.HasKey("Login");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RSSServer.Models.Channel", b =>
                {
                    b.HasOne("RSSServer.Models.Collection", "Collection")
                        .WithMany("Channels")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RSSServer.Models.Collection", b =>
                {
                    b.HasOne("RSSServer.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerLogin")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
