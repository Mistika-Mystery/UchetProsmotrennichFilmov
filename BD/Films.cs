//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Films
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Films()
        {
            this.Prosmotreno = new HashSet<Prosmotreno>();
            this.Actors = new HashSet<Actors>();
            this.Users1 = new HashSet<Users>();
            this.Users2 = new HashSet<Users>();
            this.Janr = new HashSet<Janr>();
        }
    
        public int IdFilm { get; set; }
        public int IdStrana { get; set; }
        public string NameFilm { get; set; }
        public int TimeFilm { get; set; }
        public int GodFilma { get; set; }
        public int IdRezhiser { get; set; }
        public string ImageFilm { get; set; }
        public System.DateTime TimeDobavlenia { get; set; }
        public string Opisanie { get; set; }
        public int IdTip { get; set; }
        public Nullable<int> IdUser { get; set; }
    
        public virtual Rezhisers Rezhisers { get; set; }
        public virtual Strany Strany { get; set; }
        public virtual Tip Tip { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prosmotreno> Prosmotreno { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Actors> Actors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Janr> Janr { get; set; }
    }
}