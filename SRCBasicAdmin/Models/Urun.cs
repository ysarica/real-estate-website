namespace SRCBasicAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urun")]
    public partial class Urun
    {
        [Key]
        public int IlanID { get; set; }

        [StringLength(50)]
        public string Fiyat { get; set; }

        [StringLength(50)]
        public string EmlakTip { get; set; }

        [StringLength(50)]
        public string mKareBrut { get; set; }

        [StringLength(50)]
        public string mKareNet { get; set; }

        [StringLength(50)]
        public string OdaSayi { get; set; }

        [StringLength(50)]
        public string BinaYas { get; set; }

        [StringLength(50)]
        public string BulunduKat { get; set; }

        [StringLength(50)]
        public string KatSayi { get; set; }

        [StringLength(50)]
        public string Isitma { get; set; }

        [StringLength(50)]
        public string BanyoSayi { get; set; }

        [StringLength(50)]
        public string Balkon { get; set; }

        [StringLength(50)]
        public string KullanımDurum { get; set; }

        [StringLength(50)]
        public string SiteIci { get; set; }

        [StringLength(50)]
        public string Aidat { get; set; }

        [StringLength(50)]
        public string KrediDurum { get; set; }

        [StringLength(50)]
        public string TapuDurum { get; set; }

        [StringLength(50)]
        public string Takas { get; set; }

        [StringLength(250)]
        public string IlanBaslik { get; set; }

        public string Adres { get; set; }

        public string IlanAciklama { get; set; }

        public string IcOzellik { get; set; }

        public string DisOzellik { get; set; }

        public string UlasimMuhit { get; set; }

        [StringLength(250)]
        public string KapakFoto { get; set; }
        [StringLength(50)]
        public string IlanTip { get; set; }
        [StringLength(50)]
        public string Engelli { get; set; }
        public string UrunVideo { get; set; }

    }
}
