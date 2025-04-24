using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Models;

public partial class Customer
{
    public int CustomerId { get; set; } 
    public string CustomerName { get; set; } = null!;
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public string CustomerPassword { get; set; } = null!;


}