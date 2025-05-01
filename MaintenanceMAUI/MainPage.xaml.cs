using System.Collections.ObjectModel;
using HotelLibrary.Models;

namespace MaintenanceMAUI
{
    public partial class MainPage : ContentPage
    {
        HotelDbContext? Dx = new();


        public MainPage()
        {
            InitializeComponent();


        }

        private async void MaintenanceButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Maintenance()
            {
                Dx = Dx
            });


        }

        private async void CleaningButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cleaner()
            {
                Dx = Dx
            });
        }

        private async void ServiceButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Service()
            {
                Dx = Dx
            });

        }
    }

}
