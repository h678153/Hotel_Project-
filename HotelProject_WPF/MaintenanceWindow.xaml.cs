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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotelLibrary;
using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelProject_WPF
{
    /// <summary>
    /// Interaction logic for MaintenanceWindow.xaml
    /// </summary>
    public partial class MaintenanceWindow : Window
    {
        public HotelDbContext? Dx = new();
        public ObservableCollection<Room> Rooms;
        public MaintenanceWindow()
        {
            InitializeComponent();
            Rooms = Dx.Rooms.Local.ToObservableCollection();

            Dx.Rooms.Load();
            roomList.DataContext = Rooms;
            Dx.Rooms.Local.CollectionChanged += Local_CollectionChanged;
        }

        private void maintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            Room? room = roomList.SelectedItem as Room;

            room.Maintained = "New";

            try
            {
                Dx.Rooms.Update(room);
                Dx.SaveChanges();


            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Local_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dx.Rooms.Load();
            roomList.DataContext = Rooms.OrderBy(r => r.Maintained);
        }

        private void roomServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Room? room = roomList.SelectedItem as Room;

            room.Serviced = "New";

            try
            {
                Dx.Rooms.Update(room);
                Dx.SaveChanges();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cleaningButton_Click(object sender, RoutedEventArgs e)
        {
            Room? room = roomList.SelectedItem as Room;

            room.Maintained = "New";

            try
            {
                Dx.Rooms.Update(room);
                Dx.SaveChanges();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
