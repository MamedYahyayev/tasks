namespace LookScoreAdmin.Service.EntityServices
{
    public interface ICrudOperation<T>
    {
        T[] FindAll();

        T FindOne();

        void Insert(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
