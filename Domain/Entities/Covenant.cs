using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum CategoryCovenant
    {
        FINANCIAL = 1,
        NONFINANCIAL = 2
    }

    public enum TypeCovenant
    {
        AFFIRMATIVE = 1,
        NEGATIVE = 2
    }
    public enum PeriodTypeCovenant
    {
        ANNUAL = 1,
        YTD = 2,
        INTERIM = 3
    }

    public enum StatementSourceCovenant
    {
        COMPANY = 1,
        CONSOLIDATION = 2,
    }
    public enum LinkedLineItemEnum
    {
        EBITDA = 1,
        EquityRatio = 2,
        DebtServiceCoverageRatio = 3,
        OperatingCashFlow = 4,
        CurrentRatio = 5,
        GrossProfit = 6,
        DebtToEquityRatio = 7,
        InterestCoverageRatio = 8,
        InventoryTurnover = 9,
    }
    public class Covenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCovenant { get; set; }
        public string NameCovenant { get; set; }
        public CategoryCovenant CategoryCovenant { get; set; }
        public string? DescriptionCovenant { get; set; }
        public TypeCovenant TypeCovenant { get; set; }
        public PeriodTypeCovenant PeriodTypeCovenant { get; set; }
        public StatementSourceCovenant StatementSourceCovenant { get; set; }
        public LinkedLineItemEnum? LinkedLineItem { get; set; } = null;

        public virtual List<CovenantCondition> CovenantConditions { get; set; }

        [ForeignKey("Counterparty")]
        public int IdCounterparty { get; set; }
    }
}
