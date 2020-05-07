using RodBrosEntertainment.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static RodBrosEntertainment.Models.Enums;

namespace RodBrosEntertainment.Data
{
    public static class DbInitializer
    {
        /// <summary>
        /// This method checks to make sure the database exists on the machine (local or server) and if not starts it up with some dummy data.
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(StoreContext context)
        {
            // If the DB doesn't exist, create it, and run any Entity Framework migrations that haven't been run yet (basically "Update-Database")
            context.Database.Migrate();

            // Look for any users.
            if (context.Users.Any())
            {
                // DB has already been seeded, nothing to do here
                return;
            }
            
            var users = new User[]
            {
            new User{
                Email = "grant@test.com",
                Password = "password",
                Name = "Grant Avery",
                UserType = UserType.Admin,
                Street1 = "123 Main",
                City = "Grand Rapids",
                State = "MI",
                Country = "United States",
                Active = ActiveStatus.Active},
            new User{
                Email = "grace@test.com",
                Password = "password",
                Name = "Grace Rodriguez",
                UserType = UserType.Admin,
                Street1 = "123 Main",
                City = "Grand Rapids",
                State = "MI",
                Country = "United States",
                Active = ActiveStatus.Active},
            new User{
                Email = "ryan@test.com",
                Password = "password",
                Name = "Ryan Wooten",
                UserType = UserType.Admin,
                Street1 = "123 Main",
                City = "Grand Rapids",
                State = "MI",
                Country = "United States",
                Active = ActiveStatus.Active},
            new User{
                Email = "austin@test.com",
                Password = "password",
                Name = "Austin Kamp",
                UserType = UserType.Admin,
                Street1 = "123 Main",
                City = "Grand Rapids",
                State = "MI",
                Country = "United States",
                Active = ActiveStatus.Active},
            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();

            Product p1 = new Product { Name = "Heroes of High Castle", Description = "Heroes of High Castle description.", ImageUrl = "https://i.ibb.co/V9SRnCV/hreos-v2.png", Price = 12.12m, ProductType = ProductType.Online, Active = ActiveStatus.Active, AddUserId = 1, AddDateTime = DateTime.Now };
            Product p2 = new Product { Name = "Risk", Description = "Take over the world in this game of strategy conquest, now with updated figures and improved Mission cards. In the Risk game, the goal is simple: players aim to conquer their enemies’ territories by building an army, moving their troops in, and engaging in battle. Depending on the roll of the dice, a player will either defeat the enemy or be defeated. This exciting game is filled with betrayal, alliances, and surprise attacks. On the battlefield, anything goes!", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fpaxsims.files.wordpress.com%2F2014%2F09%2Frisk-board-game-0uap8xyj.jpg&f=1&nofb=1", Price = 29.99m, ProductType = ProductType.Physical, Active = ActiveStatus.Active, AddUserId = 1, AddDateTime = DateTime.Now };
            Product p3 = new Product { Name = "Settlers of Catan", Description = "Picture yourself in the era of discoveries: after a long voyage of great deprivation, your ships have finally reached the coast of an uncharted island. Its name shall be Catan! But you are not the only discoverer. Other fearless seafarers have also landed on the shores of Catan: the race to settle the island has begun!", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fquickman.gameological.com%2Fwp-content%2Fuploads%2F2012%2F10%2F121030_hurricanegames_catan.jpg&f=1&nofb=1", Price = 49.00m, ProductType = ProductType.Physical, Active = ActiveStatus.Active, AddUserId = 1, AddDateTime = DateTime.Now };
            Product p4 = new Product { Name = "Tetris", Description = "Tetris is a tile-matching puzzle video game originally designed and programmed by Soviet Russian software engineer Alexey Pajitnov in 1984 for the Electronika 60. The game has been published by several companies over time, most prominently during a war for the appropriation of the game's rights in the late 1980s.", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fis3.mzstatic.com%2Fimage%2Fthumb%2FPurple118%2Fv4%2F6d%2Fad%2Ff7%2F6dadf7a0-ae79-0f60-74a9-c0c5ebb43f41%2Fsource%2F512x512bb.jpg&f=1&nofb=1", Price = 3.00m, ProductType = ProductType.Online, Active = ActiveStatus.Active, AddUserId = 1, AddDateTime = DateTime.Now };
            Product p5 = new Product { Name = "Fortnite: Save the World", Description = "Fortnite: Save the World is designed as player-versus-environment game, with four players cooperating towards a common objective on various missions. The game is set after a fluke storm appears across Earth, causing 98% of the population to disappear, and the survivors to be attacked by zombie-like 'husks'. The players take the role of commanders of home base shelters, collecting resources, saving survivors, and defending equipment that help to either collect data on the storm or to push back the storm. From missions, players are awarded a number of in-game items, which include hero characters, weapon and trap schematics, and survivors, all of which can be leveled up through gained experience to improve their attributes.", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fs.newsweek.com%2Fsites%2Fwww.newsweek.com%2Ffiles%2Fstyles%2Ffull%2Fpublic%2F2018%2F03%2F30%2Ffortnite-thumbs.jpg&f=1&nofb=1", Price = 0m, ProductType = ProductType.Online, Active = ActiveStatus.Active, AddUserId = 1, AddDateTime = DateTime.Now };
            var products = new Product[]
            {
                p1, p2, p3, p4, p5
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
            new Order { UserId = 1, StatusId = OrderStatus.Cart, OrderDate = DateTime.Now, Subtotal = 13m, Tax = 14m, ShippingCost = 15m, ShippingMiles = 16, Street1 = "123 Main", City = "Grand Rapids", State = "MI", Country = "United States", AddUserId = 2, AddDateTime = DateTime.Now },
            new Order { UserId = 1, StatusId = OrderStatus.Cart, OrderDate = DateTime.Now, Subtotal = 13m, Tax = 14m, ShippingCost = 15m, ShippingMiles = 16, Street1 = "123 Main", City = "Grand Rapids", State = "MI", Country = "United States", AddUserId = 2, AddDateTime = DateTime.Now },
            new Order { UserId = 1, StatusId = OrderStatus.Created, OrderDate = DateTime.Now, Subtotal = 13m, Tax = 14m, ShippingCost = 15m, ShippingMiles = 16, Street1 = "123 Main", City = "Grand Rapids", State = "MI", Country = "United States", AddUserId = 1, AddDateTime = DateTime.Now },
            new Order { UserId = 1, StatusId = OrderStatus.Created, OrderDate = DateTime.Now, Subtotal = 13m, Tax = 14m, ShippingCost = 15m, ShippingMiles = 16, Street1 = "123 Main", City = "Grand Rapids", State = "MI", Country = "United States", AddUserId = 2, AddDateTime = DateTime.Now },
            new Order { UserId = 1, StatusId = OrderStatus.InTransit, OrderDate = DateTime.Now, Subtotal = 13m, Tax = 14m, ShippingCost = 15m, ShippingMiles = 16, Street1 = "123 Main", City = "Grand Rapids", State = "MI", Country = "United States", AddUserId = 2, AddDateTime = DateTime.Now },
            new Order { UserId = 1, StatusId = OrderStatus.Delivered, OrderDate = DateTime.Now, Subtotal = 13m, Tax = 14m, ShippingCost = 15m, ShippingMiles = 16, Street1 = "123 Main", City = "Grand Rapids", State = "MI", Country = "United States", AddUserId = 2, AddDateTime = DateTime.Now }
            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();

            var orderProducts = new OrderProduct[]
            {
            new OrderProduct { OrderId = 1, ProductId = 3, Quantity = 3 },
            new OrderProduct { OrderId = 4, ProductId = 4, Quantity = 2 },
            new OrderProduct { OrderId = 4, ProductId = 3, Quantity = 5 }
            };
            foreach (OrderProduct op in orderProducts)
            {
                context.OrderProducts.Add(op);
            }
            context.SaveChanges();
        }
    }
}