using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public abstract class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        protected int Id { get; set; }
        protected string Name { get; set; }
        protected string LastName { get; set; }
        protected string Email { get; set; }
        protected string Password { get; set; }
        public Rol UserRol { get; set; }
    }
}

   