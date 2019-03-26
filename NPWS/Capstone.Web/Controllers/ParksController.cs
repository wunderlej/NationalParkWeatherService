using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Controllers
{
    public class ParksController : SessionController
    {
        /// <summary>
        /// Error message used if final survey submission fails
        /// </summary>
        private const string _surveySubmissionErrorMessage = "Something went wrong during submission. Please contact tech support";

        private IParksDAO _parkDAO;

        public ParksController(IParksDAO parkDAO)
        {
            _parkDAO = parkDAO;
        }

        /// <summary>
        /// Home page. Displays a list of all the Parks
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IList<ParkListViewModel> parks = _parkDAO.GetAllParks();

            return View(parks);
        }

        /// <summary>
        /// Park Detail page. Displays all information about a particular Park as well as the Park's 5-day forecast
        /// </summary>
        /// <param name="parkCode">Park Code of the desired Park</param>
        /// <returns></returns>
        public IActionResult Detail(string parkCode)
        {
            string code;

            //parkCode parameter check. Null if redirected by the SwitchDegree HttpPost
            if(parkCode == null)
            {
                //Park Code provided by the SwitchDegree HttpPost
                code = GetTempData<string>("code");
            }
            else
            {
                code = parkCode;
            }

            DetailViewModel vm = new DetailViewModel();
            vm.Park = _parkDAO.GetPark(code);
            vm.FiveDayForecast = _parkDAO.GetWeatherForecast(code);
            //Check on session data to determine if the user wants Fahrenheit or Celsius scale
            vm.IsCelsius = GetSessionData<bool>("IsCelsius");
            //Check on session data to determine if the user wants Imperial or Metric systems
            vm.IsMetric = GetSessionData<bool>("IsMetric");

            return View(vm);
        }

        /// <summary>
        /// HttpPost enacted on the Park Detail page to switch between Fahrenheit and Celsius scales
        /// </summary>
        /// <param name="isThisCelsius">Currently selected scale</param>
        /// <param name="parkCode">Park Code of the Park displayed on the Park Details page</param>
        /// <returns>Redirects back to the Park Details page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwitchDegree(bool isThisCelsius, string parkCode)
        {
            if (isThisCelsius)
            {
                SetSessionData("IsCelsius", false);
            }
            else
            {
                SetSessionData("IsCelsius", true);
            }

            SetTempData<string>("code", parkCode);

            return RedirectToAction("Detail", "Parks");
        }

        /// <summary>
        /// Survey page. Allows the user to submit a survey for their favorite Park
        /// </summary>
        /// <returns></returns>
        public IActionResult Survey()
        {
            SurveyViewModel survey = new SurveyViewModel();
            survey.Parks = _parkDAO.GetParkListItems();

            return View(survey);
        }

        /// <summary>
        /// HttpPost handling the survey submitted by the user
        /// </summary>
        /// <param name="survey">Survey submitted by the user</param>
        /// <returns>Redirects to Favorites page if successfull. Stays on Survey page otherwise</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSurvey(SurveyViewModel survey)
        {
            IActionResult result;
            
            //List of Parks. Needed if survey submission fails and redirects to Survey page (precautionary)
            survey.Parks = _parkDAO.GetParkListItems();

            bool validState = false;
            //Checks to see if the State provided by the user is one of the 50 US States (used below)
            foreach (SelectListItem item in SurveyViewModel.States)
            {
                if (item.Text.Equals(survey.SelectedState))
                {
                    validState = true;
                }
            }

            if (!ModelState.IsValid)/*Checks to see if base Survey object validations have passed*/
            {
                result = View("Survey", survey);
            }
            else if (!(_parkDAO.GetParkCodes().Contains(survey.SelectedParkCode)))/*Checks to see if the Park Code provided by the user is valid*/
            {
                ModelState.AddModelError("invalidCode", "Selected park not recognized");

                result = View("Survey", survey);
            }
            else if (!validState)/*Valid State bool created above*/
            {
                ModelState.AddModelError("invalidState", "Selected state not recognized");

                result = View("Survey", survey);
            }
            else if (!(SurveyViewModel.ActivityLevels.Contains(survey.ActivityLevel)))/*Checks to see if the Activity Level provided by the user is valid*/
            {
                ModelState.AddModelError("invalidActivity", "Selected activity level not recognized");

                result = View("Survey", survey);
            }
            else
            {
                try
                {/*Attempts to insert survey into database. If fails or does not affect any rows, throws an exception*/
                    int rowsAffected = _parkDAO.SubmitSurvey(survey);

                    if (rowsAffected != 1)
                    {
                        throw new Exception();
                    }
                    else
                    {/*Redirects to Favorites page when successfully submitted*/
                        result = RedirectToAction("Favorites");
                    }
                }
                catch
                {/*Catches exceptions thrown from database insert attempt and redirects to Survey page to display submission error*/
                    ModelState.AddModelError("submitBroke", _surveySubmissionErrorMessage);

                    result = View("Survey", survey);
                }
            }

            return result;
        }

        /// <summary>
        /// Favorites page. Displays a list of Parks that users have submitted surveys for
        /// </summary>
        /// <returns></returns>
        public IActionResult Favorites()
        {
            IList<FavoriteListViewModel> parks = _parkDAO.GetAllFavorites();

            return View(parks);
        }

        /// <summary>
        /// HttpPost enacted on the Park Detail page to switch between Imperial and Metric systems
        /// </summary>
        /// <param name="isThisMetric">Currently selected system</param>
        /// <param name="parkCode">Park Code of the Park displayed on the Park Details page</param>
        /// <returns>Redirects back to the Park Details page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwitchSystem(bool isThisMetric, string parkCode)
        {
            if (isThisMetric)
            {
                SetSessionData("IsMetric", false);
            }
            else
            {
                SetSessionData("IsMetric", true);
            }

            SetTempData<string>("code", parkCode);

            return RedirectToAction("Detail", "Parks");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
