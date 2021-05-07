using AutoMapper;
using EventBusRabbitMQ1.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tender.Tendering.Entities;

namespace Tender.Tendering.Mapping
{
    public class SourcingMapping : Profile
    {
        public SourcingMapping()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}
