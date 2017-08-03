using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Mobizone.TSIC.MP;
using Senparc.Weixin.QY.AdvancedAPIs;
using WeatherForecast.Models;

namespace WeatherForecast.Utility
{
	public class ImageEXIF
	{
		// <summary>
		/// 获取图片中的GPS坐标点
		/// </summary>
		/// <param name="p_图片路径">图片路径</param>
		/// <returns>返回坐标【纬度+经度】用"+"分割 取数组中第0和1个位置的值</returns>
		public static String fnGPS(String picURL)
		{
			String s_GPS = "";
			//载入图片 
			Image objImage = Image.FromFile(picURL);
			//取得所有的属性(以PropertyId做排序) 
			var propertyItems = objImage.PropertyItems.OrderBy(x => x.Id);
			//暂定纬度N(北纬) 
			char chrGPSLatitudeRef = 'N';
			//暂定经度为E(东经) 
			char chrGPSLongitudeRef = 'E';
			foreach (PropertyItem objItem in propertyItems)
			{
				//只取Id范围为0x0000到0x001e
				if (objItem.Id >= 0x0000 && objItem.Id <= 0x001e)
				{
					objItem.Id = 0x0002;
					switch (objItem.Id)
					{
						case 0x0000:
							var query = from tmpb in objItem.Value select tmpb.ToString();
							string sreVersion = string.Join(".", query.ToArray());
							break;
						case 0x0001:
							chrGPSLatitudeRef = BitConverter.ToChar(objItem.Value, 0);
							break;
						case 0x0002:
							if (objItem.Value.Length == 24)
							{
								//degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint) 
								double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
								//minutes(将byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint) 
								double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
								//seconds(将byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint) 
								double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
								//计算经纬度数值, 如果是南纬, 要乘上(-1) 
								double dblGPSLatitude = (((s / 60 + m) / 60) + d) * (chrGPSLatitudeRef.Equals('N') ? 1 : -1);
								string strLatitude = string.Format("{0:#} deg {1:#}' {2:#.00}\" {3}", d
								, m, s, chrGPSLatitudeRef);
								//纬度+经度
								s_GPS += dblGPSLatitude + "+";
							}
							break;
						case 0x0003:
							//透过BitConverter, 将Value转成Char('E' / 'W') 
							//此值在后续的Longitude计算上会用到 
							chrGPSLongitudeRef = BitConverter.ToChar(objItem.Value, 0);
							break;
						case 0x0004:
							if (objItem.Value.Length == 24)
							{
								//degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint) 
								double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
								//minutes(将byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint) 
								double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
								//seconds(将byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint) 
								double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
								//计算精度的数值, 如果是西经, 要乘上(-1) 
								double dblGPSLongitude = (((s / 60 + m) / 60) + d) * (chrGPSLongitudeRef.Equals('E') ? 1 : -1);
							}
							break;
						case 0x0005:
							string strAltitude = BitConverter.ToBoolean(objItem.Value, 0) ? "0" : "1";
							break;
						case 0x0006:
							if (objItem.Value.Length == 8)
							{
								//将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint 
								double dblAltitude = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
							}
							break;
					}
				}
			}
			return s_GPS;
		}

		public static double GetGPSLatitude(string GPSLatitude)
		{
			string[] GPSLatitudes = GPSLatitude.Split(',');
			if (GPSLatitudes.Length == 3)
			{
				double d = double.Parse(GPSLatitudes[0]);
				double m = double.Parse(GPSLatitudes[1]);
				double s = double.Parse(GPSLatitudes[2]);

				//暂定纬度N(北纬) 
				char chrGPSLatitudeRef = 'N';

				return (((s / 60 + m) / 60) + d) * (chrGPSLatitudeRef.Equals('N') ? 1 : -1);
			}

			return 0;
		}

