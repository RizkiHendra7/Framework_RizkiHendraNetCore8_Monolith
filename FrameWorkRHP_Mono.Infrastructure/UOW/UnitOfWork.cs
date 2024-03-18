using FrameWorkRHP_Mono.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Infrastructure.Context;
using FrameWorkRHP_Mono.Core.CommonFunction;


namespace FrameWorkRHP_Mono.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public AppsDbContext Context = null;
        private IDbContextTransaction? _objTran = null;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        public IUnitOfWork _unitOfWork;

        public GenericRepository<Muser> MUsers { get; private set; }
        public GenericRepository<Mrole> MRoles { get; private set; }
        public GenericRepository<MMenu> MMenus { get; private set; }
        public GenericRepository<MRoleXMenu> MRoleXMenus { get; private set; } 
        public IGenericDataTables GenericDataTables { get; private set; }  

        public UnitOfWork(AppsDbContext _context)
        {
            Context = _context;
            MUsers = new GenericRepository<Muser>(Context);
            MRoles = new GenericRepository<Mrole>(Context);
            MMenus = new GenericRepository<MMenu>(Context);
            MRoleXMenus = new GenericRepository<MRoleXMenu>(Context);
            GenericDataTables = new GenericDataTables(Context); 
        }

        public void CreateTransaction()
        {
            _objTran = Context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _objTran?.Commit();
        }
        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }
        public async Task Save()
        {
            try
            {

                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var exceptionEntries = ex.Entries.Where(e => e.State == EntityState.Added);
                foreach (var entry in exceptionEntries)
                {
                    foreach (var error in entry.Entity.GetType().GetProperties()
                        .Where(prop => !Attribute.IsDefined(prop, typeof(NotMappedAttribute)) &&
                                       prop.GetValue(entry.Entity) == null)
                        .Select(prop => new { Property = prop.Name }))
                    {
                        _errorMessage += $"Property: {error.Property} Error: Validation Failed {Environment.NewLine}";
                    }
                }
                _errorMessage = string.IsNullOrEmpty(_errorMessage) ? ex.Message : _errorMessage;
                throw new Exception(_errorMessage, ex);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();
            _disposed = true;
        }
    }
}
