using Domain.Entities;
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
    public enum ResultStatus
    {
        PASSED = 1,
        FAILED = 2
    }
    public class CovenantResultDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdResult { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public DateTime ResultDate { get; set; }

        [JsonIgnore]
        public virtual List<ResultNote>? ResultNotes { get; set; }

        [ForeignKey("CovenantCondition")]
        public int IdCondition { get; set; }
        //public virtual CovenantCondition? CovenantCondition { get; set; }

    }
}
