using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotelLibrary;
using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelProject_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HotelDbContext? Dx = new();
        public ObservableCollection<Room> Rooms;

        public MainWindow()
        {
            InitializeComponent();
            Rooms = Dx.Rooms.Local.ToObservableCollection();
            Dx.Rooms.Load();
            roomList.DataContext = Rooms.OrderBy(r => r.Roomnumber);

            roomList.MouseDoubleClick += roomList_MouseDoubleClick;

            Dx.Rooms.Local.CollectionChanged += Local_CollectionChanged;    
        }

        private void roomList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Room? r = roomList.SelectedItem as Room;

            if (r != null)
            {
                ReservationWindow rw = new()
                {
                    dx = Dx
                };
                rw.Show();
            }
        }

        private void viewReservations_Click(object sender, RoutedEventArgs e)
        {
            ReservationWindow rw = new()
            {
                dx = Dx
            };
            rw.Show();
        }

        private void checkin_Click(object sender, RoutedEventArgs e)
        {
            Room? room = roomList.SelectedItem as Room;

            if (room != null)
            {
                try
                {
                    if(room.IsAvailable == false) room.IsAvailable = true;
                     else room.IsAvailable = false;
                    
                    Dx.Rooms.Update(room);
                    Dx.SaveChanges();

                    if (room.IsAvailable == true) MessageBox.Show("Room checked out successfully.");
                    else MessageBox.Show("Room checked in successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    refresh_Click(sender, e);
                }
            }
        }
        private void requestMaintenance_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceWindow mw = new()
            {
                Dx = Dx
            };
            mw.Show();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Dx.Rooms.Load();
            roomList.DataContext = Rooms.OrderBy(r => r.Roomnumber);
        }

        private void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dx.Rooms.Load();
            roomList.DataContext = Rooms.OrderBy(r => r.Roomnumber);
        }
    }
}