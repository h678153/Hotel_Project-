using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Models;

public partial class Room : INotifyPropertyChanged
{
    public int Roomnumber { get; set; }
    public string Roomtype { get; set; } = null!; //small - 1 bed, medium - 2 beds, large - 4 beds
    private bool _IsAvailable = true;
    public bool IsAvailable
    {
        get => _IsAvailable;
        set
        {
            _IsAvailable = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAvailable)));
        }
    }

    private string _Cleaned { get; set; } = null!;
    public string Cleaned
    {
        get => _Cleaned;
        set
        {
            _Cleaned = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cleaned)));
        }
    }
    private string _Maintained { get; set; } = null!;
    public string Maintained
    {
        get => _Maintained;
        set
        {
            _Maintained = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Maintained)));
        }
    }
    private string _Serviced { get; set; } = null!;
    public string Serviced
    {
        get => _Serviced;
        set
        {
            _Serviced = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Serviced)));
        }
    }
    public string RoomNotes { get; set; } = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
} 
