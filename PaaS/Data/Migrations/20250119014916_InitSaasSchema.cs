using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaaS.Migrations
{
    /// <inheritdoc />
    public partial class InitSaasSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Province Table
                CREATE TABLE Province (
                    provinceId INTEGER PRIMARY KEY AUTOINCREMENT,
                    name VARCHAR(100) NOT NULL
                );

                -- City Table
                CREATE TABLE City (
                    cityId INTEGER PRIMARY KEY AUTOINCREMENT,
                    name VARCHAR(100) NOT NULL,
                    provinceId INTEGER NOT NULL,
                    FOREIGN KEY (provinceId) REFERENCES Province(provinceId)
                );

                -- Role Table
                CREATE TABLE Role (
                    roleId INTEGER PRIMARY KEY AUTOINCREMENT,
                    description VARCHAR(100) NOT NULL
                );

                -- Status Table
                CREATE TABLE Status (
                    statusId INTEGER PRIMARY KEY AUTOINCREMENT,
                    description VARCHAR(50) NOT NULL
                );

                -- User Table
                CREATE TABLE User (
                    userId INTEGER PRIMARY KEY AUTOINCREMENT,
                    firstName VARCHAR(100) NOT NULL,
                    lastName VARCHAR(100) NOT NULL,
                    email VARCHAR(255) NOT NULL,
                    password VARCHAR(255) NOT NULL,
                    isVerified BOOLEAN NOT NULL,
                    roleId INTEGER NOT NULL,
                    FOREIGN KEY (roleId) REFERENCES Role(roleId)
                );

                -- ContactInfo Table
                CREATE TABLE ContactInfo (
                    contactId INTEGER PRIMARY KEY AUTOINCREMENT,
                    phone VARCHAR(15) NOT NULL,
                    address1 VARCHAR(255) NOT NULL,
                    address2 VARCHAR(255),
                    cityId INTEGER NOT NULL,
                    provinceId INTEGER NOT NULL,
                    userId INTEGER NOT NULL,
                    FOREIGN KEY (cityId) REFERENCES City(cityId),
                    FOREIGN KEY (provinceId) REFERENCES Province(provinceId),
                    FOREIGN KEY (userId) REFERENCES User(userId)
                );

                -- DeliveryMethod Table
                CREATE TABLE DeliveryMethod (
                    deliveryMethodId INTEGER PRIMARY KEY AUTOINCREMENT,
                    methodName VARCHAR(100) NOT NULL
                );

                -- PaymentMethod Table
                CREATE TABLE PaymentMethod (
                    paymentMethodId INTEGER PRIMARY KEY AUTOINCREMENT,
                    methodName VARCHAR(100) NOT NULL
                );

                -- Order Table
                CREATE TABLE ""Order"" (
                    orderId INTEGER PRIMARY KEY AUTOINCREMENT,
                    userId INTEGER NOT NULL,
                    orderDate TIMESTAMP NOT NULL,
                    totalAmount NUMERIC(10,2) NOT NULL,
                    deliveryMethodId INTEGER NOT NULL,
                    paymentMethodId INTEGER NOT NULL,
                    statusId INTEGER NOT NULL,
                    FOREIGN KEY (userId) REFERENCES User(userId),
                    FOREIGN KEY (deliveryMethodId) REFERENCES DeliveryMethod(deliveryMethodId),
                    FOREIGN KEY (paymentMethodId) REFERENCES PaymentMethod(paymentMethodId),
                    FOREIGN KEY (statusId) REFERENCES Status(statusId)
                );

                -- ItemType Table
                CREATE TABLE ItemType (
                    itemTypeId INTEGER PRIMARY KEY AUTOINCREMENT,
                    description VARCHAR(100) NOT NULL
                );

                -- Category Table
                CREATE TABLE Category (
                    idCategory INTEGER PRIMARY KEY AUTOINCREMENT,
                    description VARCHAR(100) NOT NULL
                );

                -- Item Table
                CREATE TABLE Item (
                    itemId INTEGER PRIMARY KEY AUTOINCREMENT,
                    name VARCHAR(100) NOT NULL,
                    description TEXT NOT NULL,
                    price NUMERIC(10,2) NOT NULL,
                    imgUrl TEXT,
                    idItemType INTEGER NOT NULL,
                    idCategory INTEGER NOT NULL,
                    FOREIGN KEY (idItemType) REFERENCES ItemType(itemTypeId),
                    FOREIGN KEY (idCategory) REFERENCES Category(idCategory)
                );

                -- OrderItem Table
                CREATE TABLE OrderItem (
                    orderId INTEGER NOT NULL,
                    itemId INTEGER NOT NULL,
                    details TEXT,
                    size VARCHAR(50),
                    quantity INTEGER NOT NULL,
                    PRIMARY KEY (orderId, itemId),
                    FOREIGN KEY (orderId) REFERENCES ""Order""(orderId),
                    FOREIGN KEY (itemId) REFERENCES Item(itemId)
                );
            ");
            

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TABLE IF EXISTS OrderItem;
                DROP TABLE IF EXISTS Order;
                DROP TABLE IF EXISTS Item;
                DROP TABLE IF EXISTS Category;
                DROP TABLE IF EXISTS ItemType;
                DROP TABLE IF EXISTS User;
                DROP TABLE IF EXISTS Role;
                DROP TABLE IF EXISTS Status;
                DROP TABLE IF EXISTS ContactInfo;
                DROP TABLE IF EXISTS DeliveryMethod;
                DROP TABLE IF EXISTS PaymentMethod;
                DROP TABLE IF EXISTS City;
                DROP TABLE IF EXISTS Province;
            ");
        }
    }
}
