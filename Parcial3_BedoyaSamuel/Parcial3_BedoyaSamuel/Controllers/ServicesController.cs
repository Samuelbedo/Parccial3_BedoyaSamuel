using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial3_BedoyaSamuel.DAL;
using Parcial3_BedoyaSamuel.DAL.Entities;
using Parcial3_BedoyaSamuel.Models;
using System.Diagnostics.Metrics;

namespace Parcial3_BedoyaSamuel.Controllers
{
    public class ServicesController : Controller
    {

        #region Constructor

        private readonly DataBaseContext _context;

        public ServicesController(DataBaseContext context)
        {
            _context = context;
        }

        #endregion


        #region Private Methods
        private async Task<Service> GetServiceById(Guid? servicesId)
        {
            return await _context.Services
                .Include(v => v.Vehicles)
                .ThenInclude(d => d.VehicleDetails)
                .FirstOrDefaultAsync(c => c.Id == servicesId);
        }

        private async Task<Vehicle> GetVehicleById(Guid? vehicleId)
        {
            return await _context.Vehicles
                .Include(s => s.Services)
                .Include(c => c.VehicleDetails)
                .FirstOrDefaultAsync(c => c.Id == vehicleId);
        }

        private async Task<VehicleDetails> GetVehicleDetailsById(Guid? vehicleDetailsId)
        {
            return await _context.VehiclesDetails
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync(c => c.Id == vehicleDetailsId);
        }

        #endregion

        #region Service actions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services
                .Include(c => c.Vehicles)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //service.CreatedDate = DateTime.Now;
                    _context.Add(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(service);
        }

        public async Task<IActionResult> Edit(Guid? serviceId)
        {
            if (serviceId == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await GetServiceById(serviceId);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //service.ModifiedDate = DateTime.Now;
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(service);
        }

        public async Task<IActionResult> Details(Guid? serviceId)
        {
            if (serviceId == null || _context.Services == null) return NotFound();

            var country = await _context.Services
                .Include(c => c.Vehicles)
                .ThenInclude(s => s.VehicleDetails)
                .FirstOrDefaultAsync(m => m.Id == serviceId);

            if (country == null) return NotFound();

            return View(country);
        }

        #endregion

        #region Vehicle actions
        [HttpGet]
        public async Task<IActionResult> AddVehicle(Guid? serviceId)
        {
            if (serviceId== null) return NotFound();

            Service service = await GetServiceById(serviceId);

            if (service == null) return NotFound();

            VehicleViewModel vehicleViewModel = new()
            {
                ServiceId = service.Id,
            };

            return View(vehicleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(VehicleViewModel vehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Vehicle vehicle = new Vehicle()
                    {
                        VehicleDetails = new List<VehicleDetails>(),
                        Services = await GetServiceById(vehicleViewModel.ServiceId),
                        Owner = vehicleViewModel.Owner,
                        NumberPlate = vehicleViewModel.NumberPlate
                    };

                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { vehicleViewModel.ServiceId });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(vehicleViewModel);
        }

        public async Task<IActionResult> DetailsVehicle(Guid? vehicleId)
        {
            if (vehicleId == null || _context.Vehicles == null) return NotFound();

            Vehicle vehicle = await GetVehicleById(vehicleId);

            if (vehicle == null) return NotFound();

            return View(vehicle);
        }
        #endregion

        #region VehicelDetails actions
        [HttpGet]
        public async Task<IActionResult> AddVehicleDetails(Guid? vehicleId)
        {
            if (vehicleId == null) return NotFound();

            Vehicle vehicle = await GetVehicleById(vehicleId);

            if (vehicle == null) return NotFound();

            VehicleDetailsViewModel vehicleDetailsViewModel = new()
            {
                VehicleId = vehicle.Id,
            };

            return View(vehicleDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicleDetails(VehicleDetailsViewModel vehicleDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VehicleDetails vehicleDetails = new()
                    {
                        Vehicle = await GetVehicleById(vehicleDetailsViewModel.VehicleId),
                        CreationDate = DateTime.Now,
                    };

                    _context.Add(vehicleDetails);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsVehicle), new { stateId = vehicleDetailsViewModel.VehicleId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "Ya existe una ciudad con el mismo nombre en este Dpto/Estado.");
                    else
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(vehicleDetailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditVehicleDetails(Guid? vehicleDetailsId)
        {
            if (vehicleDetailsId == null || _context.VehiclesDetails == null) return NotFound();

            VehicleDetails vehicleDetails = await GetVehicleDetailsById(vehicleDetailsId);

            if (vehicleDetails == null) return NotFound();

            VehicleDetailsViewModel vehicleDetailsViewModel = new()
            {
                VehicleId = vehicleDetails.Vehicle.Id,
                Id = vehicleDetails.Id,
                CreationDate = vehicleDetails.CreationDate,
                DeliveryDate = vehicleDetails.DeliveryDate,
            };

            return View(vehicleDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicleDetails(Guid vehicleId, VehicleDetailsViewModel vehicleDetailsViewModel)
        {
            if (vehicleId != vehicleDetailsViewModel.VehicleId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    VehicleDetails vehicleDetails = new()
                    {
                        Id = vehicleDetailsViewModel.Id,
                        CreationDate = vehicleDetailsViewModel.CreationDate,
                    };

                    _context.Update(vehicleDetails);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsVehicle), new { vehicleId = vehicleDetailsViewModel.VehicleId });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(vehicleDetailsViewModel);
        }

        public async Task<IActionResult> DetailsVehicleDetails(Guid? vehicleDetailsId)
        {
            if (vehicleDetailsId == null || _context.VehiclesDetails == null) return NotFound();

            VehicleDetails vehicleDetails = await GetVehicleDetailsById(vehicleDetailsId);

            if (vehicleDetails == null) return NotFound();

            return View(vehicleDetails);
        }

        #endregion
    }
}
