using BUA_project.Models;
using BUA_project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BUA_project.Controllers
{
	public class UserReservationController : Controller
	{
		private readonly Entity _context = new Entity();


		// ============================================
		// GET: UserReservation/Create
		// ============================================

		public async Task<IActionResult> Create(int? vehicleId)
		{
			// No vehicle selected
			if (vehicleId == null)
				return BadRequest();


			// Get selected vehicle
			// It must still be available
			var vehicle = await _context.Vehicles
				.FirstOrDefaultAsync(v =>
					v.VehicleId == vehicleId &&
					v.Status == "Available");


			// Vehicle doesn't exist or isn't available
			if (vehicle == null)
				return NotFound();


			// Create ViewModel
			var viewModel = new UserReservationViewModel
			{
				VehicleId = vehicle.VehicleId
			};


			// Send vehicle information to View
			ViewBag.Vehicle = vehicle;


			return View(viewModel);
		}


		// ============================================
		// POST: UserReservation/Create
		// ============================================

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			UserReservationViewModel model)
		{
			// ============================================
			// Validate ViewModel
			// ============================================

			if (!ModelState.IsValid)
			{
				var vehicle = await _context.Vehicles
					.FirstOrDefaultAsync(v =>
						v.VehicleId == model.VehicleId &&
						v.Status == "Available");

				ViewBag.Vehicle = vehicle;

				return View(model);
			}


			// ============================================
			// Get Current Logged-in User
			// ============================================

			var currentUserEmail = User.Identity?.Name;


			var currentUser = await _context.Users
				.FirstOrDefaultAsync(u =>
					u.Email == currentUserEmail);


			if (currentUser == null)
				return Unauthorized();


			// ============================================
			// Check Vehicle Availability Again
			// ============================================

			// Important:
			// The vehicle might have become unavailable
			// after the GET request.

			var selectedVehicle = await _context.Vehicles
				.FirstOrDefaultAsync(v =>
					v.VehicleId == model.VehicleId &&
					v.Status == "Available");


			if (selectedVehicle == null)
			{
				ModelState.AddModelError(
					"",
					"This vehicle is no longer available.");


				ViewBag.Vehicle =
					await _context.Vehicles
						.FirstOrDefaultAsync(v =>
							v.VehicleId == model.VehicleId);


				return View(model);
			}


			// ============================================
			// Create Reservation
			// ============================================

			var reservation = new Reservation
			{
				VehicleId = model.VehicleId,

				StartDateTime = model.StartDateTime,

				EndDateTime = model.EndDateTime,

				Origin = model.Origin,

				Destination = model.Destination,

				Passengers = model.Passengers,

				Load = model.Load,

				Purpose = model.Purpose,


				// Automatically assigned
				// from logged-in user
				UserId = currentUser.UserId,


				// New user reservation
				// always starts as Pending
				Status = "Pending"
			};


			// Add reservation
			_context.Reservations.Add(reservation);


			// Save to database
			await _context.SaveChangesAsync();


			// ============================================
			// Return to User Dashboard
			// ============================================

			return RedirectToAction(
				"Index",
				"UserDashboard");
		}
	}
}
