using System;
using System.Collections.Generic;

namespace net6d1.Models
{
    public partial class Islem
    {
        public Islem()
        {
            Mcihazs = new HashSet<Mcihaz>();
        }

        public int Id { get; set; }
        public string? Ad { get; set; }
        public int? Ucret { get; set; }

        public virtual ICollection<Mcihaz> Mcihazs { get; set; }
    }
}
