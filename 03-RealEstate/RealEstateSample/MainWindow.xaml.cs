using RealEstateSample.Models;
using RealEstateSample.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Windows.Devices.Geolocation;
using System;

namespace RealEstateSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private List<House> Houses { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            await GetPosition();
        }

        private void LoadData()
        {
            DatabaseService db = new DatabaseService();
            db.InitializeDatabase();
            Houses = db.GetHouses();
            HousesGrid.ItemsSource = Houses;
            Subject.GetInstance().Subscribe(this);
        }

        private void OnSelectedHome(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var house = e.AddedItems[0] as House;

                if (house != null && house.HouseId != 0)
                {
                    HouseDetails detail = new HouseDetails();
                    detail.SelectedHouse = house;

                    detail.Show();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Update(House house)
        {
            var r = Houses.Find(h => h.HouseId == house.HouseId);
            if (r != null)
                r = house;
            HousesGrid.Items.Refresh();
        }

        public async Task GetPosition()
        {
            Geolocator geolocator = new Geolocator() { DesiredAccuracyInMeters = 5 };
            Geoposition pos = await geolocator.GetGeopositionAsync();

            await TheMap.TrySetViewAsync(pos.Coordinate.Point, 15.0);
        }
    }
}