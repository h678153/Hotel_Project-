using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HotelLibrary;
using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelProject_WPF
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : Window
    {
        public HotelDbContext? dx = new();
        public ObservableCollection<Reservation> Reservations;
        public ObservableCollection<Room> Rooms;
        public ReservationWindow()
        {
            InitializeComponent();
            Reservations = dx.Reservations.Local.ToObservableCollection();
            Rooms = dx.Rooms.Local.ToObservableCollection();

            dx.Reservations.Load();
            dx.Rooms.Load();
            reservationList.DataContext = Reservations;

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int roomNumber = int.Parse(roomnumberbox.Text);
                int customerid = int.Parse(customeridbox.Text);

                Room room = dx.Rooms.FirstOrDefault(r => r.Roomnumber == roomNumber);
                Customer customer = dx.Customers.FirstOrDefault(c => c.CustomerId == customerid);

                if (room == null)
                {
                    MessageBox.Show("Room not found.");
                    return;
                }

                if (customer == null)
                {
                    MessageBox.Show("Customer not found.");
                    return;
                }

                Reservation r = new()
                {
                    ReservationId = int.Parse(idbox.Text),
                    RoomNumber = int.Parse(roomnumberbox.Text),
                    CustomerId = int.Parse(customeridbox.Text),
                    ReservedRoom = room,
                    Customer = customer,
                    InDate = DateOnly.Parse(indatebox.Text),
                    OutDate = DateOnly.Parse(outdatebox.Text)
                };
                dx.Reservations.Add(r);
                dx.SaveChanges();

                MessageBox.Show("Reservation added successfully!");


            }
            catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void addRoomButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room r = new()
                {
                    Roomnumber = int.Parse(addRoomnumber.Text),
                    Roomtype = addRoomtype.Text
                };
                dx.Rooms.Add(r);
                dx.SaveChanges();

                MessageBox.Show("Room added successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
