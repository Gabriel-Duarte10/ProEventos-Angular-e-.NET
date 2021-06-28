
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Migrations;

namespace ProEventos.Persistence
{
    public class EventosPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventosPersist(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includeEvento = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.lote)
                .Include(e => e.RedeSociais);
                if(includeEvento)
                {
                    query = query
                        .Include(e => e.PalestranteEvento)
                        .ThenInclude(pe => pe.Palestrante);
                }
                query = query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
                return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includeEvento = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.lote)
                .Include(e => e.RedeSociais);
                if(includeEvento)
                {
                    query = query
                        .Include(e => e.PalestranteEvento)
                        .ThenInclude(pe => pe.Palestrante);
                }
                query = query.OrderBy(e => e.Id);
                return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includeEvento = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.lote)
                .Include(e => e.RedeSociais);
                if(includeEvento)
                {
                    query = query
                        .Include(e => e.PalestranteEvento)
                        .ThenInclude(pe => pe.Palestrante);
                }
                query = query.OrderBy(e => e.Id).Where(e => e.Id == eventoId);
                return await query.FirstOrDefaultAsync();
        }

    }
}