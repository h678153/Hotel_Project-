namespace MaintenanceMAUI;

using System.Collections.ObjectModel;
using HotelLibrary;
using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public partial class Cleaner : ContentPage
{
    public HotelDbContext? Dx = new();
    public ObservableCollection<Room> Rooms;
    public Cleaner()
	{
		InitializeComponent();
        Rooms = Dx.Rooms.Local.ToObservableCollection();
        Dx.Rooms.Load();
        CleanedListView.ItemsSource = Rooms;

    }

    private void CleanedButton_Clicked(object sender, EventArgs e)
    {
        Room? room = CleanedListView.SelectedItem as Room;

        room.Cleaned = true;
        try
        {
            Dx.Rooms.Update(room);
            Dx.SaveChanges();
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            ObservableCollection<Room> updatedRooms;
            updatedRooms = Dx.Rooms.Local.ToObservableCollection();
            CleanedListView.ItemsSource = updatedRooms;
            CleanedListView.SelectedItem = null;
        }
    }
}