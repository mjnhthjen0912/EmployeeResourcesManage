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
    
    public partial class ChiTietKiLuat
    {
        public string MaCTKL { get; set; }
        public string MaNV { get; set; }
        public string HinhThuc { get; set; }
        public System.DateTime NgayKL { get; set; }
        public string MaKL { get; set; }
        public string NguyenNhan { get; set; }
    
        public virtual KiLuat KiLuat { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
