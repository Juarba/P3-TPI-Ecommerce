namespace Domain.Entities
{
    public abstract class User
    {

        protected int Id { get; set; }
        protected string Name { get; set; }
        protected string LastName { get; set; }
        protected string Email { get; set; }
        protected string Password { get; set; }

        private int _lastId = 0;
     

    }
}

