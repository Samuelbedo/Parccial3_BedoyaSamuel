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

        private async Task<VehicleDetails> GetVehicleDetailById(Guid? vehicleDetailId)
        {
            VehicleDetails vehicleDetail = await _context.VehiclesDetails
                .Include(vehicleDetail => vehicleDetail.Vehicle)
                .ThenInclude(vehicle => vehicle.Services)
                .FirstOrDefaultAsync(vehicleDetail => vehicleDetail.Id == vehicleDetailId);
            return vehicleDetail;
        }

        #endregion

        #region Service actions
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services
                .Include(v => v.Vehicles)
                .ToListAsync());
        }
        [Authorize(Roles = "Client")]
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

        [HttpGet]
        public async Task<IActionResult> ProcessService()
        {
            return View(await _context.VehiclesDetails.Include(vehicleDetails => vehicleDetails.Vehicle).ThenInclude(vehicle => vehicle.Services).ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProcessService(Guid? vehicleDetailId)
        {
            Console.WriteLine(vehicleDetailId);
            if (vehicleDetailId == null || _context.VehiclesDetails == null)
            {
                return NotFound();
            }

            var vehicleDetail = await GetVehicleDetailById(vehicleDetailId);
            if (vehicleDetail == null)
            {
                return NotFound();
            }
            return View(vehicleDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProcessService(Guid vehicleDetailId, VehicleDetails vehicleDetail)
        {
            if (vehicleDetailId != vehicleDetail.Id)
            {
                return NotFound();
            }

            _context.Update(vehicleDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProcessService));

        }

        [HttpGet]
        public async Task<IActionResult> ServiceStatus()
        {
            return View(await _context.VehiclesDetails.Include(vehicleDetails => vehicleDetails.Vehicle).ThenInclude(vehicle => vehicle.Services).ToListAsync());
        }

        #endregion
    }
}
