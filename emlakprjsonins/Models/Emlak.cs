//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace emlakprjsonins.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Emlak
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Emlak()
        {
            this.Foto = new HashSet<Foto>();
        }
    
        public int emlakId { get; set; }
        public string emlakAdi { get; set; }
        public string emlakFiyat { get; set; }
        public string emlakAciklama { get; set; }
        public int emlakKatId { get; set; }
        public int emlakUyeId { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        public virtual Uye Uye { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Foto> Foto { get; set; }
    }
}