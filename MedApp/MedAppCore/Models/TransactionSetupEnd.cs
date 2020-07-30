using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    public class TransactionSetupEnd
    {
        public Guid Id { get; set; }
        public Status TransactionStatus { get; set; }
        public Guid AppoitmentId { get; set; }
        public Guid BillId { get; set; }
    }
}
