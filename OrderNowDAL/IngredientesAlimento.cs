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
    
    public partial class IngredientesAlimento
    {
        public int IdIngredientesAlimento { get; set; }
        public Nullable<int> Ingrediente { get; set; }
        public Nullable<int> Alimento { get; set; }
        public Nullable<int> Cantidad { get; set; }
    
        public virtual Alimento Alimento1 { get; set; }
        public virtual Ingrediente Ingrediente1 { get; set; }
    }
}
