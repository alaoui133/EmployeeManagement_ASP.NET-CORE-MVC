namespace EmployeeManagement.Models.Repositories
{
    public interface ICompanyRepository<TEntity> 
    {
        TEntity Get(int id);
        TEntity Add(TEntity entity);   
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity EntityChanges);
        TEntity Delete(int id);
    }
}
