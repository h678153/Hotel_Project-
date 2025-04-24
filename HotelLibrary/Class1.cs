
using HotelLibrary.Models;

namespace HotelLibrary;

public class Class1
{

    public Customer? AuthenticateLogin (string username, string password, HotelDbContext context)
    {
        return context.Customers.FirstOrDefault(c => c.CustomerName == username && c.CustomerPassword == password);
    }

}
