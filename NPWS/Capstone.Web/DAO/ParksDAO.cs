using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public class ParksDAO : IParksDAO
    {
        /// <summary>
        /// Database connection
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Constructor requiring the database connection
        /// </summary>
        /// <param name="connectionString">The database connection</param>
        public ParksDAO (string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Calls the database and creates a list of Parks containing their codes, names, states, and descriptions
        /// </summary>
        /// <returns>A list of ParkListViewModel</returns>
        public IList<ParkListViewModel> GetAllParks()
        {
            IList<ParkListViewModel> parks = new List<ParkListViewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT parkCode, parkName, state, parkDescription FROM park;", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ParkListViewModel park = new ParkListViewModel();
                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.ParkDescription = Convert.ToString(reader["parkDescription"]);


                    parks.Add(park);
                }
            }

            return parks;
        }

        /// <summary>
        /// Calls the database and creates a Park object containing all the Park's information
        /// </summary>
        /// <param name="parkCode">The Park Code of the desired Park</param>
        /// <returns>The Park corresponding to the given Park Code</returns>
        public Park GetPark(string parkCode)
        {
            Park park = new Park();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM park where parkCode = @ParkCode ;", conn);
                cmd.Parameters.AddWithValue("@ParkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.Acreage = Convert.ToInt32(reader["acreage"]);
                    park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                    park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                    park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                    park.Climate = Convert.ToString(reader["climate"]);
                    park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                    park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                    park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                    park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                    park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                    park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                }
            }

            return park;
        }

        /// <summary>
        /// Calls the database and creates a list of Park Code-Park Name pairs for all parks
        /// </summary>
        /// <returns>List of SelectListItem with Park Codes and Park Names</returns>
        public IList<SelectListItem> GetParkListItems()
        {
            List<SelectListItem> parks = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT parkCode, parkName FROM park;", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string parkCode = Convert.ToString(reader["parkCode"]);
                    string parkName = Convert.ToString(reader["parkName"]);

                    parks.Add(new SelectListItem(parkName, parkCode));
                }
            }

            return parks;
        }

        /// <summary>
        /// Calls the database and creates a 5-day forecast for the given Park
        /// </summary>
        /// <param name="parkCode">Park Code of the Park</param>
        /// <returns>List of WeatherData objects representing the 5-day forecast</returns>
        public IList<WeatherData> GetWeatherForecast(string parkCode)
        {
            IList<WeatherData> data = new List<WeatherData>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM weather where parkCode = @ParkCode ;", conn);
                cmd.Parameters.AddWithValue("@ParkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    WeatherData weatherData = new WeatherData();

                    weatherData.Day = Convert.ToInt32(reader["fiveDayForecastValue"]);
                    weatherData.LowInF = Convert.ToInt32(reader["low"]);
                    weatherData.HighInF = Convert.ToInt32(reader["high"]);
                    weatherData.Forecast = Convert.ToString(reader["forecast"]);

                    data.Add(weatherData);
                }
            }
            return data;
        }

        /// <summary>
        /// Calls the database inserts the Survey filled in by the user
        /// </summary>
        /// <param name="survey">The survey as filled in by the user</param>
        /// <returns>The number of rows affected</returns>
        public int SubmitSurvey(SurveyViewModel survey)
        {
            int numRows = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) "
                                                + "VALUES (@ParkCode, @Email, @State, @Activity)", conn);

                cmd.Parameters.AddWithValue("@ParkCode", survey.SelectedParkCode);
                cmd.Parameters.AddWithValue("@Email", survey.Email);
                cmd.Parameters.AddWithValue("@State", survey.SelectedState);
                cmd.Parameters.AddWithValue("@Activity", survey.ActivityLevel);

                numRows = cmd.ExecuteNonQuery();
            }

            return numRows;
        }

        /// <summary>
        /// Calls the database and creates a list of Park Codes of all the Parks
        /// </summary>
        /// <returns>List of Park Codes as strings</returns>
        public IList<string> GetParkCodes()
        {
            IList<string> parkCodes = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT parkCode FROM park;", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string code = Convert.ToString(reader["parkCode"]);

                    parkCodes.Add(code);
                }
            }

            return parkCodes;
        }

        /// <summary>
        /// Calls the database and creates a list of Parks that users have submitted surveys for
        /// </summary>
        /// <returns>List of FavoriteListViewModels</returns>
        public IList<FavoriteListViewModel> GetAllFavorites()
        {
            IList<FavoriteListViewModel> parks = new List<FavoriteListViewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT p.parkCode, p.parkName, p.state, p.parkDescription, COUNT(s.surveyId) AS numberSurveys "
                                            + "FROM park AS p JOIN survey_result AS s ON p.parkCode = s.parkCode "
                                            + "GROUP BY p.parkCode, p.parkName, p.state, p.parkDescription "
                                            + "ORDER BY numberSurveys DESC, p.parkName;", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FavoriteListViewModel park = new FavoriteListViewModel();
                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.NumberSurveys = Convert.ToInt32(reader["numberSurveys"]);

                    parks.Add(park);
                }
            }

            return parks;
        }
    }
}
