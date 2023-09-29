using Application.InterfaceServices;
using Application.RabbitMQ;
using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CovenantManagementWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CovenantConditionController : ControllerBase
    {
        private readonly ICovenantConditionService _covenantConditionService;
        private readonly ICovenantService _covenantService;
        private readonly ICovenantResultService _covenantResultService;
        private readonly ILinkedLineItemValueCalculatorService _linkedLineItemValueCalculatorService;
        private readonly IRabbitMQ _rabbitMQ;
        private readonly IMapper _mapper;

        public CovenantConditionController(ICovenantConditionService covenantConditionService, ICovenantService covenantService, ICovenantResultService covenantResultService, ILinkedLineItemValueCalculatorService linkedLineItemValueCalculatorService, IMapper mapper, IRabbitMQ rabbitMQ)
        {
            _covenantConditionService = covenantConditionService;
            _covenantService = covenantService;
            _covenantResultService = covenantResultService;
            _linkedLineItemValueCalculatorService = linkedLineItemValueCalculatorService;
            _mapper = mapper;
            _rabbitMQ = rabbitMQ;
        }

        [HttpGet]
        public IActionResult GetAllCovenantConditions()
        {
            var covenantConditions = _covenantConditionService.GetAllCovenantConditions();
            return Ok(covenantConditions);
        }

        [HttpGet("{id}")]
        public IActionResult GetCovenantConditionById(int id)
        {
            var covenantCondition = _covenantConditionService.GetCovenantConditionById(id);
            if (covenantCondition == null)
            {
                return NotFound();
            }
            return Ok(covenantCondition);
        }

        [HttpPost("{covenantId}")]
        public IActionResult AddCovenantCondition(int covenantId, [FromBody] CovenantConditionDTO conditionDTO)
        {
            var existingCovenant = _covenantService.GetCovenantById(covenantId);
            if (existingCovenant == null)
            {
                return NotFound();
            }
            var linkedLineItem = existingCovenant.LinkedLineItem;
            if (conditionDTO.LowerLimitCondition == 0 && conditionDTO.UpperLimitCondition == 0)
            {
                if (existingCovenant.TypeCovenant == Domain.Entities.TypeCovenant.AFFIRMATIVE)
                {
                    if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.EBITDA)
                    {
                        conditionDTO.LowerLimitCondition = 100.000m;
                        conditionDTO.UpperLimitCondition = 500.000m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.EquityRatio)
                    {
                        conditionDTO.LowerLimitCondition = 0.2m;
                        conditionDTO.UpperLimitCondition = 0.5m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.DebtServiceCoverageRatio)
                    {
                        conditionDTO.LowerLimitCondition = 1.5m;
                        conditionDTO.UpperLimitCondition = 3.0m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.OperatingCashFlow)
                    {
                        conditionDTO.LowerLimitCondition = 50.000m;
                        conditionDTO.UpperLimitCondition = 200.000m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.CurrentRatio)
                    {
                        conditionDTO.LowerLimitCondition = 1.2m;
                        conditionDTO.UpperLimitCondition = 2.0m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.GrossProfit)
                    {
                        conditionDTO.LowerLimitCondition = 100.000m;
                        conditionDTO.UpperLimitCondition = 500.000m;
                    }
                }
                else if (existingCovenant.TypeCovenant == Domain.Entities.TypeCovenant.NEGATIVE)
                {
                    if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.CurrentRatio)
                    {
                        conditionDTO.LowerLimitCondition = 1.0m;
                        conditionDTO.UpperLimitCondition = 1.5m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.GrossProfit)
                    {
                        conditionDTO.LowerLimitCondition = 200.000m;
                        conditionDTO.UpperLimitCondition = 400.000m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.DebtToEquityRatio)
                    {
                        conditionDTO.LowerLimitCondition = 0.4m;
                        conditionDTO.UpperLimitCondition = 0.7m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.InterestCoverageRatio)
                    {
                        conditionDTO.LowerLimitCondition = 2.0m;
                        conditionDTO.UpperLimitCondition = 5.0m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.InventoryTurnover)
                    {
                        conditionDTO.LowerLimitCondition = 5.0m;
                        conditionDTO.UpperLimitCondition = 10.0m;
                    }
                }
                else
                {
                    return BadRequest("Invalid linked line item selection.");
                }
            }
           
            conditionDTO.IdCovenant = existingCovenant.IdCovenant;
            var covenantCondition = _mapper.Map<CovenantCondition>(conditionDTO);

            //Calculate the current value of the linkedLineItem
            Domain.Entities.LinkedLineItemEnum lineItem = (Domain.Entities.LinkedLineItemEnum)existingCovenant.LinkedLineItem;
            decimal linkedLineItemValue = _linkedLineItemValueCalculatorService.CalculateLinkedLineItem(lineItem, conditionDTO.FinancialData);
            Console.WriteLine("linkedLineItem Current Value : " + linkedLineItemValue);
            var covenantResult = new CovenantResult
            {
                ResultStatus = (linkedLineItemValue >= conditionDTO.LowerLimitCondition && linkedLineItemValue <= conditionDTO.UpperLimitCondition)
                                ? Domain.Entities.ResultStatus.PASSED
                                : Domain.Entities.ResultStatus.FAILED,
                ResultDate = DateTime.Now,
                IdCondition = covenantCondition.IdCondition
            };
            _covenantResultService.CreateCovenantResult(covenantResult);
            Console.WriteLine("Result : " + covenantResult.ResultStatus);
            covenantCondition.CovenantResult = covenantResult;
            covenantCondition.FinancialData = conditionDTO.FinancialData;

            existingCovenant.CovenantConditions.Add(covenantCondition);
            _covenantConditionService.AddCovenantCondition(covenantCondition);

            _rabbitMQ.SendCovenantMessage("New condition added: \n" + covenantCondition + 
                "\n at :" + DateTime.Now + " With Current Value = " + linkedLineItem + 
                " and Result = " + covenantResult.ResultStatus);
            return Ok(covenantCondition);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCovenantCondition(int id, CovenantConditionDTO covenantConditionDTO)
        {
            var existingCondition = _covenantConditionService.GetCovenantConditionById(id);
            if (existingCondition == null)
            {
                return NotFound();
            }
            var existingCovenant = _covenantService.GetCovenantById(existingCondition.IdCovenant);
            var linkedLineItem = existingCovenant.LinkedLineItem;
            if (covenantConditionDTO.LowerLimitCondition == 0 && covenantConditionDTO.UpperLimitCondition == 0)
            {
                if (existingCovenant.TypeCovenant == Domain.Entities.TypeCovenant.AFFIRMATIVE)
                {
                    if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.EBITDA)
                    {
                        covenantConditionDTO.LowerLimitCondition = 100.000m;
                        covenantConditionDTO.UpperLimitCondition = 500.000m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.EquityRatio)
                    {
                        covenantConditionDTO.LowerLimitCondition = 0.2m;
                        covenantConditionDTO.UpperLimitCondition = 0.5m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.DebtServiceCoverageRatio)
                    {
                        covenantConditionDTO.LowerLimitCondition = 1.5m;
                        covenantConditionDTO.UpperLimitCondition = 3.0m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.OperatingCashFlow)
                    {
                        covenantConditionDTO.LowerLimitCondition = 50.000m;
                        covenantConditionDTO.UpperLimitCondition = 200.000m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.CurrentRatio)
                    {
                        covenantConditionDTO.LowerLimitCondition = 1.2m;
                        covenantConditionDTO.UpperLimitCondition = 2.0m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.GrossProfit)
                    {
                        covenantConditionDTO.LowerLimitCondition = 100.000m;
                        covenantConditionDTO.UpperLimitCondition = 500.000m;
                    }
                }
                else if (existingCovenant.TypeCovenant == Domain.Entities.TypeCovenant.NEGATIVE)
                {
                    if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.CurrentRatio)
                    {
                        covenantConditionDTO.LowerLimitCondition = 1.0m;
                        covenantConditionDTO.UpperLimitCondition = 1.5m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.GrossProfit)
                    {
                        covenantConditionDTO.LowerLimitCondition = 200.000m;
                        covenantConditionDTO.UpperLimitCondition = 400.000m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.DebtToEquityRatio)
                    {
                        covenantConditionDTO.LowerLimitCondition = 0.4m;
                        covenantConditionDTO.UpperLimitCondition = 0.7m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.InterestCoverageRatio)
                    {
                        covenantConditionDTO.LowerLimitCondition = 2.0m;
                        covenantConditionDTO.UpperLimitCondition = 5.0m;
                    }
                    else if (linkedLineItem == Domain.Entities.LinkedLineItemEnum.InventoryTurnover)
                    {
                        covenantConditionDTO.LowerLimitCondition = 5.0m;
                        covenantConditionDTO.UpperLimitCondition = 10.0m;
                    }
                }
                else
                {
                    return BadRequest("Invalid linked line item selection.");
                }
            }
            covenantConditionDTO.IdCondition = existingCondition.IdCondition;
            covenantConditionDTO.IdCovenant = existingCondition.IdCovenant;

            //Calculate the current value of the linkedLineItem
            Domain.Entities.LinkedLineItemEnum lineItem = (Domain.Entities.LinkedLineItemEnum)existingCovenant.LinkedLineItem;
            decimal linkedLineItemValue = _linkedLineItemValueCalculatorService.CalculateLinkedLineItem(lineItem, covenantConditionDTO.FinancialData);
            Console.WriteLine("linkedLineItem Current Value : " + linkedLineItemValue);
            // RE-Generate CovenantResult if the date is within start/end Date
            if (existingCondition.CovenantResult.ResultDate >= covenantConditionDTO.StartDateCondition && existingCondition.CovenantResult.ResultDate <= covenantConditionDTO.EndDateCondition)
            {
                covenantConditionDTO.CovenantResult = existingCondition.CovenantResult;
            }
            else
            {
                var covenantResult = existingCondition.CovenantResult;
                covenantResult.ResultStatus = (linkedLineItemValue >= covenantConditionDTO.LowerLimitCondition && linkedLineItemValue <= covenantConditionDTO.UpperLimitCondition)
                                ? Domain.Entities.ResultStatus.PASSED
                                : Domain.Entities.ResultStatus.FAILED;
                covenantResult.ResultDate = DateTime.Now;
                covenantResult.IdCondition = existingCondition.IdCondition;
                _covenantResultService.UpdateCovenantResult(covenantResult);
                Console.WriteLine("Result : " + covenantResult.ResultStatus);
                covenantConditionDTO.CovenantResult = covenantResult;
            }
            existingCondition.FinancialData = covenantConditionDTO.FinancialData;

            _mapper.Map(covenantConditionDTO, existingCondition);
            _covenantConditionService.UpdateCovenantCondition(existingCondition);
            _rabbitMQ.SendCovenantMessage("Condition updated: \n" + existingCondition +
                "\n at :" + DateTime.Now);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCovenantCondition(int id)
        {
            var covenantCondition = _covenantConditionService.GetCovenantConditionById(id);
            if (covenantCondition == null)
            {
                return NotFound();
            }
            _covenantConditionService.DeleteCovenantCondition(covenantCondition);
            _rabbitMQ.SendCovenantMessage("Condition deleted." + "\n at :" + DateTime.Now);
            return Ok();
        }
    }
}
