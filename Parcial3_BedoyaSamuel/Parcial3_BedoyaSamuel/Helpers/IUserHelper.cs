using Microsoft.AspNetCore.Identity;
using Parcial3_BedoyaSamuel.DAL.Entities;

namespace Parcial3_BedoyaSamuel.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);//recibo el email para logearme

        Task<IdentityResult> AddUserAsync(User user, string password);//agrega el usuario

        Task CheckRoleAsync(string roleName);//si el rol existe

        Task AddUserToRoleAsync(User user, string roleName);//agrega el usuario a un rol

        Task<bool> IsUserInRoleAsync(User user, string roleName);//ya esta relacionado con rol o no

    }
}
