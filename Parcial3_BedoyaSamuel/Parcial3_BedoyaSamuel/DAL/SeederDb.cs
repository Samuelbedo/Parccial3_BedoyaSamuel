using Parcial3_BedoyaSamuel.DAL.Entities;
using Parcial3_BedoyaSamuel.Enums;
using Parcial3_BedoyaSamuel.Helpers;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;

namespace Parcial3_BedoyaSamuel.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        private readonly IUserHelper _userHelper;
        public SeederDb(DataBaseContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeederAsync()
        {
            var email = "SamuelBedoya@yopmail.com";
            await _context.Database.EnsureCreatedAsync();
            await PopulateServicesAsync();
            await PopulateRolesAsync();
            await PopulateUserAsync(email);

            await _context.SaveChangesAsync();
        }


        private async Task PopulateServicesAsync()
        {
            if (!_context.Services.Any())
            {
                _context.Services.Add(new Service { Name = "Lavada Simple", Price = 25000 });
                _context.Services.Add(new Service { Name = "Lavada + Polishada", Price = 50000 });
                _context.Services.Add(new Service { Name = "Lavada + Aspirada de Cojineria", Price = 30000 });
                _context.Services.Add(new Service { Name = "Lavada Full", Price = 65000 });
                _context.Services.Add(new Service { Name = "Lavada en seco del Motor", Price = 80000 });
                _context.Services.Add(new Service { Name = "Lavada del Chasis", Price = 90000 });
            }
        }

        private async Task PopulateRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Client.ToString());
        }
        private async Task PopulateUserAsync(string email)
        {
            User user = await _userHelper.GetUserAsync(email);

            if (user == null)
            {
                user = new User
                {
                    Document = "1044210047",
                    FirstName = "Samuel",
                    LastName = "Bedoya",
                    Email = email,
                    UserName = email,
                    UserType = UserType.Admin,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, UserType.Admin.ToString());
            }
        }


    }
}
