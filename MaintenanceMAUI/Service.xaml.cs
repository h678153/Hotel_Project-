namespace MaintenanceMAUI;

using System.Collections.ObjectModel;
using HotelLibrary;
using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public partial class Service : ContentPage
{
    public HotelDbContext? Dx = new();
    public ObservableCollection<Room> Rooms;
    public Service()
	{
		InitializeComponent();
        Rooms = Dx.Rooms.Local.ToObservableCollection();
        Dx.Rooms.Load();
        ServicedListView.ItemsSource = Rooms;
    }
    private void ServicedButton_Clicked(object sender, EventArgs e)
    {
        Room? room = ServicedListView.SelectedItem as Room;
        room.Serviced = true;
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
            ServicedListView.ItemsSource = updatedRooms;
            ServicedListView.SelectedItem = null;
        }
    }
}