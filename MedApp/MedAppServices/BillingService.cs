﻿using MedAppCore;
using MedAppCore.Models;
using System;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class BillingService : IBillingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BillingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  int CreateBill(Bill newBill)
        {
            return 2;
        }

        public Task DeleteBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public Task<Bill> GetBillById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBill(Bill billToBeUpdated, Bill bill)
        {
            throw new NotImplementedException();
        }
    }
}