		public static Dictionary<string,string> UploadFile(string hpf, UploadImageInfo model)
		{
			string filePath = System.Configuration.ConfigurationManager.AppSettings[BaseDictType.TempFileUploadPath];

		    Dictionary<string, string> data = new Dictionary<string, string>();

			string saveUrl = System.Web.HttpContext.Current.Server.MapPath(filePath);
			if (!System.IO.Directory.Exists(saveUrl))
			{
				System.IO.Directory.CreateDirectory(saveUrl);
			}

			//将文件流写到byte数组中
			byte[] arr = Convert.FromBase64String(hpf);
			MemoryStream ms = new MemoryStream(arr);
			Bitmap bmp = new Bitmap(ms);

			var filename = Util.RPCNow.Ticks.ToString() + Util.RPCNow.Millisecond + ".jpg";
			try
			{
				var saveurl = saveUrl + filename;
				bmp.Save(saveurl, System.Drawing.Imaging.ImageFormat.Jpeg);
				//bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
				//bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
				//bmp.Save(txtFileName + ".png", ImageFormat.Png);
				bmp.Dispose();
				ms.Close();

				string strDescription = model.DateTimeOriginal + "\r\n";
				strDescription += model.StoreAddr + "\r\n"; // + " 精度(" + model.SignPoint + ")";
				strDescription += model.StoreCode + " " + model.StoreName + "\r\n";
				strDescription += model.EmpCode + " " + model.EmpName + "(" + model.EmpDuty + ")\r\n";
				//strDescription += "纬度:" + GetGPSLatitude(GPSLatitude) + "\r\n";
				//strDescription += "经度:" + GetGPSLatitude(GPSLongitude) + "\r\n";
				//strDescription += GetDistance(GPSLatitude, GPSLongitude, storeGPSLat, storeGPSLng) ;
				//strDescription += "拍摄地址:" + GetAddrName(GetGPSLatitude(model.GPSLatitude) + "," + GetGPSLatitude(model.GPSLongitude));

				ms = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(saveurl));
				System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
				Graphics g = Graphics.FromImage(image);
				//Brush drawBrush = new SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(243)), ((System.Byte)(255)))); //自定义字体颜色  
				Font font = new Font("宋体", 10);
				SizeF sizeF = g.MeasureString(" " + strDescription + " ", font);
				Color Mycolor = System.Drawing.Color.FromArgb(0, 0, 0, 0);//说明：1-（128/255）=1-0.5=0.5 透明度为0.5，即50%
				SolidBrush sb1 = new System.Drawing.SolidBrush(Mycolor);
				g.FillRectangle(sb1, new RectangleF(new PointF(0, image.Height-100), sizeF));
				//FillRoundRectangle(g, sb1, new Rectangle(15, 20, (int)sizeF.Width+20, (int)sizeF.Height), 8);
				//DrawRoundRectangle(g, Pens.Transparent, new Rectangle(15, 20, (int)sizeF.Width +20, (int)sizeF.Height), 8);
				g.DrawString(strDescription, new Font("宋体", 10), Brushes.White, new PointF(2, image.Height - 94));

				System.IO.File.Delete(saveurl);
				image.Save(saveurl);

				var url = filePath + filename;

				data.Add("status", "1");
				data.Add("content", "上传成功");
				data.Add("url", url);
				data.Add("filename", filename);

			}
			catch (Exception ex)
			{
				data.Add("status", "0");
				data.Add("content", "上传失败");
				data.Add("url", "");
				data.Add("filename", "");
			}
			finally
			{
				bmp.Dispose();
				ms.Close();
			}

			//string returnstring=PostApply(hpf, DateTimeOriginal, GPSLatitude, GPSLongitude);
			

			return data;
		}

