//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DictConverter
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lexeme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lexeme()
        {
            this.LexemeEntries = new HashSet<LexemeEntry>();
            this.LexemeEntries1 = new HashSet<LexemeEntry>();
        }
    
        public int Id { get; set; }
        public string Lexeme1 { get; set; }
        public Nullable<int> HomonymNumber { get; set; }
        public string LexemeOrtho { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LexemeEntry> LexemeEntries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LexemeEntry> LexemeEntries1 { get; set; }
    }
}
