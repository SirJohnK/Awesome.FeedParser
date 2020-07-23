using System;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Geographical location.
    /// </summary>
    public class GeoCoordinate
    {
        private double latitude;
        private double longitude;

        /// <summary>
        /// Constructor setting location to unknown.
        /// </summary>
        public GeoCoordinate() : this(double.NaN, double.NaN)
        {
        }

        /// <summary>
        /// Constructor setting location.
        /// </summary>
        /// <param name="latitude">Location latitude value.</param>
        /// <param name="longitude">Location longitude value.</param>
        public GeoCoordinate(double latitude, double longitude)
        {
            //Init
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Location latitude value.
        /// </summary>
        public double Latitude
        {
            get => latitude;
            set
            {
                //Verify value
                if (value > 90.0 || value < -90.0) throw new ArgumentOutOfRangeException("Latitude", "Must be between -90 and 90!");
                latitude = value;
            }
        }

        /// <summary>
        /// Location longitude value.
        /// </summary>
        public double Longitude
        {
            get => longitude;
            set
            {
                //Verify value
                if (value > 180.0 || value < -180.0) throw new ArgumentOutOfRangeException("Longitude", "Must be between -180 and 180!");
                longitude = value;
            }
        }
    }
}