using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    public enum Status { didnt = 0, success = 1, failed = 2 };

    [Serializable]
    public class TransactionSetup
    {
        public Guid Id { get; set; }
        public Status AppoitmentCreated { get; set; } = Status.didnt;
        public Status BillCreated { get; set; } = Status.didnt;
        public Status MailSent { get; set; } = Status.didnt;
    }
}
