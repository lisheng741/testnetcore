namespace SwaggerTest
{
    /// <summary>
    /// 天气预报类
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 概述
        /// </summary>
        public string? Summary { get; set; }
    }
}