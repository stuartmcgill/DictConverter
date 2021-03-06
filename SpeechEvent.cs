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
    
    public partial class SpeechEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SpeechEvent()
        {
            this.SpeechEventComments = new HashSet<SpeechEventComment>();
            this.SpeechEventContributors = new HashSet<SpeechEventContributor>();
            this.SpeechEventCreators = new HashSet<SpeechEventCreator>();
            this.SpeechEventFiles = new HashSet<SpeechEventFile>();
            this.SpeechEventTopics = new HashSet<SpeechEventTopic>();
        }
    
        public int Id { get; set; }
        public string ToolboxCode { get; set; }
        public string Title { get; set; }
        public int EventTypeId { get; set; }
        public Nullable<int> GenreId { get; set; }
        public int LanguageId { get; set; }
        public Nullable<System.DateTime> RecordingDate { get; set; }
        public string RecordingPlace { get; set; }
        public string RecordingEquipment { get; set; }
        public string Description { get; set; }
        public string AccessRights { get; set; }
        public Nullable<bool> Reviewed { get; set; }
        public string RecordingQuality { get; set; }
        public Nullable<bool> Interlinearised { get; set; }
    
        public virtual Genre Genre { get; set; }
        public virtual Language Language { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpeechEventComment> SpeechEventComments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpeechEventContributor> SpeechEventContributors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpeechEventCreator> SpeechEventCreators { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpeechEventFile> SpeechEventFiles { get; set; }
        public virtual SpeechEventType SpeechEventType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpeechEventTopic> SpeechEventTopics { get; set; }
    }
}
