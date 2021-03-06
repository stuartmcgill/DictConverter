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
    
    public partial class Contributor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contributor()
        {
            this.ContributorImages = new HashSet<ContributorImage>();
            this.ContributorLanguages = new HashSet<ContributorLanguage>();
            this.SenseReferences = new HashSet<SenseReference>();
            this.SpeechEventContributors = new HashSet<SpeechEventContributor>();
            this.SpeechEventCreators = new HashSet<SpeechEventCreator>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int EthnicGroupId { get; set; }
        public string Birthplace { get; set; }
        public string ParentalDetails { get; set; }
        public string CurrentResidence { get; set; }
        public string ChildhoodResidence { get; set; }
        public string TimeAtCurrentResidence { get; set; }
        public string LevelEducation { get; set; }
        public string Occupation { get; set; }
        public int SexId { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string AccessRights { get; set; }
        public string Comment { get; set; }
        public string ToolboxInitials { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContributorImage> ContributorImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContributorLanguage> ContributorLanguages { get; set; }
        public virtual EthnicGroup EthnicGroup { get; set; }
        public virtual Sex Sex { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SenseReference> SenseReferences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpeechEventContributor> SpeechEventContributors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpeechEventCreator> SpeechEventCreators { get; set; }
    }
}
