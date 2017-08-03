using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLib.Model {
  public class WeatherResponse :BaseResponse{
    public List<HeWeather5> HeWeather5 { get; set; }
  }
  public class HeWeather5 {
    /// <summary>
    /// 接口状态
    /// </summary>
    public string status { get; set; }
    public Basic basic { get; set; }
    public List<Daily_forecast> daily_forecast { get; set; }
    public Now now { get; set; }
    public List<Hourly_forecast> hourly_forecast { get; set; }
    public Suggestion suggestion { get; set; }
    /// <summary>
    /// 灾害预警，若所在城市无预警则不显示该字段，仅限国内城市
    /// </summary>
    public List<Alarms> alarms { get; set; }
    /// <summary>
    /// 空气质量
    /// </summary>
    public Aqi aqi { get; set; }
  }
  /// <summary>
  /// aqi空气质量指数
  /// </summary>
  public class Aqi {
    public City city { get; set; }
  }
  public class City{
    public decimal aqi { get; set; }
    public decimal co { get; set; }
    public decimal no2 { get; set; }
    public decimal o3 { get; set; }
    public decimal pm10 { get; set; }
    public decimal pm25 { get; set; }
    public decimal so2 { get; set; }
    /// <summary>
    /// 共六个级别，分别：优，良，轻度污染，中度污染，重度污染，严重污染
    /// </summary>
    public string qlty { get; set; }

  }
  /// <summary>
  /// 基本信息
  /// </summary>
  public class Basic {
    /// <summary>
    /// 城市名称
    /// </summary>
    [JsonProperty(PropertyName = "city")]
    public string City { get; set; }
    /// <summary>
    /// 国家
    /// </summary>
    [JsonProperty(PropertyName = "cnty")]
    public string Cnty { get; set; }
    /// <summary>
    /// 城市ID
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string ID { get; set; }
    /// <summary>
    /// 城市纬度
    /// </summary>
    [JsonProperty(PropertyName = "lat")]
    public decimal Lat { get; set; }
    /// <summary>
    /// 城市经度
    /// </summary>
    [JsonProperty(PropertyName = "lon")]
    public decimal Lon { get; set; }
    /// <summary>
    /// 城市所属省份（国内城市）
    /// </summary>
    [JsonProperty(PropertyName = "prov")]
    public string Prov { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    [JsonProperty(PropertyName = "update")]
    public UpdateTime Update { get; set; }
  }
  /// <summary>
  /// 更新时间
  /// </summary>
  public class UpdateTime{
    /// <summary>
    /// 当地时间
    /// </summary>
    [JsonProperty(PropertyName = "loc")]
    public DateTime? Loc { get; set; }
    /// <summary>
    /// utc时间
    /// </summary>
    [JsonProperty(PropertyName = "utc")]
    public DateTime? Utc { get; set; }
  }
  /// <summary>
  /// 7-10天天气预报
  /// </summary>
  public class Daily_forecast {
    public Astro astro { get; set; }
    public Cond cond { get; set; }
    /// <summary>
    /// 预报日期
    /// </summary>
    public string date { get; set; }
    /// <summary>
    /// 相对适度 %
    /// </summary>
    public Int16 hum { get; set; }
    /// <summary>
    /// 降水量 （mm）
    /// </summary>
    public decimal pcpn { get; set; }
    /// <summary>
    /// 江水概率
    /// </summary>
    public decimal pop { get; set; }
    /// <summary>
    /// 气压
    /// </summary>
    public decimal pres { get; set; }
    public Temperature tmp { get; set; }
    /// <summary>
    /// 能见度(km)
    /// </summary>
    public decimal vis { get; set; }
    public Wind wind { get; set; }
  }

  /// <summary>
  /// 天文数值
  /// </summary>
  public class Astro {
    /// <summary>
    /// 月升时间
    /// </summary>
    public string mr { get; set; }
    /// <summary>
    /// 月落时间
    /// </summary>
    public string ms { get; set; }
    /// <summary>
    /// 日出时间
    /// </summary>
    public string sr { get; set; }
    /// <summary>
    /// 日落时间
    /// </summary>
    public string ss { get; set; }
  }
  /// <summary>
  /// 天气状况
  /// </summary>
  public class Cond {
    /// <summary>
    /// 白天天气状况代码
    /// </summary>
    public Int16 code_d { get; set; }
    /// <summary>
    /// 夜间天气状况代码
    /// </summary>
    public Int16 code_n { get; set; }
    /// <summary>
    /// 白天天气状况描述
    /// </summary>
    public string txt_d { get; set; }
    /// <summary>
    /// 夜间天气描述
    /// </summary>
    public string txt_n { get; set; }
  }
  /// <summary>
  /// 温度
  /// </summary>
  public class Temperature {
    public decimal max { get; set; }
    public decimal min { get; set; }
  }
  /// <summary>
  /// 风向风力
  /// </summary>
  public class Wind {
    /// <summary>
    /// 风向（360度）
    /// </summary>
    public decimal deg { get; set; }
    /// <summary>
    /// 风向
    /// </summary>
    public string dir { get; set; }
    /// <summary>
    /// 风力等级
    /// </summary>
    public string sc { get; set; }
    /// <summary>
    /// 风速（kmph）
    /// </summary>
    public decimal spd { get; set; }
  }
  /// <summary>
  /// 实况天气
  /// </summary>
  public class Now {
    /// <summary>
    /// 天气状况
    /// </summary>
    public Cond cond { get; set; }
    /// <summary>
    /// 体感温度
    /// </summary>
    public decimal fl { get; set; }
    /// <summary>
    /// 相对湿度
    /// </summary>
    public decimal hum { get; set; }
    /// <summary>
    /// 降水量
    /// </summary>
    public decimal pcpn { get; set; }
    /// <summary>
    /// 气压
    /// </summary>
    public decimal pres { get; set; }
    /// <summary>
    /// 温度
    /// </summary>
    public decimal tmp { get; set; }
    /// <summary>
    /// 能见度
    /// </summary>
    public decimal vis { get; set; }
    /// <summary>
    /// 风力风向
    /// </summary>
    public Wind wind { get; set; }
  }
  /// <summary>
  /// 小时预报
  /// </summary>
  public class Hourly_forecast {
    public Cond cond { get; set; }
    /// <summary>
    /// 时间
    /// </summary>
    public DateTime date { get; set; }
    /// <summary>
    /// 相对湿度（%）
    /// </summary>
    public decimal hum { get; set; }
    /// <summary>
    /// 降水概率
    /// </summary>
    public decimal pop { get; set; }
    /// <summary>
    /// 气压
    /// </summary>
    public decimal pres { get; set; }
    /// <summary>
    /// 温度
    /// </summary>
    public decimal tmp { get; set; }
    public Wind wind { get; set; }
  }
  /// <summary>
  /// 生活指数
  /// </summary>
  public class Suggestion {
    /// <summary>
    /// 舒适度指数
    /// </summary>
    public Sugg_Item comf { get; set; }
    /// <summary>
    /// 洗车指数
    /// </summary>
    public Sugg_Item cw { get; set; }
    /// <summary>
    /// 穿衣指数
    /// </summary>
    public Sugg_Item drsg { get; set; }
    /// <summary>
    /// 感冒指数
    /// </summary>
    public Sugg_Item flu { get; set; }
    /// <summary>
    /// 运动指数
    /// </summary>
    public Sugg_Item sport { get; set; }
    /// <summary>
    /// 旅游指数
    /// </summary>
    public Sugg_Item trav { get; set; }
    /// <summary>
    /// 紫外线指数
    /// </summary>
    public Sugg_Item uv { get; set; }
  }
  /// <summary>
  /// 生活指数详细
  /// </summary>
  public class Sugg_Item {
    /// <summary>
    /// 简介
    /// </summary>
    public string brf { get; set; }
    /// <summary>
    /// 详细描述
    /// </summary>
    public string txt { get; set; }
  }
  /// <summary>
  /// 灾害预警
  /// </summary>
  public class Alarms {
    /// <summary>
    /// 预警等级
    /// </summary>
    [JsonProperty(PropertyName = "level")]
    public string level { get; set; }
    /// <summary>
    /// 预警状态
    /// </summary>
    public string stat { get; set; }
    /// <summary>
    /// 预警标题
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 预警详情
    /// </summary>
    public string txt { get; set; }
    /// <summary>
    /// 预警类型
    /// </summary>
    public string type { get; set; }
  }
}
