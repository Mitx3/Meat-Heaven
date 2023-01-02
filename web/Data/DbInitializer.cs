using web.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TrgovinaContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Kmetije.Any())
            {
                return;   // DB has been seeded
            }

            var kmetije = new Kmetija[]
            {
            new Kmetija{Lastnik="Bob Lee",Lokacija="Koper"},
            new Kmetija{Lastnik="Marjo Kostabona",Lokacija="Ljubljana"}

            };
            foreach (Kmetija k in kmetije)
            {
                context.Kmetije.Add(k);
            }
            context.SaveChanges();

             var oddelki = new Oddelek[]
            {
                new Oddelek { OddelekIme = "Pregara",     VrstaIzdelkov = "Perutnina",
                    KmetijaID = 1},
                new Oddelek { OddelekIme = "Tuniši",     VrstaIzdelkov = "Govedina",
                    KmetijaID = 1},
                new Oddelek { OddelekIme = "Predmestje",     VrstaIzdelkov = "Razno",
                    KmetijaID = 2}
                
            };

            foreach (Oddelek o in oddelki)
            {
                context.Oddelki.Add(o);
            }
            context.SaveChanges();



            var zgradbe = new Zgradba[]
            {
                new Zgradba { Lokacija = "Kraljeva ulica 2",     OddelekID = 1},
                new Zgradba { Lokacija = "Kraljeva ulica 2a",     OddelekID = 1},
                new Zgradba { Lokacija = "Pregara 77",     OddelekID = 2},

                new Zgradba { Lokacija = "Tržaška cesta 8",     OddelekID = 3},
                new Zgradba { Lokacija = "Tržaška cesta 9",     OddelekID = 3},
            };

            foreach (Zgradba z in zgradbe)
            {
                context.Zgradbe.Add(z);
            }
            context.SaveChanges();

            var izdelki = new Izdelek[]
            {
                new Izdelek { IzdelekIme = "Perutnina", IzdelekVrsta = "Kos 500g", IzdelekCena = 5.6, OddelekID = 1, RokProizvodnje = DateTime.Parse("2022-12-31"), RokUporabe = DateTime.Parse("2023-01-15")},
                new Izdelek { IzdelekIme = "Perutnina", IzdelekVrsta = "Kos 1000g", IzdelekCena = 10, OddelekID = 1, RokProizvodnje = DateTime.Parse("2023-01-02"), RokUporabe = DateTime.Parse("2023-01-25")},

                new Izdelek { IzdelekIme = "Govedina", IzdelekVrsta = "Kos 250g", IzdelekCena = 7, OddelekID = 2, RokProizvodnje = DateTime.Parse("2023-01-02"), RokUporabe = DateTime.Parse("2023-01-31")},

                new Izdelek { IzdelekIme = "Govedina", IzdelekVrsta = "Mleto 250g", IzdelekCena = 7.7, OddelekID = 3, RokProizvodnje = DateTime.Parse("2023-01-04"), RokUporabe = DateTime.Parse("2023-01-15")},
                
            };

            foreach (Izdelek i in izdelki)
            {
                context.Izdelki.Add(i);
            }
            context.SaveChanges();

            var kmetje = new Kmet[]
            {
                new Kmet {Ime = "Marko", Priimek = "Janez", Starost = 24, OddelekID = 1},
                new Kmet {Ime = "Janko", Priimek = "John", Starost = 23, OddelekID = 2},
                new Kmet {Ime = "Darko", Priimek = "Lee", Starost = 77, OddelekID = 3},
                
            };

            foreach (Kmet k in kmetje)
            {
                context.Kmetje.Add(k);
            }
            context.SaveChanges();

            
            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator"},
                new IdentityRole{Id="2", Name="Manager"},
                new IdentityRole{Id="3", Name="Staff"}
            };

            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }

            var user = new ApplicationUser
            {
                FirstName = "Bob",
                LastName = "Dilon",
                City = "Ljubljana",
                Email = "bob@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "bob@example.com",
                NormalizedUserName = "bob@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"Testni123!");
                user.PasswordHash = hashed;
                context.Users.Add(user);
                
            }

            context.SaveChanges();
            

            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
            };

            foreach (IdentityUserRole<string> r in UserRoles)
            {
                context.UserRoles.Add(r);
            }


            context.SaveChanges();
        }
    }
}