using System;
using System.ComponentModel.DataAnnotations;
namespace EntityFramework.Spike.Entities
{
    public class Session : IEntity
    {
        [Key]
        public string Token { get; set; }

        public string Provider { get; set; }

        public string ID { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
