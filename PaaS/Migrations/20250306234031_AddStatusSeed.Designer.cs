﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaaS.Data;

#nullable disable

namespace PaaS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250306234031_AddStatusSeed")]
    partial class AddStatusSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PaaS.Models.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdCategory");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            IdCategory = 1,
                            Description = "Specialty Pizzas"
                        },
                        new
                        {
                            IdCategory = 2,
                            Description = "Vegetarian Pizzas"
                        },
                        new
                        {
                            IdCategory = 3,
                            Description = "Appetizers"
                        },
                        new
                        {
                            IdCategory = 4,
                            Description = "Custom"
                        });
                });

            modelBuilder.Entity("PaaS.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CityId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("City");

                    b.HasData(
                        new
                        {
                            CityId = -1,
                            Name = "",
                            ProvinceId = -1
                        },
                        new
                        {
                            CityId = 1,
                            Name = "Vancouver",
                            ProvinceId = 1
                        },
                        new
                        {
                            CityId = 2,
                            Name = "Toronto",
                            ProvinceId = 2
                        });
                });

            modelBuilder.Entity("PaaS.Models.ContactInfo", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address2")
                        .HasColumnType("TEXT");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Phone")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ContactId");

                    b.HasIndex("CityId");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("UserId");

                    b.ToTable("ContactInfo");
                });

            modelBuilder.Entity("PaaS.Models.DeliveryMethod", b =>
                {
                    b.Property<int>("DeliveryMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DeliveryMethodId");

                    b.ToTable("DeliveryMethod");

                    b.HasData(
                        new
                        {
                            DeliveryMethodId = 1,
                            MethodName = "Pickup"
                        },
                        new
                        {
                            DeliveryMethodId = 2,
                            MethodName = "Delivery"
                        });
                });

            modelBuilder.Entity("PaaS.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCategory")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("ItemId");

                    b.HasIndex("IdCategory");

                    b.HasIndex("ItemTypeId");

                    b.ToTable("Item");

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            Description = "Grilled chicken, BBQ sauce, red onions, and cilantro",
                            IdCategory = 1,
                            ImgUrl = "https://plus.unsplash.com/premium_photo-1664472696633-4b0b41e95202?q=80&w=2752&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            ItemTypeId = 1,
                            Name = "BBQ Chicken",
                            Price = 10m
                        },
                        new
                        {
                            ItemId = 2,
                            Description = "Plant-based cheese, mushrooms, peppers, and vegan sausage",
                            IdCategory = 2,
                            ImgUrl = "https://plus.unsplash.com/premium_photo-1722945691819-e58990e7fb27?q=80&w=2821&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            ItemTypeId = 1,
                            Name = "Vegan Delight",
                            Price = 18.99m
                        },
                        new
                        {
                            ItemId = 3,
                            Description = "Breaded mozzarella with marinara sauce",
                            IdCategory = 3,
                            ImgUrl = "https://images.unsplash.com/photo-1708980108318-4b843e243080?q=80&w=2670&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            ItemTypeId = 2,
                            Name = "Mozzarella Sticks",
                            Price = 6m
                        },
                        new
                        {
                            ItemId = 4,
                            Description = "Create your own pizza with your choice of toppings",
                            IdCategory = 4,
                            ItemTypeId = 1,
                            Name = "Custom Pizza",
                            Price = 12m
                        },
                        new
                        {
                            ItemId = 5,
                            Description = "Coca-Cola",
                            IdCategory = 3,
                            ImgUrl = "https://images.unsplash.com/photo-1624552184280-9e9631bbeee9?q=80&w=2787&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            ItemTypeId = 3,
                            Name = "Coke",
                            Price = 4m
                        },
                        new
                        {
                            ItemId = 6,
                            Description = "Pepsi",
                            IdCategory = 3,
                            ImgUrl = "https://images.unsplash.com/photo-1553456558-aff63285bdd1?q=80&w=2787&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            ItemTypeId = 3,
                            Name = "Pepsi",
                            Price = 4m
                        },
                        new
                        {
                            ItemId = 7,
                            Description = "Local craft beer",
                            IdCategory = 3,
                            ImgUrl = "https://images.unsplash.com/photo-1612528443702-f6741f70a049?q=80&w=2680&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            ItemTypeId = 3,
                            Name = "Craft Beer",
                            Price = 6m
                        });
                });

            modelBuilder.Entity("PaaS.Models.ItemType", b =>
                {
                    b.Property<int>("ItemTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ItemTypeId");

                    b.ToTable("ItemType");

                    b.HasData(
                        new
                        {
                            ItemTypeId = 1,
                            Description = "Pizza"
                        },
                        new
                        {
                            ItemTypeId = 2,
                            Description = "Slide"
                        },
                        new
                        {
                            ItemTypeId = 3,
                            Description = "Drink"
                        });
                });

            modelBuilder.Entity("PaaS.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeliveryMethodId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentMethodId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("OrderId");

                    b.HasIndex("DeliveryMethodId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("PaaS.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Size")
                        .HasColumnType("TEXT");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("PaaS.Models.PayPalConfirmationModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PayerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("PayPalConfirmationModel");
                });

            modelBuilder.Entity("PaaS.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethod");

                    b.HasData(
                        new
                        {
                            PaymentMethodId = 1,
                            MethodName = "PayPal"
                        });
                });

            modelBuilder.Entity("PaaS.Models.Province", b =>
                {
                    b.Property<int>("ProvinceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProvinceId");

                    b.ToTable("Province");

                    b.HasData(
                        new
                        {
                            ProvinceId = -1,
                            Name = ""
                        },
                        new
                        {
                            ProvinceId = 1,
                            Name = "British Columbia"
                        },
                        new
                        {
                            ProvinceId = 2,
                            Name = "Ontario"
                        });
                });

            modelBuilder.Entity("PaaS.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Description = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            Description = "Manager"
                        },
                        new
                        {
                            RoleId = 3,
                            Description = "Customer"
                        });
                });

            modelBuilder.Entity("PaaS.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StatusId");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            Description = "Pending"
                        },
                        new
                        {
                            StatusId = 2,
                            Description = "Processing"
                        },
                        new
                        {
                            StatusId = 3,
                            Description = "Completed"
                        },
                        new
                        {
                            StatusId = 4,
                            Description = "Cancelled"
                        });
                });

            modelBuilder.Entity("PaaS.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PaaS.Models.City", b =>
                {
                    b.HasOne("PaaS.Models.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("PaaS.Models.ContactInfo", b =>
                {
                    b.HasOne("PaaS.Models.City", "City")
                        .WithMany("ContactInfos")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaaS.Models.Province", "Province")
                        .WithMany("ContactInfos")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaaS.Models.User", "User")
                        .WithMany("ContactInfos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Province");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaaS.Models.Item", b =>
                {
                    b.HasOne("PaaS.Models.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaaS.Models.ItemType", "ItemType")
                        .WithMany()
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ItemType");
                });

            modelBuilder.Entity("PaaS.Models.Order", b =>
                {
                    b.HasOne("PaaS.Models.DeliveryMethod", "DeliveryMethod")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaaS.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaaS.Models.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaaS.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryMethod");

                    b.Navigation("PaymentMethod");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaaS.Models.OrderItem", b =>
                {
                    b.HasOne("PaaS.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaaS.Models.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PaaS.Models.User", b =>
                {
                    b.HasOne("PaaS.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PaaS.Models.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("PaaS.Models.City", b =>
                {
                    b.Navigation("ContactInfos");
                });

            modelBuilder.Entity("PaaS.Models.DeliveryMethod", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PaaS.Models.Order", b =>
                {
                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("PaaS.Models.PaymentMethod", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PaaS.Models.Province", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("ContactInfos");
                });

            modelBuilder.Entity("PaaS.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("PaaS.Models.Status", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PaaS.Models.User", b =>
                {
                    b.Navigation("ContactInfos");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
