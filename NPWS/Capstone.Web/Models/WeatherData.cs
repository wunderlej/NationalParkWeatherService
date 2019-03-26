using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    /// <summary>
    /// Object representing the weather for a specific day. Includes the weekday, high and low in Fahrenheit, forecast, and any relevant warnings
    /// </summary>
    public class WeatherData
    {
        #region Member Variables

        /// <summary>
        /// Temperature threshold in Fahrenheit for gauging a hot day
        /// </summary>
        private const int _tooHot = 75;

        /// <summary>
        /// Temperature threshold in Fahrenheit for gauging an extreme difference between the High and Low Temperatures
        /// </summary>
        private const int _drasticChange = 20;

        /// <summary>
        /// Temperature threshold in Fahrenheit for gauging a cold day
        /// </summary>
        private const int _tooCold = 20;

        /// <summary>
        /// Represents zero relevant warning messages for the Day's weather
        /// </summary>
        private const int _noWarningMessages = 0;

        /// <summary>
        /// List of warning messages pertaining to different forcasts
        /// </summary>
        private static Dictionary<string, string> _forcastWarnings = new Dictionary<string, string>()
        {
            { "snow", "Pack snowshoes!" },
            { "rain", "Pack rain gear and wear waterproof shoes!" },
            { "thunderstorms", "Seek shelter! Avoid hiking on exposed ridges!" },
            { "sunny", "Pack sunblock!" }
        };

        /// <summary>
        /// Warning message for hot days
        /// </summary>
        private const string _tooHotWarning = "Bring an extra gallon of water!";

        /// <summary>
        /// Warning message for drastic high/low temperature differences
        /// </summary>
        private const string _drasticChangeWarning = "Wear breathable layers!";

        /// <summary>
        /// Warning message for cold days
        /// </summary>
        private const string _tooColdWarning = "It's cold! Bundle up!";

        /// <summary>
        /// "Warning" message given for a perfect day (no other relevant warnings)
        /// </summary>
        private const string _perfectMessage = "Perfect day!";

        /// <summary>
        /// Day integer representation for today
        /// </summary>
        private const int _today = 1;

        #endregion

        #region Properties

        /// <summary>
        /// Day of the week (starting at 1 as Today)
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Returns the Name of the day as a string based on the given Day property and the current day of the week (ex. if Today is Monday and Day property is 2, results in 'Tuesday')
        /// </summary>
        public string WeekDay
        {
            get
            {
                string result;

                if(IsToday())
                {
                    result = "Today";
                }
                else
                {
                    result = GetDayName(Day);
                }

                return result;
            }
        }

        /// <summary>
        /// The Day's lowest temperature in Fahrenheit
        /// </summary>
        public int LowInF { get; set; }

        /// <summary>
        /// The Day's highest temperature in Fahrenheit
        /// </summary>
        public int HighInF { get; set; }

        /// <summary>
        /// The Day's forecast (sunny, cloudy, snow, etc.)
        /// </summary>
        public string Forecast { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Takes a temperature in Fahrenheit and converts it to Celsius
        /// </summary>
        /// <param name="tempInF">The Temperature in Fahrenheit</param>
        /// <returns>The temperature in Celsius</returns>
        public int ConvertToC(int tempInF)
        {
            return (int)((tempInF - 32)*(5D/9));
        }

        /// <summary>
        /// Returns a string representation of the Day's high temperature with the measurement and scale
        /// </summary>
        /// <param name="isCelsius">True if the desired scale is Celsius, false if Fahrenheit</param>
        /// <returns>String containing the measure and scale</returns>
        public string HighTempString(bool isCelsius)
        {
            if (!isCelsius)
            {
                return $"{HighInF} °F";
            }
            else
            {
                return $"{ConvertToC(HighInF)} °C";
            }
        }

        /// <summary>
        /// Returns a string representation of the Day's low temperature with the measurement and scale
        /// </summary>
        /// <param name="isCelsius">True if the desired scale is Celsius, false if Fahrenheit</param>
        /// <returns>String containing the measure and scale</returns>
        public string LowTempString(bool isCelsius)
        {
            if (!isCelsius)
            {
                return $"{LowInF} °F";
            }
            else
            {
                return $"{ConvertToC(LowInF)} °C";
            }
        }

        /// <summary>
        /// Generates a list of advisory warnings for the Day's forcast. Returns a single 'Perfect Day' message if no warnings apply
        /// </summary>
        /// <returns>List of relevant warning messages</returns>
        public List<string> WarningMessages()
        {
            List<string> messages = new List<string>();

            if(_forcastWarnings.ContainsKey(Forecast))
            {
                messages.Add(_forcastWarnings[Forecast]);
            }

            if (IsTooHot())
            {
                messages.Add(_tooHotWarning);
            }

            if (IsDrasticChange())
            {
                messages.Add(_drasticChangeWarning);
            }

            if (IsTooCold())
            {
                messages.Add(_tooColdWarning);
            }

            if(NoRelevantWarnings(messages))
            {
                messages.Add(_perfectMessage);
            }

            return messages;
        }

        /// <summary>
        /// Returns true if the Day's High will be 75 degrees Fahrenheit or above
        /// </summary>
        /// <returns>True if hot</returns>
        private bool IsTooHot()
        {
            return (HighInF >= _tooHot);
        }

        /// <summary>
        /// Returns true if the difference between the Day's High and Low is greater than 20 degrees Fahrenheit
        /// </summary>
        /// <returns>True if the difference between High and Low is greater than 20</returns>
        private bool IsDrasticChange()
        {
            return ((HighInF - LowInF) > _drasticChange);
        }

        /// <summary>
        /// Returns true if the Day's Low will be 20 degrees Fahrenheit or below
        /// </summary>
        /// <returns>True if cold</returns>
        private bool IsTooCold()
        {
            return (LowInF <= _tooCold);
        }

        /// <summary>
        /// Returns true if the Day's weather has no relevant warning messages applied.
        /// </summary>
        /// <param name="messages">List of possible warning messages</param>
        /// <returns>True if no relevant warning messages</returns>
        private bool NoRelevantWarnings(List<string> messages)
        {
            return (messages.Count == _noWarningMessages);
        }

        /// <summary>
        /// Returns true if the Weather Data is for today
        /// </summary>
        /// <returns>True if data is for today</returns>
        private bool IsToday()
        {
            return (Day == _today);
        }

        /// <summary>
        /// Returns the Name of the Weekday for a futre date for the given Day property counting from 1 being Today
        /// </summary>
        /// <param name="day">The WeatherData's Day property</param>
        /// <returns>Name of the Weekday as a string</returns>
        private string GetDayName(int day)
        {
            int daysFromNow = day - 1;

            DateTime currentDay = DateTime.Now;

            DateTime futureDay = currentDay.AddDays(daysFromNow);

            string dayName = futureDay.DayOfWeek.ToString();

            return dayName;
        }

        #endregion
    }
}
