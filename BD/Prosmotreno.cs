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
    
    public partial class Prosmotreno
    {
        public int UserId { get; set; }
        public int FilmId { get; set; }
        public System.DateTime TimeDobazlenia { get; set; }
        public int MyReiting { get; set; }
        public string komentarii { get; set; }
    
        public virtual Films Films { get; set; }
        public virtual Users Users { get; set; }
    }
}
