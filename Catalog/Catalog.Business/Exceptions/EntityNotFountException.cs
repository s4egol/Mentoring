namespace Catalog.Business.Exceptions
{
    public class EntityNotFountException : Exception
    {
        public EntityNotFountException() : base() { }
        public EntityNotFountException(string message) : base(message) { }
        public EntityNotFountException(string message, Exception innerException) : base(message, innerException) { }
    }
}
