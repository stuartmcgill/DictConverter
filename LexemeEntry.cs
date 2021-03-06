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
    
    public partial class LexemeEntry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LexemeEntry()
        {
            this.Senses = new HashSet<Sens>();
            this.SenseXReferences = new HashSet<SenseXReference>();
        }
    
        public int Id { get; set; }
        public int LexemeId { get; set; }
        public int TypeId { get; set; }
        public int Order { get; set; }
        public string Citation { get; set; }
        public string Phonetic { get; set; }
        public string TonePattern { get; set; }
        public string LoanwordComment { get; set; }
        public int PartOfSpeechId { get; set; }
        public string SingularForm { get; set; }
        public string PluralForm { get; set; }
        public string Gender { get; set; }
        public string VerbalComment { get; set; }
        public string Literally { get; set; }
        public Nullable<int> MainEntryId { get; set; }
        public string CitationOrtho { get; set; }
    
        public virtual PartsOfSpeech PartsOfSpeech { get; set; }
        public virtual Lexeme Lexeme { get; set; }
        public virtual LexemeEntryType LexemeEntryType { get; set; }
        public virtual Lexeme Lexeme1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sens> Senses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SenseXReference> SenseXReferences { get; set; }
    }
}
