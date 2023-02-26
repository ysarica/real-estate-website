namespace SRCBasicAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mesaj")]
    public partial class Mesaj
    {
        [Key]
        public int MID { get; set; }

        [StringLength(150)]
        public string Ad { get; set; }

        [StringLength(150)]
        public string Mail { get; set; }

        [StringLength(150)]
        public string Konu { get; set; }

        [Column("Mesaj")]
        public string Mesaj1 { get; set; }

        [StringLength(50)]
        public string Okundu { get; set; }

        [StringLength(50)]
        public string Tarih { get; set; }
    }
}
