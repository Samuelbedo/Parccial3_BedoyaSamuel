using Microsoft.AspNetCore.Mvc.Rendering;

namespace Parcial3_BedoyaSamuel.Helpers
{
    public interface IDropDownListsHelper
    {
        Task<IEnumerable<SelectListItem>> GetDDLServices();
    }
}
