using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Models;

public partial class Room
{
    public int Roomnumber { get; set; }
    public string Roomtype { get; set; } = null!; //small - 1 bed, medium - 2 beds, large - 4 beds
    public bool IsAvailable { get; set; } = true;
}
