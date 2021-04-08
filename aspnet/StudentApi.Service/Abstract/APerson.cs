namespace StudentApi.Abstracts
{
    public abstract class APerson : AEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        protected APerson(){}
    }
}