		public static Dictionary<string, string> WXUploadFile(string ServerID, UploadImageInfo model)
		{
			//string gps= map_tx2bd(double.Parse(model.GPSLatitude), double.Parse(model.GPSLongitude));

			string filePath = System.Configuration.ConfigurationManager.AppSettings[BaseDictType.TempFileUploadPath];
			var filename = Util.RPCNow.Ticks.ToString() + Util.RPCNow.Millisecond + ".jpg";
			var saveURl = System.Web.HttpContext.Current.Server.MapPath(filePath) + filename;
			var accessToken = WeixinCache.GetCorpAccessToken();
			using (MemoryStream stream = new MemoryStream())
			{
				MediaApi.Get(accessToken, ServerID, stream);

				if (!System.IO.Directory.Exists(saveURl))
				{
					System.IO.Directory.CreateDirectory(saveURl.Replace(filename, ""));
				}
				using (var fs = new FileStream(saveURl, FileMode.CreateNew))
				{
					stream.Seek(0, SeekOrigin.Begin);
					stream.CopyTo(fs);
					fs.Flush();
				}
			}

			Dictionary<string, string> data = new Dictionary<string, string>();

			MemoryStream ms = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(saveURl));
			try
			{
				string strDescription = model.DateTimeOriginal + "\r\n";
				strDescription += model.StoreAddr + "\r\n"; // + " 精度(" + model.SignPoint + ")" ;
				strDescription += model.StoreCode + " " + model.StoreName + "\r\n";
				strDescription += model.EmpCode + " " + model.EmpName + "(" + model.EmpDuty + ")\r\n";

				System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
				Graphics g = Graphics.FromImage(image);
				//Brush drawBrush = new SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(243)), ((System.Byte)(255)))); //自定义字体颜色  
				Font font = new Font("宋体", 10);
				SizeF sizeF = g.MeasureString(" " + strDescription + " ", font);
				Color Mycolor = System.Drawing.Color.FromArgb(0, 0, 0, 0);//说明：1-（128/255）=1-0.5=0.5 透明度为0.5，即50%
				SolidBrush sb1 = new System.Drawing.SolidBrush(Mycolor);
				g.FillRectangle(sb1, new RectangleF(new PointF(0, image.Height - 100), sizeF));
				//FillRoundRectangle(g, sb1, new Rectangle(15, 20, (int)sizeF.Width+20, (int)sizeF.Height), 8);
				//DrawRoundRectangle(g, Pens.Transparent, new Rectangle(15, 20, (int)sizeF.Width +20, (int)sizeF.Height), 8);
				g.DrawString(strDescription, new Font("宋体", 10), Brushes.White, new PointF(2, image.Height - 94));

				System.IO.File.Delete(saveURl);
				image.Save(saveURl);

				var url = filePath + filename;

				data.Add("status", "1");
				data.Add("content", "上传成功");
				data.Add("url", url);
				data.Add("filename", filename);

			}
			catch (Exception ex)
			{
				data.Add("status", "0");
				data.Add("content", "上传失败");
				data.Add("url", "");
				data.Add("filename", "");
			}
			finally
			{
				ms.Close();
			}

			//string returnstring=PostApply(hpf, DateTimeOriginal, GPSLatitude, GPSLongitude);


