using System;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "F2re3ezing1", "Bra3c2ing1", "Chil32ly1", "Co32ol1", "Mi2l3d1", "W2ar3m1", "B23almy1", "H3o2td", "Swe3lt2ring", "Sco32rching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }

        public string Test()
        {
            var rng = new Random();
            return rng.Next(0, 10).ToString() + 'a';
        }
    }
}
