namespace SRCBasicAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UrunFoto")]
    public partial class UrunFoto
    {
        [Key]
        public int FotoID { get; set; }

        public int? IlanID { get; set; }

        [StringLength(250)]
        public string Foto { get; set; }
    }
}
