namespace SRCBasicAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int AID { get; set; }

        [StringLength(150)]
        public string KulAdi { get; set; }

        [StringLength(150)]
        public string Sifre { get; set; }

        [StringLength(150)]
        public string AdSoyad { get; set; }

        [StringLength(150)]
        public string Mail { get; set; }
    }
}
