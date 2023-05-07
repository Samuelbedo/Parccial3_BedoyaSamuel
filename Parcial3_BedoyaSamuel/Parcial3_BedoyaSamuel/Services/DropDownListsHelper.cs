using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial3_BedoyaSamuel.DAL;
using Parcial3_BedoyaSamuel.Helpers;

namespace Parcial3_BedoyaSamuel.Services
{
    public class DropDownListsHelper : IDropDownListsHelper
    {
        private readonly DataBaseContext _context;

        public DropDownListsHelper(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetDDLServices()
        {
            List<SelectListItem> listServices = await _context.Services
               .Select(s => new SelectListItem
               {
                   Text = s.Name,
                   Value = s.Id.ToString(), //Guid                    
               })
               .ToListAsync();

            listServices.Insert(0, new SelectListItem
            {
                Text = "Selecione un servicio...",
                Value = Guid.Empty.ToString(),
                Selected = true 
            });

            return listServices;
        }
    }
}
