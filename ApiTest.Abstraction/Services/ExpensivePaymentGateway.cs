﻿using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Services.Enum;
using ApiTest.SQL;
using ApiTest.SQL.DBModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Services
{
    public class ExpensivePaymentGateway : IPaymentGateway
    {
        private readonly IEntityRepository<Payment> _paymentRepository;
        private readonly IMapper _mapper;
        private bool _isavailable;
        public ExpensivePaymentGateway(IEntityRepository<Payment> paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _isavailable = (new Random().Next(100) <= 20 ? true : false);
        }

        public bool IsAvailable  // read-write instance property
        {
            get => _isavailable;
            set => _isavailable = value;
        }
        public async Task<bool> ProcessPayment(CardDetails cardDetails)
        {
            var payment = _mapper.Map<Payment>(cardDetails);

            payment.Status = "pending";
            payment.GateWayType = "Expensive";
            await _paymentRepository.AddAsync(payment);

            return true;
        }
    }
}
