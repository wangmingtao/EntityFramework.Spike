using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Spike.Entities
{
    [Table("SubSession")]
    public class SubSession : IEntity
    {
        [Key]
        [Column(Order = 0)]
        public string Token { get; set; }
        [Key]
        [Column(Order = 1)]
        public string SubSessionName { get; set; }

        public Session Session { get; set; }
    }
}
