using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ResultNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNote { get; set; }
        public string TextNote { get; set; }
        public bool IsArchived { get; set; } = false;// New property to indicate if the note is archived

        [ForeignKey("CovenantResult")]
        public int IdCovenantResult { get; set; } // Required foreign key property
       // public CovenantResult? CovenantResult { get; set; }
    }
}
