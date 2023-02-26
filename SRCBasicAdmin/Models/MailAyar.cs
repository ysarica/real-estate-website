namespace SRCBasicAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MailAyar")]
    public partial class MailAyar
    {
        [Key]
        public int MID { get; set; }

        [StringLength(150)]
        public string GondericiMail { get; set; }

        [StringLength(50)]
        public string Host { get; set; }

        [StringLength(50)]
        public string Port { get; set; }

        [StringLength(150)]
        public string Sifre { get; set; }
    }
}
