//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAO
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietChamCong
    {
        public string MaChamCong { get; set; }
        public string MaNV { get; set; }
        public int NgayCong { get; set; }
    
        public virtual ChamCong ChamCong { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
