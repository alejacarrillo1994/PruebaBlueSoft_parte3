//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PruebaBlueSoft_parte3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClienteReporte
    {
        public int idReporte { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> NumeroTransacciones { get; set; }
        public Nullable<decimal> MontoTotal { get; set; }
    }
}