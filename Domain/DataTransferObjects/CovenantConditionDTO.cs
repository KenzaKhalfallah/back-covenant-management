using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.DataTransferObjects
{
    public enum BreachWeight
    {
        MAJOR = 1,
        MODERATE = 2,
        MINOR = 3,
    }
    public class CovenantConditionDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCondition { get; set; }
        public DateTime StartDateCondition { get; set; }
        public DateTime EndDateCondition { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LowerLimitCondition { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UpperLimitCondition { get; set; }
        public bool ContractualFlagCondition { get; set; }
        public bool ExceptionFlagCondition { get; set; }
        public BreachWeight BreachWeight { get; set; }

        [ForeignKey("Covenant")]
        public int IdCovenant { get; set; }
        [JsonIgnore]
        public virtual CovenantResult? CovenantResult { get; set; }
        public virtual FinancialData? FinancialData { get; set; }
    }
}
