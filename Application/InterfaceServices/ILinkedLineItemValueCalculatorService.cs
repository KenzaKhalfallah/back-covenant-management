using Domain.Entities;

namespace Application.InterfaceServices
{
    public interface ILinkedLineItemValueCalculatorService
    {
        decimal CalculateLinkedLineItem(LinkedLineItemEnum linkedLineItem, FinancialData data);
        decimal CalculateEBITDA(FinancialData data);
        decimal CalculateEquityRatio(FinancialData data);
        decimal CalculateDebtServiceCoverageRatio(FinancialData data);
        decimal CalculateOperatingCashFlow(FinancialData data);
        decimal CalculateCurrentRatio(FinancialData data);
        decimal CalculateGrossProfit(FinancialData data);
        decimal CalculateDebtToEquityRatio(FinancialData data);
        decimal CalculateInterestCoverageRatio(FinancialData data);
        decimal CalculateInventoryTurnover(FinancialData data);
        decimal CalculateNetOperatingIncome(FinancialData data);
        decimal CalculateOperatingIncome(FinancialData data);
    }
}
