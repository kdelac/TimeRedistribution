using DDD.BuildingBlocks.Domain;
using Scheduling.Domain.Clinics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.MedicalStaff
{
    public class MedicalStuff : Entity, IAggregateRoot
    {
        public MedicalStuffId Id { get; private set; }
        private string _firstname;
        private string _lastname;
        private DateTime _dateOfBirth;
        private ClinicId _clinicId;
        private MedicalStuffRole _medicalStuffRole;

        public MedicalStuff()
        {

        }

        internal static MedicalStuff CreateNewDoctor(
            ClinicId clinicId,
            string firstname,
            string lastname,
            DateTime dateOfBirth)
        {            
            return new MedicalStuff(
                clinicId,
                firstname,
                lastname,
                dateOfBirth,
                MedicalStuffRole.Doctor);
        }

        internal static MedicalStuff CreateNewNurse(
            ClinicId clinicId,
            string firstname,
            string lastname,
            DateTime dateOfBirth)
        {
            return new MedicalStuff(
                clinicId,
                firstname,
                lastname,
                dateOfBirth,
                MedicalStuffRole.Nurse);
        }

        public MedicalStuff(
            ClinicId clinicId,
            string firstname,
            string lastname,
            DateTime dateOfBirth,
            MedicalStuffRole medicalStuffRole)
        {
            Id = new MedicalStuffId(Guid.NewGuid());
            _firstname = firstname;
            _lastname = lastname;
            _dateOfBirth = dateOfBirth;
            _medicalStuffRole = medicalStuffRole;

        }
    }
}
