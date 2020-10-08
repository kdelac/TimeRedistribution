using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MedAppCore.Models
{
    public enum Role
    {
        Doctor,
        Patient
    }
    public class Users {         

        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
    
    public class UsersResource
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
