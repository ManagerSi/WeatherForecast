using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using log4net;
using Magnum.Collections;
using Magnum.Concurrency;
using Mobizone.TSIC.Web.SessionState;
using OfficeOpenXml;
using OfficeOpenXml.Style;

using Ionic.Zip;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Impl;
using System.Threading.Tasks;

namespace WeatherForecast.Utility {
  public class ExcelZippedExporter {
    private static readonly ILog log = LogManager.GetLogger(typeof(ExcelZippedExporter));

    protected class ExportItem {
      public dynamic Export;
      public dynamic Data;
      public string SheetName;
    }

    public ExcelZippedExporter() {
      TemplateFileName = null;
      Exports = new List<ExportItem>();
      SplitSheetInAloneFile = false;
      NewSheetInsertAtStart = false;
      ResetOpenedTabIndex = true;
    }
    protected IList<ExportItem> Exports { get; set; }
    public string TemplateFileName { get; set; }
    /// <summary>
    /// 新的Sheet在最后一个位置插入
    /// </summary>
    public bool NewSheetInsertAtStart { get; set; }
    /// <summary>
    /// 每个Sheet导出于单独文件中
    /// </summary>
    public bool SplitSheetInAloneFile { get; set; }
    /// <summary>
    /// 打开时显示第一个Sheet
    /// </summary>
    public bool ResetOpenedTabIndex { get; set; }

    public void AddExport(string sheetName, dynamic export, dynamic data) {
      Exports.Add(new ExportItem() {
        Export = export,
        Data = data,
        SheetName = sheetName
      });
    }
    public bool EnableProfileLog { get; set; }

    protected byte[] BuildZipFileInternal() {
      if (SplitSheetInAloneFile) {
        return BuildAloneFileZip();
      } else {
        return BuildOneFileZip();
      }
    }
    protected async Task<byte[]> BuildZipFileInternalSync()
    {
        if (SplitSheetInAloneFile)
        {
            return BuildAloneFileZip();
        }
        else
        {
            return await BuildOneFileZipSync();
        }
    }
    public static Atomic<int> counter = Atomic.Create(0);

    public async Task<byte[]> BuildZipFileSync()
    {
        counter.Set(i => i + 1);
        try
        {
            string url = "";
            string empcode = "";
            if (HttpContext.Current != null)
            {
                url = HttpContext.Current.Request.RawUrl;
                var sessoin = SessionFactory.Create<IBLSessionPersisiter>();
                var emp = sessoin.BaseEmployee.EmpCode;

                if (emp != null)
                {
                    empcode = sessoin.BaseEmployee.EmpCode;
                }
            }
            var start = DateTime.Now;
            if (EnableProfileLog)
            {
                log.Info("exporting zipped report, uri={" + url + "}, emp=" + empcode + ", concurrency = " + counter.Value);
            }

            var ret =await BuildZipFileInternalSync();

            if (EnableProfileLog)
            {

                log.Info("exported zipped report cost: " + (DateTime.Now - start).TotalMilliseconds + "ms, uri={" + url +
                         "}, emp=" + empcode);
            }

            return ret;
        }
        finally
        {
            counter.Set(i => i - 1);
        }

    }
    public byte[] BuildZipFile() {
      counter.Set(i => i + 1);
      try {
        string url = "";
        string empcode = "";
        if (HttpContext.Current != null) {
          url = HttpContext.Current.Request.RawUrl;
          var sessoin = SessionFactory.Create<IBLSessionPersisiter>();
          var emp = sessoin.BaseEmployee.EmpCode;

          if (emp != null) {
            empcode = sessoin.BaseEmployee.EmpCode;
          }
        }
        var start = DateTime.Now;
        if (EnableProfileLog) {
          log.Info("exporting zipped report, uri={" + url + "}, emp=" + empcode + ", concurrency = " + counter.Value);
        }

        var ret = BuildZipFileInternal();

        if (EnableProfileLog) {

          log.Info("exported zipped report cost: " + (DateTime.Now - start).TotalMilliseconds + "ms, uri={" + url +
                   "}, emp=" + empcode);
        }

        return ret;
      } finally {
        counter.Set(i => i - 1);
      }

    }
    private async Task<byte[]> BuildOneFileZipSync()
    {
        using (ExcelPackage pck = LoadOrNewPackage())
        {
            var rangeIndex = new Dictionary<string, ExcelRange>();
            foreach (var item in Exports)
            {
                ExcelWorksheet ws;
                if (pck.Workbook.Worksheets.Count(i => i.Name == item.SheetName) > 0)
                {
                    ws = pck.Workbook.Worksheets.First(i => i.Name == item.SheetName);
                }
                else
                {
                    ws = pck.Workbook.Worksheets.Add(item.SheetName);
                    if (NewSheetInsertAtStart)
                    {
                        pck.Workbook.Worksheets.MoveToStart(ws.Index);
                    }
                }
                item.Export.RenderWorkSheet(item.Data, ws);
                var dataRange = ws.Cells["A1:" + ws.Dimension.End.Address.ToString()];
                rangeIndex[item.SheetName] = dataRange;
            }
            // 刷新数据透视表
            foreach (var w in pck.Workbook.Worksheets)
            {
                foreach (var p in w.PivotTables)
                {
                    var name = p.CacheDefinition.SourceRange.Worksheet.Name;
                    if (rangeIndex.ContainsKey(name))
                    {
                        p.CacheDefinition.SourceRange = rangeIndex[name];
                    }
                }
            }
            return await ZipPackageSync(pck);
        }
    }
    private byte[] BuildOneFileZip() {
      using (ExcelPackage pck = LoadOrNewPackage()) {
        var rangeIndex = new Dictionary<string, ExcelRange>();
        foreach (var item in Exports) {
          ExcelWorksheet ws;
          if (pck.Workbook.Worksheets.Count(i => i.Name == item.SheetName) > 0) {
            ws = pck.Workbook.Worksheets.First(i => i.Name == item.SheetName);
          } else {
            ws = pck.Workbook.Worksheets.Add(item.SheetName);
            if (NewSheetInsertAtStart) {
              pck.Workbook.Worksheets.MoveToStart(ws.Index);
            }
          }
          item.Export.RenderWorkSheet(item.Data, ws);
          var dataRange = ws.Cells["A1:" + ws.Dimension.End.Address.ToString()];
          rangeIndex[item.SheetName] = dataRange;
        }
        // 刷新数据透视表
        foreach (var w in pck.Workbook.Worksheets) {
          foreach (var p in w.PivotTables) {
            var name = p.CacheDefinition.SourceRange.Worksheet.Name;
            if (rangeIndex.ContainsKey(name)) {
              p.CacheDefinition.SourceRange = rangeIndex[name];
            }
          }
        }
        return ZipPackage(pck);
      }
    }

