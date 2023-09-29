using Application.InterfaceServices;
using Domain.Entities;

namespace Application.Services
{
    public class LinkedLineItemValueCalculatorService : ILinkedLineItemValueCalculatorService
    {
        public LinkedLineItemValueCalculatorService()
        {
        }

        public decimal CalculateLinkedLineItem(LinkedLineItemEnum linkedLineItem, FinancialData data)
        {
            switch (linkedLineItem)
            {
                case LinkedLineItemEnum.EBITDA:
                    return CalculateEBITDA(data);
                case LinkedLineItemEnum.EquityRatio:
                    return CalculateEquityRatio(data);
                case LinkedLineItemEnum.DebtServiceCoverageRatio:
                    return CalculateDebtServiceCoverageRatio(data);
                case LinkedLineItemEnum.OperatingCashFlow:
                    return CalculateOperatingCashFlow(data);
                case LinkedLineItemEnum.CurrentRatio:
                    return CalculateCurrentRatio(data);
                case LinkedLineItemEnum.GrossProfit:
                    return CalculateGrossProfit(data);
                case LinkedLineItemEnum.DebtToEquityRatio:
                    return CalculateDebtToEquityRatio(data);
                case LinkedLineItemEnum.InterestCoverageRatio:
                    return CalculateInterestCoverageRatio(data);
                case LinkedLineItemEnum.InventoryTurnover:
                    return CalculateInventoryTurnover(data);
                default:
                    return 0; // Handle invalid linked line item
            }
        }

        public decimal CalculateEBITDA(FinancialData data)
        {
            return (decimal)(data.NetIncome + data.Interest + data.Taxes + data.Depreciation + data.Amortization);
        }

        public decimal CalculateEquityRatio(FinancialData data)
        {
            return (decimal)((data.TotalAssets != 0) ? data.TotalEquity / data.TotalAssets : 0);
        }

        public decimal CalculateDebtServiceCoverageRatio(FinancialData data)
        {
            decimal netOperatingIncome = CalculateNetOperatingIncome(data);
            return (decimal)((data.TotalDebtService != 0) ? netOperatingIncome / data.TotalDebtService : 0);
        }

        public decimal CalculateOperatingCashFlow(FinancialData data)
        {
            return (decimal)(data.TotalRevenues - data.OperatingExpenses - data.Taxes);
        }

        public decimal CalculateCurrentRatio(FinancialData data)
        {
            return (decimal)((data.CurrentLiabilities != 0) ? data.CurrentAssets / data.CurrentLiabilities : 0);
        }

        public decimal CalculateGrossProfit(FinancialData data)
        {
            return (decimal)((data.TotalRevenues != 0) ? (data.GrossProfit / data.TotalRevenues) * 100 : 0);
        }

        public decimal CalculateDebtToEquityRatio(FinancialData data)
        {
            return (decimal)((data.TotalEquity != 0) ? data.TotalDebt / data.TotalEquity : 0);
        }

        public decimal CalculateInterestCoverageRatio(FinancialData data)
        {
            decimal ebitda = CalculateEBITDA(data);
            return (decimal)((data.InterestExpense != 0) ? ebitda / data.InterestExpense : 0);
        }

        public decimal CalculateInventoryTurnover(FinancialData data)
        {
            return (decimal)((data.AverageInventory != 0) ? data.CostOfGoodsSold / data.AverageInventory : 0);
        }

        public decimal CalculateNetOperatingIncome(FinancialData data)
        {
            decimal operatingIncome = CalculateOperatingIncome(data);
            return (decimal)(operatingIncome + data.Depreciation);
        }

        public decimal CalculateOperatingIncome(FinancialData data)
        {
            return (decimal)(data.TotalRevenues - data.OperatingExpenses);
        }

    }
}
