using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MedAppCore.Models
{
    public enum Status { Start, NewBill, BilligSucces, SendEmail,Succes, Failed };

    [Serializable]
    public class TransactionSetup
    {
        public Guid Id { get; set; }
        public Status TransactionStatus { get; set; }
        public bool EventRaised { get; set; }

        public Guid? AppoitmentId { get; set; }
        public Guid? BillId { get; set; }
    }
}
