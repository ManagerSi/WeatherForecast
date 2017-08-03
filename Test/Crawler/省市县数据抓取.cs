using HtmlAgilityPack;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.Crawler {
  public class 省市县数据抓取 {
    private ILog log = log4net.LogManager.GetLogger(typeof(省市县数据抓取));
    public const string UrlStr = "http://www.stats.gov.cn/tjsj/tjbz/xzqhdm/201703/t20170310_1471429.html";
    public List<City> SaveList = new List<City>();
    public 省市县数据抓取() {
      try {
        log.Info("抓取数据");
        string HtmlStr = HttpHelper.DownloadHtml(UrlStr,Encoding.UTF8);
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(HtmlStr);
        //string goodsListPath = "//*[@id='J_goodsList']";
        //HtmlNode goodsListNode = doc.DocumentNode.SelectSingleNode(goodsListPath);
        string liPath = "//p[@class='MsoNormal']";
        HtmlNodeCollection goodsNodeCollection = doc.DocumentNode.SelectNodes(liPath);

        City c = new City() { 
          ID=1,
          Name = "全国",
          Code = "100000",
          Contry = "China",
          Org_Level = "1"
        };
        SaveList.Add(c);
        foreach(var item in goodsNodeCollection) {
          var firstNode = item.FirstChild;
          if(firstNode.Name == "b")
            GetProvince(item);
          else if(firstNode.InnerText == "　") {
            GetCity(item);
          } else if(firstNode.InnerText == "　　") {
            GetCounty(item);
          }
        }

      } catch(Exception e) {
        log.Info("last child code:" + SaveList.Last().Code);
        log.Info(e);
        throw (e);
      }
    }

    private void GetCounty(HtmlNode item) {
      City c = new City();
      c.Code = item.ChildNodes[1].InnerText.Replace("&nbsp;","").Trim();
      c.Name = item.ChildNodes[2].InnerText.Trim();
      c.Org_Level = "4";
      c.ID = SaveList.Last().ID + 1;
      var pc = SaveList.Last(i => i.Org_Level == "3");
      c.ParentCode = pc.Code;
      c.ParentID = pc.ID;
      c.Contry = "China";
      //if(c.Name == "市辖区")
      //  return;
      SaveList.Add(c);
    }

    private void GetCity(HtmlNode item) {
      City c = new City();
      c.Code = item.ChildNodes[1].InnerText.Replace("&nbsp;","").Trim();
      c.Name = item.ChildNodes[2].InnerText.Trim();     
      c.Org_Level = "3";
      c.ID = SaveList.Last().ID + 1;
      var pc = SaveList.Last(i => i.Org_Level == "2");
      c.ParentCode = pc.Code;
      c.ParentID = pc.ID;
      c.Contry = "China";
      SaveList.Add(c);

    }

    private void GetProvince(HtmlNode item) {
      City c = new City();
      c.Code = item.ChildNodes[0].FirstChild.InnerText.Replace("&nbsp;","").Trim();
      c.Name = item.ChildNodes[1].FirstChild.InnerText.Trim();
      c.Org_Level = "2";
      c.ID = SaveList.Last().ID + 1;
      var pc = SaveList.Last(i => i.Org_Level == "1");
      c.ParentCode = pc.Code;
      c.ParentID = pc.ID;
      c.Contry = "China";
      SaveList.Add(c);
    }

    public void Save() {
      log.Info("报存数据");
      SQLHelper.ExecuteNonQueryForCity(SaveList);
    }
  }
  public class City {
    public decimal ID { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Org_Level { get; set; }
    public string ParentCode { get; set; }
    public decimal ParentID { get; set; }
    public string Contry { get; set; }
    public string Loc_x { get; set; }
    public string Loc_y { get; set; }
  }
}
