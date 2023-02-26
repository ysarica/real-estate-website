namespace SRCBasicAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Referans
    {
        [Key]
        public int RID { get; set; }

        [StringLength(250)]
        public string ReferansAdi { get; set; }

        public string ReferansAciklama { get; set; }
    }
}
