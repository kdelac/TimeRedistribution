﻿using MedAppCore.Models;
using System;
using System.Threading.Tasks;

namespace MedAppCore
{
    public interface IBillingService
    {
        Task<Bill> GetBillById(Guid id);
        int CreateBill(Bill newBill);
        Task UpdateBill(Bill billToBeUpdated, Bill bill);
        Task DeleteBill(Bill bill);
    }
}