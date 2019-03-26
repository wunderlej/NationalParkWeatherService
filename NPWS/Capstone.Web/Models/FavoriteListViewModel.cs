using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    /// <summary>
    /// View model used for the Favorites page
    /// </summary>
    public class FavoriteListViewModel
    {
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
        /// Number of surveys submitted for the given Park
        /// </summary>
        public int NumberSurveys { get; set; }
    }
}
