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
    
    public partial class OfertaPedido
    {
        public int IdOfertaPedido { get; set; }
        public Nullable<int> IdOferta { get; set; }
        public Nullable<int> IdPedido { get; set; }
    
        public virtual Oferta Oferta { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
