using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    /// <summary>
    /// View model used for the Survey pages
    /// </summary>
    public class SurveyViewModel
    {
        /// <summary>
        /// List of possible States as SelectListItem objects
        /// </summary>
        public static IList<SelectListItem> States = new List<SelectListItem>()
        {
                    new SelectListItem {Text = "Alabama" },
                    new SelectListItem {Text = "Alaska" },
                    new SelectListItem {Text = "Arizona" },
                    new SelectListItem {Text = "Arkansas" },
                    new SelectListItem {Text = "California" },
                    new SelectListItem {Text = "Colorado" },
                    new SelectListItem {Text = "Connecticut" },
                    new SelectListItem {Text = "Delaware" },
                    new SelectListItem {Text = "Florida" },
                    new SelectListItem {Text = "Georgia" },
                    new SelectListItem {Text = "Hawaii" },
                    new SelectListItem {Text = "Idaho" },
                    new SelectListItem {Text = "Illinois" },
                    new SelectListItem {Text = "Indiana" },
                    new SelectListItem {Text = "Iowa" },
                    new SelectListItem {Text = "Kansas" },
                    new SelectListItem {Text = "Kentucky" },
                    new SelectListItem {Text = "Louisiana" },
                    new SelectListItem {Text = "Maine" },
                    new SelectListItem {Text = "Maryland" },
                    new SelectListItem {Text = "Massachusetts" },
                    new SelectListItem {Text = "Michigan" },
                    new SelectListItem {Text = "Minnesota" },
                    new SelectListItem {Text = "Mississippi" },
                    new SelectListItem {Text = "Missouri" },
                    new SelectListItem {Text = "Montana" },
                    new SelectListItem {Text = "North Carolina" },
                    new SelectListItem {Text = "North Dakota" },
                    new SelectListItem {Text = "Nebraska" },
                    new SelectListItem {Text = "Nevada" },
                    new SelectListItem {Text = "New Hampshire" },
                    new SelectListItem {Text = "New Jersey" },
                    new SelectListItem {Text = "New Mexico" },
                    new SelectListItem {Text = "New York" },
                    new SelectListItem {Text = "Ohio" },
                    new SelectListItem {Text = "Oklahoma" },
                    new SelectListItem {Text = "Oregon" },
                    new SelectListItem {Text = "Pennsylvania" },
                    new SelectListItem {Text = "Rhode Island" },
                    new SelectListItem {Text = "South Carolina" },
                    new SelectListItem {Text = "South Dakota" },
                    new SelectListItem {Text = "Tennessee" },
                    new SelectListItem {Text = "Texas" },
                    new SelectListItem {Text = "Utah" },
                    new SelectListItem {Text = "Vermont" },
                    new SelectListItem {Text = "Virginia" },
                    new SelectListItem {Text = "Washington" },
                    new SelectListItem {Text = "West Virginia" },
                    new SelectListItem {Text = "Wisconsin" },
                    new SelectListItem {Text = "Wyoming" }
        };

        /// <summary>
        /// List of possible Activity Levels as strings
        /// </summary>
        public static IList<string> ActivityLevels = new List<string>()
        {
            "Inactive",
            "Sedentary",
            "Active",
            "Extremely Active"
        };

        /// <summary>
        /// List of possible Parks as SelectListItem objects
        /// </summary>
        public IList<SelectListItem> Parks { get; set; }

        /// <summary>
        /// Email address provided by the user
        /// </summary>
        [Display(Name = "Your email")]
        [Required(ErrorMessage = "Must enter an email")]
        [EmailAddress(ErrorMessage = "Must enter valid email address")]
        public string Email { get; set; }

        /// <summary>
        /// Activity Level selected by the user
        /// </summary>
        [Display(Name = "Activity Level")]
        [Required(ErrorMessage = "Must select an activity level")]
        public string ActivityLevel { get; set; }

        /// <summary>
        /// Park Code of the Park selected by the User
        /// </summary>
        [Display(Name = "Favorite National Park")]
        [Required(ErrorMessage = "Must select a park")]
        public string SelectedParkCode { get; set; }

        /// <summary>
        /// State selected by the user
        /// </summary>
        [Display(Name = "State of residence")]
        [Required(ErrorMessage = "Must select a state")]
        public string SelectedState { get; set; }
    }
}
