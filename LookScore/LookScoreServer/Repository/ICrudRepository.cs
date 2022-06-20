namespace LookScoreServer.Repository
{
    public interface ICrudRepository<T>
    {
        T[] FindAll();

        T FindOne(int id);

        void Insert(T entity);

        T InsertAndReturn(T entity);    

        void Update(T entity);

        void Delete(int id);
    }
}
