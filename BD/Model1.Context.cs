﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UchetProsmotrennichFilmov.BD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UchetFilmofEntities : DbContext
    {
        private static UchetFilmofEntities _context;
        public UchetFilmofEntities()
            : base("name=UchetFilmofEntities")
        {
        }
        private static UchetFilmofEntities GetContext()
        {
            if (_context == null)
                _context = new UchetFilmofEntities();

            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actors> Actors { get; set; }
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<Janr> Janr { get; set; }
        public virtual DbSet<Prosmotreno> Prosmotreno { get; set; }
        public virtual DbSet<Rezhisers> Rezhisers { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Strany> Strany { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tip> Tip { get; set; }
        public virtual DbSet<Users> Users { get; set; }

    }
}
