using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelLibrary.Models;

namespace HotelWebAppMVC.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rooms.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Roomnumber == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roomnumber,Roomtype,IsAvailable,Cleaned")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Roomnumber,Roomtype,IsAvailable,Cleaned")] Room room)
        {
            if (id != room.Roomnumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Roomnumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Roomnumber == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Roomnumber == id);
        }
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string Roomtype, DateTime from, DateTime to)
        {
            var fromDate = DateOnly.FromDateTime(from);
            var toDate = DateOnly.FromDateTime(to);

            var reservedRoomNumbers = _context.Reservations
                .Where(r =>
                    (fromDate <= r.OutDate) && (toDate >= r.InDate)
                )
                .Select(r => r.RoomNumber)
                .Distinct()
                .ToList();

            var availableRoomsFiltered = _context.Rooms
                .Where(room =>
                    room.IsAvailable &&                
                    (room.Cleaned=="Done") &&                    
                    (string.IsNullOrEmpty(Roomtype) || room.Roomtype.ToLower() == Roomtype.ToLower()) && 
                    !reservedRoomNumbers.Contains(room.Roomnumber) 
                )
                .ToList();

            
            Console.WriteLine($"Rooms found: {availableRoomsFiltered.Count}");

            return View("Index", availableRoomsFiltered);
        }

        public IActionResult Reserve(int id)  
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Roomnumber == id);
            if (room == null)
            {
               
                return RedirectToAction("Index");  
            }

            return View(room);  
        }




        [HttpPost]
        public IActionResult ConfirmReservation(int roomNumber, int customerId, DateTime from, DateTime to)
        {
            var customerIdcheck = HttpContext.Session.GetInt32("CustomerId");

            int reservationid = _context.Reservations.Max(r => r.ReservationId) + 1;

            Room room = _context.Rooms.FirstOrDefault(r => r.Roomnumber == roomNumber);

            if (customerIdcheck == 0)
            {
                return RedirectToAction("Index", "Customers");
            }

            var fromDate = DateOnly.FromDateTime(from);
            var toDate = DateOnly.FromDateTime(to);

            var reservation = new Reservation
            {
                ReservationId = reservationid,
                RoomNumber = roomNumber,
                CustomerId = customerId,
                ReservedRoom = room,
                InDate = fromDate,
                OutDate = toDate
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return RedirectToAction("Index"); 
        }



    }
}
