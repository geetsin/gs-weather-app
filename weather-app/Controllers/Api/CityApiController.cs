﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using weather_app.Models.DataModels;
using weather_app.Models.ViewModels;
using weather_app.Services;
using weather_app.Utilities;

namespace weather_app.Controllers.Api
{
    [Route("api/city")]
    [ApiController]
    public class CityApiController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityApiController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// Gets the a list of city name suggestions as per characters input. Minimum is 2 characters.
        /// </summary>
        /// <returns>The list of city name suggestions.</returns>
        /// <response code="200">Returns the List of city suggestions</response>
        /// <response code="400">If the input is null</response>   
        /// <response code="404">If the there is no city suggestion matching input</response>   
        /// <response code="429">If the rate of 500 req/month or 5 req/10sec exceeded</response>   
        [HttpGet]
        [Route("autocomplete/{input}")]
        [EnableQuery]
        public IActionResult GetCityAutocompleteSuggestions(string input)
        {
            CommonResponse<List<String>> commonResponse = new CommonResponse<List<String>>();
            if (input.Length < 2)
            {
                commonResponse.dataenum = null;
                commonResponse.message = "Error! Input of minimum length 3 characters is required";
                commonResponse.status = Helper.CODE_FAILURE;
                return BadRequest(commonResponse);
            }
            try
            {
                List<String> cityList = _cityService.GetCityAutocompleteSuggestions(input).Result;
                if (cityList.Count > 0)
                {
                    commonResponse.dataenum = cityList;
                    commonResponse.message = "Success! Autocomplete suggestion received";
                    commonResponse.status = Helper.CODE_SUCCESS;
                }
                else
                {
                    commonResponse.dataenum = null;
                    commonResponse.message = "Error! Could not find autocomplete suggestions";
                    commonResponse.status = Helper.CODE_FAILURE;
                    return NotFound(commonResponse);
                }
            }
            catch (Exception ex)
            {
                commonResponse.message = ex.Message;
                commonResponse.status = Helper.CODE_FAILURE;
            }
            return Ok(commonResponse);
        }

        /// <summary>
        /// Gets the details of a city like timezone, alternate names, populaition, country name, country code.
        /// </summary>
        /// <returns>The details of a city.</returns>
        /// <response code="200">Returns the details of the input city</response>
        /// <response code="400">If the input is null</response>   
        /// <response code="404">If the there is no city matching input</response>   
        /// <response code="429">If the rate of 500 req/month or 5 req/10sec exceeded</response>   
        [HttpGet]
        [Route("details/{input}")]
        [EnableQuery]
        public IActionResult GetCityDetails(string input)
        {
            CommonResponse<List<CityDetailsObj>> commonResponse = new CommonResponse<List<CityDetailsObj>>();
            try
            {
                List<CityDetailsObj> cityList = _cityService.GetCityDetails(input).Result;
                if (cityList.Count > 0)
                {
                    commonResponse.dataenum = cityList;
                    commonResponse.message = "Success! Autocomplete suggestion received";
                    commonResponse.status = Helper.CODE_SUCCESS;
                }
                else
                {
                    commonResponse.dataenum = null;
                    commonResponse.message = "Error! Could not find autocomplete suggestions";
                    commonResponse.status = Helper.CODE_FAILURE;
                    return NotFound(commonResponse);
                }
            } catch (Exception ex)
            {
                commonResponse.message = ex.Message;
                commonResponse.status = Helper.CODE_FAILURE;
            }
            return Ok(commonResponse);
        }
    }
}
