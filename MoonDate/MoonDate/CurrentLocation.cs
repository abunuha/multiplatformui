using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonDate
{
    public class CurrentLocation
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public Location GetLocation()
        {
            Task<Location> task = GetCurrentLocationAsync();
            Location loc = task.Result;
            return loc;
        }

        private async Task<Location> GetCurrentLocationAsync()
        {
            try
            {
                Location location = await Geolocation.GetLastKnownLocationAsync();
                //if (location != null)
                //location = await Geolocation.GetLocationAsync();
                //else
                //await DisplayAlert("Unknown", "Your Location is unknown", "Ok");
                return location;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String GetAddress(double latitude, double longitude)
        {
            Task<string> task = GetGeocodeReverseData(latitude, longitude);
            return task.Result;
        }

        private async Task<string> GetGeocodeReverseData(double latitude = 47.673988, double longitude = -122.121513)
        {
            IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

            Placemark placemark = placemarks?.FirstOrDefault();

            if (placemark != null)
            {
                return
                    $"AdminArea:       {placemark.AdminArea}\n" +
                    $"CountryCode:     {placemark.CountryCode}\n" +
                    $"CountryName:     {placemark.CountryName}\n" +
                    $"FeatureName:     {placemark.FeatureName}\n" +
                    $"Locality:        {placemark.Locality}\n" +
                    $"PostalCode:      {placemark.PostalCode}\n" +
                    $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                    $"SubLocality:     {placemark.SubLocality}\n" +
                    $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                    $"Thoroughfare:    {placemark.Thoroughfare}\n";

            }

            return "";
        }

        private async Task<Location> GetCurrentLocationAsyncDefunct()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Lowest, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                return location;

               // if (location != null)
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
                return null;
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }
}
