using Escola.Context;

namespace Escola.Repository
{
    public class BaseRepository<TEntity,TKey> where TEntity : class
    {
        protected readonly EscolaDbContext _context;
        public BaseRepository(EscolaDbContext context)
        {
            _context = context;
        }

        public virtual TEntity Atualizar(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Excluir(TEntity entity)
        {

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual TEntity Inserir(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual TEntity ObterPorId(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual List<TEntity> ObterTodos()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}
