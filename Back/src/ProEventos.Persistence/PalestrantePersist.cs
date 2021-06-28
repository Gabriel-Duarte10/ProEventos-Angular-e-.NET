
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;

        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includePalestrantes = false)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedeSocials);
                if(includePalestrantes)
                {
                    query = query
                        .Include(p => p.PalestranteEvento)
                        .ThenInclude(pe => pe.Evento);
                }
                query = query.OrderBy(e => e.Id);
                return await query.ToArrayAsync();
        }
        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includePalestrantes = false)
        {
                IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedeSocials);
                if(includePalestrantes)
                {
                    query = query
                        .Include(p => p.PalestranteEvento)
                        .ThenInclude(pe => pe.Evento);
                }
                query = query.OrderBy(e => e.Id)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
                return await query.ToArrayAsync();
        }
        
        public async Task<Palestrante> GetPalestranteByIdAsync(int palestrantesId, bool includePalestrantes = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedeSocials);
                if(includePalestrantes)
                {
                    query = query
                        .Include(p => p.PalestranteEvento)
                        .ThenInclude(pe => pe.Evento);
                }
                query = query.OrderBy(e => e.Id)
                .Where(p => p.Id == palestrantesId);
                
                return await query.FirstOrDefaultAsync();
        }
    }
}