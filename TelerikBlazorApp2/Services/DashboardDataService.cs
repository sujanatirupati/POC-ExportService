﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelerikBlazorApp2.Models;

using Telerik.DataSource;
using Telerik.DataSource.Extensions;

namespace TelerikBlazorApp2.Services
{
    public class DashboardDataService
    {
        private static List<PodcastViewModel> podcasts = new List<PodcastViewModel>();
        private Random random = new Random();
        private List<string> firstNames = new List<string> { "Nancy", "Andrew", "Janet", "Margaret", "Steven", "Michael", "Robert", "Laura", "Anne", "Nige" };
        private List<string> lastNames = new List<string> { "Davolio", "Fuller", "Leverling", "Peacock", "Buchanan", "Suyama", "King", "Callahan", "Dodsworth", "White" };
        private List<string> platforms = new List<string> { "Apple Podcasts", "Spotify", "Other", "Overcast", "Anchor", "Stitcher" };
        private List<string> devices = new List<string> { "iOS", "Android", "Other", "Web" };

        // we generate some data and store it a static variable in this singleton service for simplicity
        // in a real app you should use actaul database and real time data and replace this data retrieval logic
        public DashboardDataService()
        {
            if (podcasts.Count == 0)
            {
                podcasts = Enumerable.Range(1, 50).Select(x => new PodcastViewModel()
                {
                    Name = string.Format("Episode #{0} with guest {1} {2}", random.Next(0, 200), firstNames[random.Next(0, firstNames.Count)], lastNames[random.Next(0, lastNames.Count)]),
                    Streams = random.Next(0, 18000),
                    Downloads = random.Next(0, 15000),
                    PlatformName = platforms[random.Next(0, platforms.Count)],
                    Device = devices[random.Next(0, devices.Count)],
                    Date = DateTime.Now.AddDays(-x),
                    Reach = x * random.Next(0, 1000),
                }).ToList();
            }
        }

        // methods from the service that are used across the components to get data
        public async Task<IEnumerable<PodcastViewModel>> GetPodcasts()
        {
            return await Task.FromResult(podcasts);
        }

        public async Task<int> GetStreams()
        {
            int result = podcasts.Sum(f => f.Streams);
            return await Task.FromResult(result);
        }

        public async Task<int> GetDownloads()
        {
            int result = podcasts.Sum(f => f.Downloads);
            return await Task.FromResult(result);
        }

        public async Task<int> GetReach()
        {
            int result = podcasts.Sum(f => f.Reach);
            return await Task.FromResult(result);
        }

        public async Task<double> GetRevenue()
        {
            double result = podcasts.Sum(f => f.Views) / 100;
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<PlatformViewModel>> GetPlatformData(bool byDevice)
        {
            var deviceViews = podcasts.GroupBy(x => byDevice == true ? x.Device : x.PlatformName)
                                .Select(x => new PlatformViewModel
                                {
                                    Category = x.Key,
                                    Views = x.Sum(v => v.Views)
                                });

            return await Task.FromResult(deviceViews);
        }
    }

    //public class WeatherForecastService
    //{
    //    private static readonly string[] Summaries = new[]
    //    {
    //        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //    };

    //    // this static list acts as our "database" in this sample
    //    private static IQueryable<WeatherForecast> _forecasts { get; set; }

    //    public async Task<IQueryable<WeatherForecast>> GetForecasts()
    //    {
    //        EnsureData();

    //        return await Task.FromResult(_forecasts);
    //    }

    //    public async Task<DataSourceResult> GetForecastListAsync(DataSourceRequest gridRequest)
    //    {
    //        EnsureData();

    //        DataSourceResult dataToReturn = await _forecasts.ToDataSourceResultAsync(gridRequest);

    //        return dataToReturn;
    //    }

    //    private void EnsureData()
    //    {
    //        // generate some data for the sake of this demo
    //        if (_forecasts == null)
    //        {
    //            var startDate = DateTime.Now.Date;
    //            _forecasts = Enumerable.Range(1, 5000).Select(index => new WeatherForecast
    //            {
    //                Id = index,
    //                Date = startDate.AddDays(index),
    //                //unlike the MS template, we will not have random data here because it may get generated anew when exporting
    //                //and new random values may make it seem like the data/filter/exporting does not work as expected
    //                TemperatureC = index % 40,
    //                Summary = Summaries[index % Summaries.Length]
    //            }).AsQueryable();
    //        }
    //    }
    //}
}
