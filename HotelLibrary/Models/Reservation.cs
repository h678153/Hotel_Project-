using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }
    public int RoomNumber { get; set; }
    public int CustomerId { get; set; }
    public Room ReservedRoom { get; set; } = null!;
    public Customer Customer { get; set; } = null!;


}
