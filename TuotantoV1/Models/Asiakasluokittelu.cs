//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TuotantoV1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asiakasluokittelu
    {
        public int Luokitteluid { get; set; }
        public int Asiakasnumero { get; set; }
        public Nullable<bool> Eläkeläisalennus { get; set; }
        public Nullable<bool> Tv { get; set; }
        public Nullable<bool> Pöytäkone { get; set; }
        public Nullable<bool> Kannettava { get; set; }
        public Nullable<bool> Matkapuhelin { get; set; }
        public Nullable<bool> Tabletti { get; set; }
        public Nullable<bool> Mokkula { get; set; }
        public Nullable<bool> Wlan { get; set; }
    
        public virtual Asiakkaanperustiedot Asiakkaanperustiedot { get; set; }
    }
}
