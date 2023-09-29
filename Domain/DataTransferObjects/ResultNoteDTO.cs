using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DataTransferObjects
{
    public class ResultNoteDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNote { get; set; }
        public string TextNote { get; set; }
        public bool IsArchived { get; set; } = false; // New property to indicate if the note is archived

        [ForeignKey("CovenantResult")]
        public int IdCovenantResult { get; set; } // Required foreign key property
        // public CovenantResult? CovenantResult { get; set; }
    }
}
