namespace SRCBasicAdmin.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SRCBasicDB : DbContext
    {
        public SRCBasicDB()
            : base("name=SRCBasicDB")
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<MailAyar> MailAyar { get; set; }
        public virtual DbSet<Mesaj> Mesaj { get; set; }
        public virtual DbSet<Referans> Referans { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<UrunFoto> UrunFoto { get; set; }
        public virtual DbSet<UrunKategori> UrunKategori { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
