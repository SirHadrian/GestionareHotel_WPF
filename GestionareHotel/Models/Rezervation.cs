//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionareHotel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rezervation
    {
        public int ID { get; set; }
        public bool Active { get; set; }
        public bool Paid { get; set; }
        public int ID_User { get; set; }
        public Nullable<int> ID_Room { get; set; }
        public Nullable<int> ID_Offer { get; set; }
        public Nullable<int> ID_Service { get; set; }
    
        public virtual Offer Offer { get; set; }
        public virtual Room Room { get; set; }
        public virtual Servicii Servicii { get; set; }
        public virtual User User { get; set; }
    }
}
