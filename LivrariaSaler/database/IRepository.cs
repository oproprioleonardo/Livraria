namespace LivrariaSaler.database;

public interface IRepository<in TK, TO>
{
    void Insert(TO obj);
    void Delete(TK key);
    TO Find(TK key);
    void Update(TO obj);
    int Count();

}