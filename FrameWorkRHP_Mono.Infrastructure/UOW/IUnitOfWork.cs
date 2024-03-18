using FrameWorkRHP_Mono.Infrastructure.Repository;
using FrameWorkRHP_Mono.Core.Models.EF;


namespace FrameWorkRHP_Mono.Infrastructure.UOW
{
    public interface IUnitOfWork
    {
        GenericRepository<Muser> MUsers { get; }
        GenericRepository<Mrole> MRoles { get; }
        GenericRepository<MMenu> MMenus { get; }
        GenericRepository<MRoleXMenu> MRoleXMenus { get; }
        IGenericDataTables GenericDataTables { get; } 
        void CreateTransaction();
        void Commit();
        void Rollback();
        Task Save();
    }
}
