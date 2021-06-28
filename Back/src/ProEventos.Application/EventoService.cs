using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventosService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _eventoPersist = eventoPersist;
            _geralPersist = geralPersist;
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.add<Evento>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new  Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEventos(int eventosId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventosId, false);
                if (evento == null)
                {
                    return null;
                }
                model.Id = evento.Id;
                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message) ;
            }
        }
        public async Task<bool> DeleteEventos(int eventosId)
        {
                try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventosId, false);
                if (evento == null) throw new Exception("Evento Para Delete Não Encontrado.");
               
                _geralPersist.Delete<Evento>(evento);

                return await _geralPersist.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message) ;
            }
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if(evento == null) return null;
                
                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if(evento == null) return null;
                
                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if(evento == null) return null;
                
                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}