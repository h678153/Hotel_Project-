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

        Dx.Rooms.Local.CollectionChanged += Local_CollectionChanged;
    }

    private void MaintainedButton_Clicked(object sender, EventArgs e)
    {
        Room? room = MaintenanceListView.SelectedItem as Room;

        if (room == null)
        {
            DisplayAlert("Error", "Please select a room", "OK");
            return;
        }

        if (room.Maintained == "New")
        {
            room.Maintained = "In progress";
        }
        else if (room.Maintained == "In progress")
        {
            room.Maintained = "Done";
        }
        else
        {
            room.Maintained = "New";
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

    private void SaveNoteButton_Clicked(object sender, EventArgs e)
    {
        Room? room = MaintenanceListView.SelectedItem as Room;

        if(room == null)
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

    private void MaintenanceListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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

    private void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
       
        MaintenanceListView.ItemsSource = Dx.Rooms.Local.ToObservableCollection();
        
    }
}