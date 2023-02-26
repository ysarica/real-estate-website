namespace SRCBasicAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UrunKategori")]
    public partial class UrunKategori
    {
        [Key]
        public int KID { get; set; }

        [StringLength(150)]
        public string KategoriAdi { get; set; }
    }
}
