using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    /// <summary>
    /// Object representation of a Park
    /// </summary>
    public class Park
    {
        #region Member Variables

        /// <summary>
        /// Feet to Meter conversion ratio
        /// </summary>
        private double _feetToMeterRatio = 0.3048;

        /// <summary>
        /// Miles to Kilometers conversion ratio
        /// </summary>
        private double _milesToKilometersRatio = 1.60934;

        #endregion

        #region Properties

        /// <summary>
        /// Park Code for a given Park
        /// </summary>
        public string ParkCode { get; set; }

        /// <summary>
        /// The Name for a given Park
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// The State the given Park is located
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Land area over which the given Park extends in Acres
        /// </summary>
        public int Acreage { get; set; }

        /// <summary>
        /// How high above sea level the given Park is in Feet
        /// </summary>
        public int ElevationInFeet { get; set; }

        /// <summary>
        /// Total distance of trails in the given Park in Miles
        /// </summary>
        public double MilesOfTrail { get; set; }

        /// <summary>
        /// Number of campsites witin the given Park
        /// </summary>
        public int NumberOfCampsites { get; set; }

        /// <summary>
        /// The climate type of the given Park
        /// </summary>
        public string Climate { get; set; }

        /// <summary>
        /// The year the given Park was founded
        /// </summary>
        public int YearFounded { get; set; }

        /// <summary>
        /// The number of yearly visitors for the given Park
        /// </summary>
        public int AnnualVisitorCount { get; set; }

        /// <summary>
        /// The inspirational quote the given Park has chosen?
        /// </summary>
        public string InspirationalQuote { get; set; }

        /// <summary>
        /// The source of the given Park's inspirational quote
        /// </summary>
        public string InspirationalQuoteSource { get; set; }

        /// <summary>
        /// A description of the given Park
        /// </summary>
        public string ParkDescription { get; set; }

        /// <summary>
        /// The cost to enter the given Park.
        /// </summary>
        public int EntryFee { get; set; }

        /// <summary>
        /// The number of local animal species residing within the given Park
        /// </summary>
        public int NumberOfAnimalSpecies { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a string representation of the Park's Elevation with the measurement and system unit
        /// </summary>
        /// <param name="isMetric">True if the desired system is Metric, false if Imperial</param>
        /// <returns>String containing the measure and scale</returns>
        public string ElevationString(bool isMetric)
        {
            if (!isMetric)
            {
                return (ElevationInFeet.ToString("N0") + " feet");
            }
            else
            {
                int elevationInMeters = FeetToMeter(ElevationInFeet);

                return (elevationInMeters.ToString("N0") + " meters");
            }
        }

        /// <summary>
        /// Converts the given Feet measurement to Meters
        /// </summary>
        /// <param name="feet">Measurement in Feet</param>
        /// <returns>Measurement in Meters</returns>
        private int FeetToMeter(int feet)
        {
            return (int)(feet * _feetToMeterRatio);
        }

        /// <summary>
        /// Returns a string representation of the Park's length of Trails with the measurement and system unit
        /// </summary>
        /// <param name="isMetric">True if the desired system is Metric, false if Imperial</param>
        /// <returns>String containing the measure and scale</returns>
        public string TrailsString(bool isMetric)
        {
            if (!isMetric)
            {
                return (MilesOfTrail.ToString("N2") + " miles");
            }
            else
            {
                double trailsInKilometers = MilesToKilometers(MilesOfTrail);

                return (trailsInKilometers.ToString("N2") + " kilometers");
            }
        }

        /// <summary>
        /// Converts the given Miles measurement to Kilometers
        /// </summary>
        /// <param name="miles">Measurement in Miles</param>
        /// <returns>Measurement in Kilometers</returns>
        private double MilesToKilometers(double miles)
        {
            return (miles * _milesToKilometersRatio);
        }

        #endregion
    }
}