			return data;
		}

		public static string GetDistance(string GPSLatitude, string GPSLongitude, decimal storeGPSLat, decimal storeGPSLng)
		{
			if(string.IsNullOrEmpty(GPSLatitude) || string.IsNullOrEmpty(GPSLongitude))
			{
				return "照片中没有坐标信息";
			}
			if (storeGPSLat==-1 || storeGPSLng==-1)
			{
				return "门店没有坐标信息";
			}
			double gPSLatitude = 0d;
			double gPSLongitude = 0d;
			try
			{
				gPSLatitude = GetGPSLatitude(GPSLatitude);
			}
			catch(Exception ex)
            {
				return "照片坐标信息不正确";
			}
			try
			{
				gPSLongitude = GetGPSLatitude(GPSLongitude);
			}
			catch (Exception ex)
			{
				return "照片坐标信息不正确";
			}

			double radLat1 = rad(gPSLatitude);


			double radLat2 = rad(double.Parse(storeGPSLat.ToString()));


			double a = radLat1 - radLat2;


			double b = rad(gPSLongitude) - rad(double.Parse(storeGPSLng.ToString()));


			double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +


		    Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));


			s = s * EARTH_RADIUS;


			s = Math.Round(s * 10000) / 10000 * 1000;

			return "距离门店:" + s.ToString() + "米";
		}

		private const double EARTH_RADIUS = 6378.137;//地球半径 

		private static double rad(double d)
		{
			return d * Math.PI / 180.0;
		}

		public static string GetAddrName(string gps)
		{
			if (string.IsNullOrEmpty(gps))
			{
				return "无法获取坐标";
			}
			else
			{
				string[] gpss = gps.Split(',');
				if (gpss.Length != 2)
				{
					return "坐标错误";
				}
				else
				{
					try
					{
						double lat = double.Parse(gpss[0]);
						double lng = double.Parse(gpss[1]);
					}
					catch (Exception ex)
					{
						return "坐标错误";
					}
				}
			}

			string resule = "";
			string ak = "T3f6VizEwjqaQl2dtPF89q5h";
			string url = "http://api.map.baidu.com/geocoder/v2/?ak={0}&callback=renderReverse&location={1}&output=json&pois=1";
			string postuel = string.Format(url, ak, gps);

			resule = GetApply(postuel).Replace("[renderReverse&&renderReverse(", "");
			resule = resule.Substring(0, resule.Length - 2);
			try
			{
				JObject jo = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(resule);
				return jo["result"]["formatted_address"].ToString();
			}
			catch (Exception ex)
			{
				return gps + "无法获取";
			}
		}

		public static string GetApply(string url)
		{
			try
			{
				WebRequest wrq;
				HttpWebResponse wrp;

				wrq = HttpWebRequest.Create(url);
				wrp = (HttpWebResponse)wrq.GetResponse();
				Stream resStream = wrp.GetResponseStream();
				StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
				string tempstr = "[" + sr.ReadToEnd() + "]";

				//WriteMessage("\r\n" + tempstr);

				return tempstr;

			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius)
		{
			using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
			{
				g.DrawPath(pen, path);
			}
		}
		public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
		{
			using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
			{
				g.FillPath(brush, path);
			}
		}
		internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
		{
			GraphicsPath roundedRect = new GraphicsPath();
			roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
			roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
			roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
			roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
			roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
			roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
			roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
			roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
			roundedRect.CloseFigure();
			return roundedRect;
		}

        /* 坐标转换，腾讯地图转换成百度地图坐标
		 * @param lat 腾讯纬度 
		 * @param lon 腾讯经度
		 * @return 返回结果：经度,纬度
		 */  
		public static String map_tx2bd(double lat, double lon)
		{
			double bd_lat;
			double bd_lon;
			double x_pi = 3.14159265358979324;
			double x = lon, y = lat;
			double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
			double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
			bd_lon = z * Math.Cos(theta) + 0.0065;
			bd_lat = z * Math.Sin(theta) + 0.006;
			return bd_lat  + "," + bd_lon;
		}


		/** 
		 * 坐标转换，百度地图坐标转换成腾讯地图坐标 
		 * @param lat  百度坐标纬度 
		 * @param lon  百度坐标经度 
		 * @return 返回结果：纬度,经度 
		 */
		public static String map_bd2tx(double lat, double lon)
		{
			double tx_lat;
			double tx_lon;
			double x_pi = 3.14159265358979324;
			double x = lon - 0.0065, y = lat - 0.006;
			double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
			double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
			tx_lon = z * Math.Cos(theta);
			tx_lat = z * Math.Sin(theta);
			return tx_lat + "," + tx_lon;
		}

		//传到云服务器
		public static string PostApply(string filename,string hpf, string DateTimeOriginal, string GPSLatitude, string GPSLongitude)
		{
			string url = "http://localhost:8067/handle/LRIMG.ashx";
			string postContent = "base64_string=" + hpf + "&" +
					"DateTimeOriginal=" + DateTimeOriginal + "&" +
					"GPSLatitude=" + GPSLatitude + "&" +
					"GPSLongitude=" + GPSLongitude + "&" +
					"FileName" + filename;

			try
			{
				WebRequest wrq;
				HttpWebResponse wrp;

				wrq = HttpWebRequest.Create(url);
				//Post请求方式
				wrq.Method = "POST";
				// 内容类型
				wrq.ContentType = "application/x-www-form-urlencoded";
				// 参数经过URL编码
				string paraUrlCoded = postContent;
				byte[] payload;
				payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded); 
				wrq.ContentLength = payload.Length;
				//获得请 求流
				System.IO.Stream writer = wrq.GetRequestStream();
				//将请求参数写入流
				writer.Write(payload, 0, payload.Length);
				// 关闭请求流
				writer.Close();

				// 获得响应流
				wrp = (System.Net.HttpWebResponse)wrq.GetResponse();
				System.IO.StreamReader myreader = new System.IO.StreamReader(wrp.GetResponseStream(), Encoding.UTF8);
				string tempstr = "[" + myreader.ReadToEnd() + "]";
				myreader.Close();

				//WriteMessage("\r\n" + tempstr);

				return tempstr;

			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
