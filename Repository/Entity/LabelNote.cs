using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository.Entity
{
    public class LabelNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("Note")]
        public int Id { get; set; }
        [JsonIgnore]
        public virtual User Note { get; set; }
        [ForeignKey("NoteId")]
        public int NotesId { get; set; }
        
        [JsonIgnore]
        public virtual NoteEntity NoteId { get; set; }
    }
}
