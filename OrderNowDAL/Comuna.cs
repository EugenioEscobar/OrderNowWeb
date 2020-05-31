//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrderNowDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comuna
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comuna()
        {
            this.Cliente = new HashSet<Cliente>();
            this.Distribuidor = new HashSet<Distribuidor>();
            this.Trabajador = new HashSet<Trabajador>();
        }
    
        public int IdComuna { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IdCiudad { get; set; }
    
        public virtual Ciudad Ciudad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente> Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Distribuidor> Distribuidor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trabajador> Trabajador { get; set; }
    }
}