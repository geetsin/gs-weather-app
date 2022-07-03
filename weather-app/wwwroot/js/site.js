// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.
$(document).ready(function () {

});

var routeURL = location.protocol + "//" + location.host;
var autocompleteSuggestions;

function getCityWeatherAndWallpaper() {
    var cityName = $('#cityInput').val().toLowerCase(); // Get the city name in lowercase
    // Hide any city weather data showing from previous search
    hideWeatherDataElement();

    getCityWallpaper(cityName);
    getCityWeather(cityName);
}

function getCityNameSuggestions() {
    
    if ($('#cityInput').val().length == 2) {
        const localApiURL = routeURL + '/api/city/autocomplete/' + $('#cityInput').val();
        console.log("**LOG: API URL: ", localApiURL);

        $.ajax({
            url: localApiURL,
            type: 'GET',
            dataType: 'JSON',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response.status == 1 && response.dataenum != undefined) {
                    console.log("**LOG: Autocomplete suggestion received ");
                    autocompleteSuggestions = response.dataenum;
                    displayAutocompleteSuggestions(autocompleteSuggestions);

                } else {
                    console.log("**LOG: ERROR returned from API 'city/autocomplete':  ", response.message);
                }
            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                console.log("** LOG: ", msg);
            },
        });
    } else {
        displayAutocompleteSuggestions(autocompleteSuggestions);
    }
}

function displayAutocompleteSuggestions(suggestions) {
    $("#cityInput").autocomplete({
        source: suggestions
    });
}

// Get the respective city wallpaer and set it as wallpaper
function getCityWallpaper(cityNameInput) {
    let cityNameArr = cityNameInput.split(',');

    const localApiURL = routeURL + '/api/wallpaper/' + cityNameArr[0];
    console.log("**LOG: API URL: ", localApiURL);

    // jQuery API call to get wallpaper location from WebAPI api/GetLocationWallpaper/
    $.ajax({
        url: localApiURL,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response.status == 1 && response.dataenum != undefined) {
                var wallPaperSource = response.dataenum;
                console.log("**LOG: Wallpaper URL: ", wallPaperSource);
                document.body.style.backgroundImage = "url(" + wallPaperSource + ")"; // Set the background wallpaper
            } else {
                console.log("**LOG: ERROR returned 'wallpaper' from API:  ", response.message);
            }
        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            console.log("** LOG: ", msg);
        },
    });
}

function getCityWeather(cityName) {
    const localApiURL = routeURL + '/api/weather/' + cityName;
    console.log("**LOG: API URL: ", localApiURL);

    $.ajax({
        url: localApiURL,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'application/json: charset=utf-8',
        success: function (response) {
            console.log("**LOG: 'weather' API call Success");
            if (response.status == 1 && response.dataenum != undefined) {
                var cityWeather = response.dataenum;
                displayWeatherData(cityWeather);
            } else {
                console.log("**LOG: ERROR returned from 'weather' API:", response.message);
            }
        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            console.log("** LOG: ", msg);
        },
    });
}

function displayWeatherData(weatherData) {

    // Set Icon
    var iconurl = "http://openweathermap.org/img/wn/" + weatherData.cityWeatherIcon + ".png";
    $('#wIcon').attr('src', iconurl);

    // Set City Name
    var fullCityName = weatherData.cityName + ", " + weatherData.cityCountryCode;
    $('#wCity').text(fullCityName);

    // Set weather description
    $('#wCondition').text(weatherData.cityWeatherDescription);

    // Set weather description
    var cityTemp = weatherData.cityTemp + "°C";
    $('#wTemp').text(cityTemp);

    // Set temp feels like
    var tempFeelsLike = weatherData.cityTempFeelsLike + "°C";
    $('#wFeelsLike').text(tempFeelsLike);

    // Set Min temp
    var cityTempMin = weatherData.cityTempMin + "°C";
    $('#wMinTemp').text(cityTempMin);

    // Set Max temp
    var cityTempMax = weatherData.cityTempMax + "°C";
    $('#wMaxTemp').text(cityTempMax);

    // Set Humidity
    var cityHumidity = weatherData.cityHumidity + "%";
    $('#wHumidity').text(cityHumidity);

    // Set air pressure
    var cityAirPressure = weatherData.cityPressure + " hPa";
    $('#wPressure').text(cityAirPressure);

    // Make the box larger and show the data
    showWeatherDataElement();
    
}

function hideWeatherDataElement() {
    $("#wData").css("visibility", "hidden");
    setTimeout(function () {

        $("#box").css('height', 200 + "px");

    }, 500);
}

function showWeatherDataElement() {
    $("#box").css('height', 470 + "px");
    setTimeout(function () {

        $("#wData").css("visibility", "visible");

    }, 250);
}