using System;
using System.Collections.Generic;

namespace net6d1.Models
{
    public partial class Durum
    {
        public Durum()
        {
            Mcihazs = new HashSet<Mcihaz>();
        }

        public int Id { get; set; }
        public string? Ad { get; set; }

        public virtual ICollection<Mcihaz> Mcihazs { get; set; }
    }
}
