using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Globalization;
namespace Mobizone.TSIC.Web {
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  public class EmailAddressAttribute : DataTypeAttribute {
    private readonly Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);

    public EmailAddressAttribute()
      : base(DataType.EmailAddress) {

    }

    public override bool IsValid(object value) {

      string str = Convert.ToString(value, CultureInfo.CurrentCulture);
      if (string.IsNullOrEmpty(str))
        return true;

      Match match = regex.Match(str);
      return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
    }
  }

  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  public class MobilePhoneNumberAttribute : DataTypeAttribute {
    //private readonly Regex regex = new Regex(@"(13[0-9]|15[0|3|6|7|8|9]|18[8|9])\d{8}", RegexOptions.Compiled);
    private readonly Regex regex = new Regex(@"\d{11}", RegexOptions.Compiled);
    public MobilePhoneNumberAttribute()
      : base(DataType.PhoneNumber) {

    }

    public override bool IsValid(object value) {

      string str = Convert.ToString(value, CultureInfo.CurrentCulture);
      if (string.IsNullOrEmpty(str))
        return true;

      Match match = regex.Match(str);

      return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
    }
  }

  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  public class WapMobilePhoneNumberAttribute : DataTypeAttribute {
    private readonly Regex regex = new Regex(@"^1\d{10}$|^[1-9]\d{5,7}$", RegexOptions.Compiled);
    public WapMobilePhoneNumberAttribute()
      : base(DataType.PhoneNumber) {

    }

    public override bool IsValid(object value) {

      string str = Convert.ToString(value, CultureInfo.CurrentCulture);
      if (string.IsNullOrEmpty(str))
        return true;

      Match match = regex.Match(str);

      return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
    }
  }

  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  public class NonNegativeInteger : DataTypeAttribute {
    private readonly Regex regex = new Regex(@"[0-9]+", RegexOptions.Compiled);

    public NonNegativeInteger()
      : base(DataType.Text) {
      ErrorMessage = "{0}必须是正整数";
    }

    public override bool IsValid(object value) {
      string str = Convert.ToString(value, CultureInfo.CurrentCulture);
      if (string.IsNullOrEmpty(str))
        return true;

      Match match = regex.Match(str);

      return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
    }
  }

  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  /// <summary>
  /// 不含有标点字符
  /// </summary>
  public class NonPunctuation : DataTypeAttribute {
    private readonly HashSet<char> punctuations = new HashSet<char>(
      "`~!@#$%^&*()_+-=[];',./?><:\"{}_+|\\，。/；‘’【】、|』『“”：？》《～·！@￥…×（） —"
      .ToCharArray());

    public NonPunctuation()
      : base(DataType.Text) {
      //ErrorMessage = "{1} 必须是正整数";
    }

    public override bool IsValid(object value) {
      try {
        string str = Convert.ToString(value, CultureInfo.CurrentCulture);
        foreach (char c in str) {
          if (punctuations.Contains(c)) {
            return false;
          }
        }
      } catch {
        return false;
      }
      return true;
    }
  }

  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  /// <summary>
  /// 只含有中文，字母以及数字
  /// </summary>
  public class ChineseNumberOrAlpha : DataTypeAttribute {


    public ChineseNumberOrAlpha()
      : base(DataType.Text) {
      //ErrorMessage = "{1} 必须是正整数";
    }

    public override bool IsValid(object value) {
      try {
        string str = Convert.ToString(value, CultureInfo.CurrentCulture);
        foreach (char c in str) {
          // unihan see http://thoughtfly.iteye.com/blog/558050
          if ((c >= 0x4e00 && c <= 0x9fbb) || // CJK Unified Ideographs 1.1 + 4.1
            (c >= 0x3400 && c <= 0x4db5) || //CJK Unified Ideographs Extension A
            (c >= 0xF900 && c <= 0xFA2D) || //CJK Compatibility Ideographs 1.1
            (c >= 0xFA30 && c <= 0xFA6A) || //CJK Compatibility Ideographs 3.2
            (c >= 'a' && c <= 'z') ||
            (c >= 'A' && c <= 'Z') ||
            (c >= '0' && c <= '9')) {
            continue;
          } else {
            return false;
          }
        }
      } catch {
        return false;
      }
      return true;
    }
  }
 
/// <summary>
/// 检查身份证号是否正确
/// </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  public class IDCardNumber : DataTypeAttribute
  {
      public IDCardNumber()
          : base(DataType.Text)
      {
          //ErrorMessage = "{1} 必须是正整数";
      }

      public override bool IsValid(object value)
      {      
          string ID = Convert.ToString(value, CultureInfo.CurrentCulture);
          if (string.IsNullOrEmpty(ID))
              return true;
          if (ID.Length == 18){
              return CheckIDCard18(ID);
          }else if (ID.Length == 15){
              return CheckIDCard15(ID);
          }
          return false;
      }
      /// <summary>
      /// 18位身份证验证
      /// </summary>
      /// <param name="Id">身份证号</param>
      /// <returns></returns>
      private static bool CheckIDCard18(string Id)
      {
          long n = 0;
          if (long.TryParse(Id.Remove(17), out n) == false
            || n < Math.Pow(10, 16)
            || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
          {
              return false;//数字验证
          }
          string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
          if (address.IndexOf(Id.Remove(2)) == -1)
          {
              return false;//省份验证
          }
          string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
          DateTime time = new DateTime();
          if (DateTime.TryParse(birth, out time) == false)
          {
              return false;//生日验证
          }
          string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
          string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
          char[] Ai = Id.Remove(17).ToCharArray();
          int sum = 0;
          for (int i = 0; i < 17; i++)
          {
              sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
          }
          int y = -1;
          Math.DivRem(sum, 11, out y);
          if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
          {
              return false;//校验码验证
          }
          return true;//符合GB11643-1999标准
      }
      /// <summary>
      /// 15位身份证验证
      /// </summary>
      /// <param name="Id">身份证号</param>
      /// <returns></returns>
      private static bool CheckIDCard15(string Id)
      {
          long n = 0;
          if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
          {
              return false;//数字验证
          }
          string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
          if (address.IndexOf(Id.Remove(2)) == -1)
          {
              return false;//省份验证
          }
          string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
          DateTime time = new DateTime();
          if (DateTime.TryParse(birth, out time) == false)
          {
              return false;//生日验证
          }
          return true;//符合15位身份证标准
      }
  }

  // /// <summary>
  // /// 薪资奖金，非负数，最小数点后两位数字
  // /// TODO 2012年7月5日
  // /// </summary>
  //[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
  //public class NonNegativeCharge : DataTypeAttribute
  //{
  //   //private readonly Regex regex = new Regex(@"[0-9]+", RegexOptions.Compiled);

  //    public NonNegativeCharge()
  //        : base(DataType.Text)
  //    {
  //        //ErrorMessage = "{1} 必须是正整数";
  //    }

  //    public override bool IsValid(object value)
  //    {
  //        string str = Convert.ToString(value, CultureInfo.CurrentCulture);
  //        if (string.IsNullOrEmpty(str))
  //            return true;
  //        //Match match = regex.Match(str);

  //        //return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
  //        return true;
  //    }
  //}

}
