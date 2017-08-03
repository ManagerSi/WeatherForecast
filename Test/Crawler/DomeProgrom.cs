using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using HtmlAgilityPack;



namespace TaskTest.Crawler {
  /// <summary>
  /// 1 爬虫，爬虫攻防
  /// 2 下载html
  /// 3 xpath解析html，获取数据和深度抓取
  /// 4 ajax数据获取
  /// 5 多线程爬虫
  /// </summary>
  public class DomeProgrom {
    private ILog log = log4net.LogManager.GetLogger(typeof(DomeProgrom));
    public void domeMain() {
      try {
        log.Info("下载html");
        //所有商品类别
        //string allSort = HttpHelper.DownloadHtml("https://www.jd.com/allSort.aspx",Encoding.UTF8);

        string HQurl = "https://search.jd.com/Search?keyword=%E5%A9%9A%E5%BA%86%E5%BA%8A%E5%93%81&enc=utf-8&qrst=1&rt=1&stop=1&vt=2&offset=1&suggest=3.def.0.T02&wq=%E5%A9%9A%E5%BA%86%E5%BA%8A&page=3&s=60&click=0";
        var html = HttpHelper.DownloadHtml(HQurl,Encoding.UTF8);
        //使用HtmlAgilityPack解析html
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);
        string PageCountPath = "//*[@id='J_topPage']/span/i";
        HtmlNode pageCountNode = doc.DocumentNode.SelectSingleNode(PageCountPath);
        List<Product> productList = new List<Product>();

        long count = 0;
        if(pageCountNode != null) {
          string sNumber = pageCountNode.InnerText;
          for(int i = 1;i <= int.Parse(sNumber);i++) {
            string pageUrl = HQurl.Replace("&page=3&",string.Format("&page={0}&",i*2-1));
            var list = GetProductList(pageUrl);
            if(list != null && list.Count() > 0) {
              productList.AddRange(list);
            }
            //save to db
            var count1 = SQLHelper.ExecuteNonQueryForProduct(list);
            Console.WriteLine("本次插入数据:" + count1);
            count += count1;
            Console.WriteLine("共插入数据:" + count);
          }
        }

        //save to db
        //count = SQLHelper.ExecuteNonQuery(productList);
        //Console.WriteLine("共插入数据:"+count);
      } catch(Exception e){
        log.Error(e);
      }
    }

    private List<Product> GetProductList(string pageUrl) {
      List<Product> products = new List<Product>();

      //下载html
      var html = HttpHelper.DownloadHtml(pageUrl,Encoding.UTF8);
       HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
       doc.LoadHtml(html);
       //string goodsListPath = "//*[@id='J_goodsList']";
       //HtmlNode goodsListNode = doc.DocumentNode.SelectSingleNode(goodsListPath);
       string liPath = "//*[@id='J_goodsList']/ul/li";
       HtmlNodeCollection goodsNodeCollection = doc.DocumentNode.SelectNodes(liPath);
       if(goodsNodeCollection == null || goodsNodeCollection.Count == 0) {
         log.Info("没有产品");
       } else {
        
         foreach(var item in goodsNodeCollection) {
           Product p = new Product();
           try {
             HtmlDocument docChild = new HtmlDocument();
             docChild.LoadHtml(item.OuterHtml);

             string pidPath = "/li";
             HtmlNode pidNode = docChild.DocumentNode.SelectSingleNode(pidPath);
             if(pidNode == null) {
               continue;
             }
             string pid = pidNode.Attributes["data-pid"].Value;
             string sku = pidNode.Attributes["data-sku"].Value;
             p.pid = long.Parse(pid);
             p.sku = long.Parse(sku);

             string titlePath = "//*[@class='p-name p-name-type-2']/a";
             HtmlNode titleNode = docChild.DocumentNode.SelectSingleNode(titlePath);
             if(titleNode == null) {
               continue;
             }
             p.title = titleNode.Attributes["title"].Value;

             string urlPath = "//*[@class='p-img']/a";
             HtmlNode urlNode = docChild.DocumentNode.SelectSingleNode(urlPath);
             if(urlNode == null) {
               continue;
             }
             string url = urlNode.Attributes["href"].Value;
             p.url = url;

             HtmlNode imgNode = urlNode.Element("img");
             if(imgNode == null) { continue; }
             if(imgNode.Attributes.Contains("src")) {
               p.img = imgNode.Attributes["src"].Value;
             }else if(imgNode.Attributes.Contains("original")) {
               p.img = imgNode.Attributes["original"].Value;
             } else if(imgNode.Attributes.Contains("data-lazy-img")) {
               p.img = imgNode.Attributes["data-lazy-img"].Value;
             } else { continue; }
             if(!p.img.StartsWith("http")) {
               p.img = "http:" + p.img;
             }
             
             string pricePath = "//*[@class='J_" + p.sku + "']";
             HtmlNode priceNode = docChild.DocumentNode.SelectSingleNode(pricePath);
             if(priceNode == null) {
               continue;
             }
             p.price = priceNode.Attributes["data-price"].Value;
           
           } catch(Exception e) {
             log.Info(e);
             continue;
           } 
           
           products.Add(p);
         }
       }

       return products;
    }

  }
}
