using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository.Entity
{
    public class CollaborativeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollabId { get; set; }
        public string CollabEmail {  get; set; }
        public bool Trash {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

        [ForeignKey("Note")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User Note { get; set; }
        [ForeignKey("NoteId")]
        public int NotesId {  get; set; }
        [JsonIgnore]
        public virtual NoteEntity NoteId {  get; set; }
    }
}
