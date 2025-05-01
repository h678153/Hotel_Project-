namespace MaintenanceMAUI;

using System.Collections.ObjectModel;
using HotelLibrary;
using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public partial class Maintenance : ContentPage
{
    public HotelDbContext? Dx = new();
	public ObservableCollection<Room> Rooms;
    public Maintenance()
	{
		InitializeComponent();
		Rooms = Dx.Rooms.Local.ToObservableCollection();

        Dx.Rooms.Load();
        MaintenanceListView.ItemsSource = Rooms;
    }

    private void MaintainedButton_Clicked(object sender, EventArgs e)
    {
        Room? room = MaintenanceListView.SelectedItem as Room;

        room.Maintained = true;

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
            MaintenanceListView.ItemsSource = updatedRooms;

            MaintenanceListView.SelectedItem = null;
        }
    }

    private void MaintainButton_Clicked(object sender, EventArgs e)
    {
        Room? room = MaintenanceListView.SelectedItem as Room;

        room.Maintained = false;

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
            MaintenanceListView.ItemsSource = updatedRooms;

            MaintenanceListView.SelectedItem = null;
        }
    }
}