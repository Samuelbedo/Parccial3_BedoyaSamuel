using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial3_BedoyaSamuel.DAL;
using Parcial3_BedoyaSamuel.DAL.Entities;
using Parcial3_BedoyaSamuel.Helpers;
using Parcial3_BedoyaSamuel.Models;
using Parcial3_BedoyaSamuel.Services;
using System.Diagnostics.Metrics;

namespace Parcial3_BedoyaSamuel.Controllers
{
    public class ServicesController : Controller
    {

        #region Constructor

        private readonly DataBaseContext _context;
        private readonly IDropDownListsHelper _dropDownListsHelper;

        public ServicesController(DataBaseContext context, IDropDownListsHelper dropDownListsHelper)
        {
            _context = context;
            _dropDownListsHelper = dropDownListsHelper;
        }

        #endregion


        #region Private Methods
        private async Task<Service> GetServiceById(Guid? serviceId)
        {
            Service service = await _context.Services
                .FirstOrDefaultAsync(service => service.Id == serviceId);
            return service;
        }

        #endregion

        #region Service actions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services
                .Include(v => v.Vehicles)
                .ToListAsync());
        }

        public async Task<IActionResult> AddService()
        {
            AddServiceViewModel addServiceViewModel = new()
            {
                Id = Guid.Empty,
                Services = await _dropDownListsHelper.GetDDLServices(),
            };

            return View(addServiceViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> AddService(AddServiceViewModel addServiceViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    Vehicle vehicle = new()
                    {
                        Owner = addServiceViewModel.Owner,
                        NumberPlate = addServiceViewModel.NumberPlate,
                        Services = await GetServiceById(addServiceViewModel.ServiceId)
                    };

                    _context.Vehicles.Add(vehicle);

                    VehicleDetails vehicleDetails = new()
                    {
                        CreationDate = DateTime.Now,
                        DeliveryDate = null,
                        Vehicle = vehicle,

                    }; 

                    _context.VehiclesDetails.Add(vehicleDetails);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);                    
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(addServiceViewModel);
        }

        #endregion
    }
}
