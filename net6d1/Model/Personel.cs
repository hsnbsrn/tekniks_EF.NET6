﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using net6d1.Models;
using System;
using System.Collections.Generic;

namespace net6d1.Model
{
    public partial class Personel
    {
        public Personel()
        {
            Mcihazs = new HashSet<Mcihaz>();
        }
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public bool? Aktifmi { get; set; }
        public virtual ICollection<Mcihaz> Mcihazs { get; set; }
    }
}