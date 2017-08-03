using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WeatherLib.Model;
using WeatherLib.BLL;
using WeatherLib.Utility;

namespace WeatherLib.Web.SessionState {
  public interface IClientSessionPersisiter : ISessionPersisiter {
    WeatherClientType ClientType { get; set; }
    string ClientVersion { get; set; }
    string ClientImgPrefix { get; set; }
    int LastMagicNum { get; set; }
    string TokenID { get; set; }

    decimal? ProdType { get; set; } //负责产品
  }
  public class ClientSessionPersisiter : SessionPersisiter, IClientSessionPersisiter {
    protected const string sessionClientTypeKey = "_s_clienttype";
    protected const string sessionClientVersionKey = "_s_clientversion";
    protected const string sessionClientImgPrefixKey = "_s_clientimgprefix";
    protected const string sessionClientLastMagicNum = "_s_clientmagnum";
    protected const string sessionClientTokenID = "_s_clienttokenID";
    protected const string sessionClientProdType = "_s_clientprodtype";
    public WeatherClientType ClientType {
      get {
        return GetValue(sessionClientTypeKey,WeatherClientType.Unknown);
      }
      set {
        SetValue(sessionClientTypeKey,value);
      }
    }

    //2014-12-04 负责产品
    public decimal? ProdType
    {
      get
      {
        return GetValue<decimal>(sessionClientProdType, -1);
      }
      set
      {
        SetValue(sessionClientProdType, value);
      }
    }

    public string ClientVersion {
      get { return GetValue(sessionClientVersionKey, ""); }
      set {
        SetValue(sessionClientVersionKey, value);
      }
    }

    public string ClientImgPrefix {
      get { return GetValue(sessionClientImgPrefixKey, ""); }
      set {
        SetValue(sessionClientImgPrefixKey, value);
      }
    }

    public int LastMagicNum {
      get { return GetValue(sessionClientLastMagicNum, -1); }
      set {
        SetValue(sessionClientLastMagicNum, value);
      }
    }

    public string TokenID {
      get { return GetValue(sessionClientTokenID,""); }
      set { SetValue(sessionClientTokenID, value); }
    }

    public override bool RemoveSession() {
      if (HttpContext.Current == null) {
        throw new InvalidOperationException();
      }
      var session = HttpContext.Current.Session;

      session.Remove(sessionClientImgPrefixKey);
      session.Remove(sessionClientTypeKey);
      session.Remove(sessionClientVersionKey);
      session.Remove(sessionClientLastMagicNum);
      session.Remove(sessionClientTokenID);
      session.Remove(sessionClientProdType);
      return true;
    }
  }
}
