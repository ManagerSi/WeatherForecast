//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherLib.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class BaseEmployee
    {
        public BaseEmployee()
        {
            this.BASE_USER = new HashSet<BaseUser>();
        }
    
        public decimal ID { get; set; }
        public Nullable<decimal> EmpType { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public Nullable<int> Sex { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<decimal> UpdateID { get; set; }
    
        public virtual ICollection<BaseUser> BASE_USER { get; set; }
    }
}
