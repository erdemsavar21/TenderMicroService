using AutoMapper;
using EventBusRabbitMQ1.Events;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tender.Order.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}
