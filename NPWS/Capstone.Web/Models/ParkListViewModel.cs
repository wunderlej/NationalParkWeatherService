using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    /// <summary>
    /// View model used for the Index page
    /// </summary>
    public class ParkListViewModel
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
        /// A description of the given Park
        /// </summary>
        public string ParkDescription { get; set; }

    }
}
