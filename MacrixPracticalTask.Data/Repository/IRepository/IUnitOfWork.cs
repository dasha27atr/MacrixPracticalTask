namespace MacrixPracticalTask.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPersonRepository Person { get; }

        void Save();
    }
}
