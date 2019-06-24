using Microsoft.Toolkit.Wpf.UI.XamlHost;
using RealEstateSample.Models;
using System;
using System.Linq;
using System.Windows;

namespace RealEstateSample
{
    /// <summary>
    /// Interaction logic for PropertyDetail.xaml
    /// </summary>
    public partial class HouseDetails : Window
    {
        public House SelectedHouse { get; set; }

        public House SelectedHouseOriginal { get; set; }

        public HouseDetails()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = SelectedHouse;

            txtPriceSqft.Text = (SelectedHouse.Price / SelectedHouse.SqrFeet).ToString("d");

            if (SelectedHouse.Photos.Count>0)
                imgPhoto.DataContext = SelectedHouse.Photos.Last();

        }

        private void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            if (sender is WindowsXamlHost windowsXamlHost &&
              windowsXamlHost.Child is EngineComponent.EngineModel model)
            {
                model.ModelUrl = new Uri("ms-appx:///RealEstateSample/Model/engine.gltf");
            }
        }
    }
}
