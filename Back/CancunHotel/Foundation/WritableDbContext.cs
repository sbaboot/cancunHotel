namespace Foundation.Api.Database.Contract
{
    public interface IWritableDbContext
    {
        int SaveChanges();
        void StartTransaction();
        void Commit();
        void Rollback();
    }
}
