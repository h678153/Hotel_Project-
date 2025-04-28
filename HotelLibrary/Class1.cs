
using HotelLibrary.Models;

namespace HotelLibrary;

public class Class1
{

    public Customer? AuthenticateLogin (string username, string password, HotelDbContext context)
    {
        return context.Customers.FirstOrDefault(c => c.CustomerName == username && c.CustomerPassword == password);
    }

    public bool RegisterCustomer(string username, string password, HotelDbContext context)
    {
        if (context.Customers.Any(c => c.CustomerName == username))
        {
            return false; 
        }

        int newCustomerId = context.Customers.Max(c => c.CustomerId) + 1;

        var newCustomer = new Customer
        {
            CustomerId = newCustomerId,
            CustomerName = username,
            CustomerPassword = password
        };
        context.Customers.Add(newCustomer);
        context.SaveChanges();
        return true;
    }

    public List<Room> GetAvailableRooms(HotelDbContext context, DateOnly from, DateOnly to)
    {
        
        var allRooms = context.Rooms.ToList();

        
        var reservedRoomNumbers = context.Reservations
            .Where(r =>
                (from <= r.OutDate) && (to >= r.InDate)
            )
            .Select(r => r.RoomNumber)
            .Distinct()
            .ToList();

        
        var availableRooms = allRooms
            .Where(room => !reservedRoomNumbers.Contains(room.Roomnumber))
            .ToList();

        return availableRooms;


    }
    public void CreateReservation(String roomtype, int customerId, DateOnly inDate, DateOnly outDate, HotelDbContext context)
    {
        var availableroom = context.Rooms.FirstOrDefault(r => r.Roomtype == roomtype);
        if (availableroom == null)
        {
            throw new Exception("Room type not found");
        }

        var reservation = new Reservation
        {
            RoomNumber = availableroom.Roomnumber,
            CustomerId = customerId,
            InDate = inDate,
            OutDate = outDate
        };
        context.Reservations.Add(reservation);
        context.SaveChanges();
    }

}
