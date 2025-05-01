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

        Dx.Rooms.Local.CollectionChanged += Local_CollectionChanged;
    }
    private void ServicedButton_Clicked(object sender, EventArgs e)
    {
        Room? room = ServicedListView.SelectedItem as Room;

        if (room == null)
        {
            DisplayAlert("Error", "Please select a room", "OK");
            return;
        }

        if (room.Serviced == "New")
        {
            room.Serviced = "In progress";
        }
        else if (room.Serviced == "In progress")
        {
            room.Serviced = "Done";
        }
        else
        {
            room.Serviced = "New";
        }


        try
        {
            Dx.Rooms.Update(room);
            Dx.SaveChanges();

        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void ServicedListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Room selectedRoom)
        {
            NotesEditor.Text = selectedRoom.RoomNotes;
        }
        else
        {
            NotesEditor.Text = string.Empty;
        }
    }

    private void SaveNoteButton_Clicked(object sender, EventArgs e)
    {
        Room? room = ServicedListView.SelectedItem as Room;

        if (room == null)
        {
            DisplayAlert("Error", "Please select a room", "OK");
            return;
        }

        room.RoomNotes = NotesEditor.Text;

        try
        {
            Dx.Rooms.Update(room);
            Dx.SaveChanges();
            DisplayAlert("Success", "Note saved successfully.", "OK");

        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        
            ServicedListView.ItemsSource = Dx.Rooms.Local.ToObservableCollection();
        
    }
}