using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Mapping
{
    public class MappingProfile : Profile
    {
        //http://localhost:15672/#/
        public MappingProfile()
        {
            CreateMap<Covenant, CovenantDTO>(); 
            CreateMap<CovenantDTO, Covenant>();
            CreateMap<CovenantCondition, CovenantConditionDTO>();
            CreateMap<CovenantConditionDTO, CovenantCondition>();
            CreateMap<CovenantResult, CovenantResultDTO>();
            CreateMap<CovenantResultDTO, CovenantResult>();
            CreateMap<ResultNote, ResultNoteDTO>();
            CreateMap<ResultNoteDTO, ResultNote>()
                .ForMember(dest => dest.IdNote, opt => opt.Ignore()); // Ignore IdNote during mapping
            CreateMap<Counterparty, CounterpartyDTO>();
            CreateMap<CounterpartyDTO, Counterparty>();
        }
    }
}
