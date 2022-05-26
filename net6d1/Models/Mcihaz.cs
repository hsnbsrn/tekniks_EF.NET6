using System;
using System.Collections.Generic;

namespace net6d1.Models
{
    public partial class Mcihaz
    {
        public int Id { get; set; }
        public string? AdSoyad { get; set; }
        public int? Islem { get; set; }
        public string? DetayIslem { get; set; }
        public string? Getirilen { get; set; }
        public string? Mail { get; set; }
        public string? TelNo { get; set; }
        public string? Kod { get; set; }
        public DateTime? Vtarih { get; set; }
        public DateTime? Btarih { get; set; }
        public int? Durum { get; set; }
        public string? Cihaz { get; set; }
        public int? Ucret { get; set; }

        public virtual Durum? DurumNavigation { get; set; }
        public virtual Islem? IslemNavigation { get; set; }
    }
}
