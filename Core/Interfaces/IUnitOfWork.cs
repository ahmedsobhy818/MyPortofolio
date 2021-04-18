namespace Core.Interfaces
{
    public interface IUnitOfWork<T> where T:class// T is not used
        //T is used donly to differentiate between unit of works
        //so when i want to use uok1 in a controller then i will do :
        // uok1 itself :   public class uok1 : UnitOfWork <uok1>
        //in startrup.cs     services.AddScoped<IUnitOfWork<uok1>, uok1>();
        //in the controller's constructor   public ctor(IUnitOfWork<uok1> uow) 
        //there is another technique where i can inject uok1 directly not IUnitOfWork<uok1>
        //in this case i will not use T in IUnitOfWork nor in class UnitOfWork 
        //so it seems better , but i am not sure about its preformance
        // i menetioned this technique in the last 5 lines in startup.cs---> ConfigureServices()
    {
        //  IGenericRepository<T> Entity { get; }
        void Save();
    }
}
