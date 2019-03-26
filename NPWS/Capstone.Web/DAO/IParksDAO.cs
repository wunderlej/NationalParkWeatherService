using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public interface IParksDAO
    {
        /// <summary>
        /// Calls the database and creates a list of Parks containing their codes, names, states, and descriptions
        /// </summary>
        /// <returns>A list of ParkListViewModel</returns>
        IList<ParkListViewModel> GetAllParks();

        /// <summary>
        /// Calls the database and creates a Park object containing all the Park's information
        /// </summary>
        /// <param name="parkCode">The Park Code of the desired Park</param>
        /// <returns>The Park corresponding to the given Park Code</returns>
        Park GetPark(string parkCode);

        /// <summary>
        /// Calls the database and creates a 5-day forecast for the given Park
        /// </summary>
        /// <param name="parkCode">Park Code of the Park</param>
        /// <returns>List of WeatherData objects representing the 5-day forecast</returns>
        IList<WeatherData> GetWeatherForecast(string parkCode);

        /// <summary>
        /// Calls the database and creates a list of Park Code-Park Name pairs for all parks
        /// </summary>
        /// <returns>List of SelectListItem with Park Codes and Park Names</returns>
        IList<SelectListItem> GetParkListItems();

        /// <summary>
        /// Calls the database inserts the Survey filled in by the user
        /// </summary>
        /// <param name="survey">The survey as filled in by the user</param>
        /// <returns>The number of rows affected</returns>
        int SubmitSurvey(SurveyViewModel survey);

        /// <summary>
        /// Calls the database and creates a list of Park Codes of all the Parks
        /// </summary>
        /// <returns>List of Park Codes as strings</returns>
        IList<string> GetParkCodes();

        /// <summary>
        /// Calls the database and creates a list of Parks that users have submitted surveys for
        /// </summary>
        /// <returns>List of FavoriteListViewModels</returns>
        IList<FavoriteListViewModel> GetAllFavorites();
    }
}
