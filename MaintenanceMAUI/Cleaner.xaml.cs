namespace MaintenanceMAUI;

using System.Collections.ObjectModel;
using HotelLibrary;
using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

        Dx.Rooms.Local.CollectionChanged += Local_CollectionChanged;
    }

    private void CleanedButton_Clicked(object sender, EventArgs e)
    {
        Room? room = CleanedListView.SelectedItem as Room;

        if (room == null)
        {
            DisplayAlert("Error", "Please select a room", "OK");
            return;
        }

        if (room.Cleaned == "New")
        {
            room.Cleaned = "In progress";
        }
        else if (room.Cleaned == "In progress")
        {
            room.Cleaned = "Done";
        }
        else
        {
            room.Cleaned = "New";
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

    private void CleanedListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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
        Room? room = CleanedListView.SelectedItem as Room;

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
        CleanedListView.ItemsSource = Dx.Rooms.Local.ToObservableCollection();
    }
}