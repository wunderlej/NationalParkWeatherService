using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    /// <summary>
    /// View Model used on the Park Detail page
    /// </summary>
    public class DetailViewModel
    {
        /// <summary>
        /// The chosen Park
        /// </summary>
        public Park Park { get; set; }

        /// <summary>
        /// The 5-day forecast for the chosen Park
        /// </summary>
        public IList<WeatherData> FiveDayForecast { get; set; }

        /// <summary>
        /// Whether or not the user wants to use the Celsius scale for temperature
        /// </summary>
        public bool IsCelsius { get; set; }

        /// <summary>
        /// Whether or not the user wants to use the Imperial system for measurements
        /// </summary>
        public bool IsMetric { get; set; }
    }
}