    private byte[] BuildAloneFileZip() {
      using (MemoryStream ms = new MemoryStream()) {
        using (ZipFile zip = new ZipFile(System.Text.Encoding.GetEncoding("GB2312"))) {
          foreach (var export in Exports) {

            using (ExcelPackage pck = LoadOrNewPackage(export.Export.TemplateFileName)) {
              var rangeIndex = new Dictionary<string, ExcelRange>();

              ExcelWorksheet ws;
              if (pck.Workbook.Worksheets.Count(i => i.Name == export.SheetName) > 0) {
                ws = pck.Workbook.Worksheets.First(i => i.Name == export.SheetName);
              } else {
                ws = pck.Workbook.Worksheets.Add(export.SheetName);
                if (NewSheetInsertAtStart) {
                  pck.Workbook.Worksheets.MoveToStart(ws.Index);
                }

              }
              export.Export.RenderWorkSheet(export.Data, ws);
              var dataRange = ws.Cells["A1:" + ws.Dimension.End.Address.ToString()];
              rangeIndex[export.SheetName] = dataRange;


              // 刷新数据透视表
              foreach (var w in pck.Workbook.Worksheets) {
                foreach (var p in w.PivotTables) {
                  var name = p.CacheDefinition.SourceRange.Worksheet.Name;
                  if (rangeIndex.ContainsKey(name)) {
                    p.CacheDefinition.SourceRange = rangeIndex[name];
                  }
                }
              }
              if (ResetOpenedTabIndex && pck.Workbook.Worksheets.Count > 0) {
                pck.Workbook.Worksheets.First().View.TabSelected = true;
              }

              zip.AddEntry(export.SheetName.Replace("\\", "_").Replace("/", "_").Replace(" ", "_").Replace("-", "_") + ".xlsx", pck.GetAsByteArray());
            }
          }
          zip.Save(ms);


        }
        return ms.ToArray();
      }
    }
    private async static Task<byte[]> ZipPackageSync(ExcelPackage pck)
    {
        // Zip it
        using (MemoryStream ms = new MemoryStream())
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddEntry(Util.RPCNow.Ticks.ToString() + ".xlsx", await System.Threading.Tasks.Task.Run(() => pck.GetAsByteArray()));
                zip.Save(ms);
            }
            return ms.ToArray();
        }
    }

    private static byte[] ZipPackage(ExcelPackage pck) {
        // Zip it
      using (MemoryStream ms = new MemoryStream()) {
        using (ZipFile zip = new ZipFile()) {
          zip.AddEntry(Util.RPCNow.Ticks.ToString() + ".xlsx", pck.GetAsByteArray());
          zip.Save(ms);
        }
        return ms.ToArray();
      }
    }

    private ExcelPackage LoadOrNewPackage(string subTemplate = null) {
      if (!string.IsNullOrEmpty(subTemplate)) {
        using (var fs = new FileStream(subTemplate, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
          return new ExcelPackage(fs);
        }
      } else if (string.IsNullOrEmpty(TemplateFileName)) {
        return new ExcelPackage();
      } else {
        using (var fs = new FileStream(TemplateFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
          return new ExcelPackage(fs);
        }
      }
    }
    public void LoadTemplate(string filename) {
      TemplateFileName = filename;
      //SkipHeader = true;
    }
    public string ContentType {
      get {
        //return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; 
        return "application/zip";
      }
    }

    public static void SetContentDispositionHeader(HttpResponseBase respose, string fileName = null) {
      // 增加TimeOut
      var context = HttpContext.Current;
      if (null != context) {
        context.Server.ScriptTimeout = 900;
      }

      if (string.IsNullOrEmpty(fileName)) {
        fileName = Util.RPCNow.Ticks.ToString() + ".zip";
      }
      if (!fileName.EndsWith(".zip")) {
        fileName += ".zip";
      }

      var cd = new System.Net.Mime.ContentDisposition {
        FileName = fileName,
        Inline = false,
      };
      respose.AppendHeader("Content-Disposition", cd.ToString());
    }
  }
}
