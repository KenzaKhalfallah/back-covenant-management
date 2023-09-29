using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FinancialData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFinancialData { get; set; }
        public decimal? NetIncome { get; set; }
        public decimal? Interest { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? TotalEquity { get; set; }
        public decimal? TotalAssets { get; set; }
        public decimal? OperatingExpenses { get; set; }
        public decimal? TotalRevenues { get; set; }
        public decimal? Depreciation { get; set; }
        public decimal? Amortization { get; set; }
        public decimal? CurrentAssets { get; set; }
        public decimal? CurrentLiabilities { get; set; }
        public decimal? GrossProfit { get; set; }
        public decimal? TotalDebt { get; set; }
        public decimal? TotalDebtService { get; set; }
        public decimal? InterestExpense { get; set; }
        public decimal? CostOfGoodsSold { get; set; }
        public decimal? AverageInventory { get; set; }

        [ForeignKey("CovenantCondition")]
        public int IdCondition { get; set; }
       // public virtual CovenantCondition? CovenantCondition { get; set; }

    }
}
