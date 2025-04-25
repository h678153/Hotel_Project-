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
            roomList.Items.Refresh();
        }
    }
}