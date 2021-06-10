using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class WeatherForecastList
    {
        private List<WeatherForecast> ForecastList;
        public WeatherForecastList() => ForecastList = new List<WeatherForecast>();

        /// <summary>
        /// Return all List.
        /// </summary>
        /// <returns></returns>
        public List<WeatherForecast> GetList() => ForecastList;

        /// <summary>
        /// Add Weather Forecast to List
        /// </summary>
        /// <param name="weather"></param>
        public void Add(WeatherForecast weather) => ForecastList.Add(weather);

        /// <summary>
        /// Find Forecast for <paramref name="date"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WeatherForecast FindForecast(DateTime date)
        {
            for (int i = 0; i < ForecastList.Count; i++)
            {
                if (ForecastList[i].Date == date) return ForecastList[i];
            }
            return null;
        }

        /// <summary>
        /// Get Forecasts List from <paramref name="start"/> date to <paramref name="end"/> date
        /// </summary>
        /// <param name="start">Starting date</param>
        /// <param name="end">Ending date</param>
        /// <returns></returns>
        public List<WeatherForecast> GetRangeForecasts(DateTime start, DateTime end)
        {
            List<WeatherForecast> result = new List<WeatherForecast>();
            for (int i = 0; i < ForecastList.Count; i++)
            {
                if ((ForecastList[i].Date >= start) & (ForecastList[i].Date <= end)) result.Add(ForecastList[i]);
            }
            return result;
        }
    }
}
