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
    
    public partial class AlimentoPedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlimentoPedido()
        {
            this.ExtraPedido = new HashSet<ExtraPedido>();
        }
    
        public int IdAlimentoPedido { get; set; }
        public Nullable<int> IdPedido { get; set; }
        public Nullable<int> IdAlimento { get; set; }
    
        public virtual Alimento Alimento { get; set; }
        public virtual Pedido Pedido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExtraPedido> ExtraPedido { get; set; }
    }
}