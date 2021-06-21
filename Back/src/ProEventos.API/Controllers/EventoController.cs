using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> _evento = new Evento[]{
    
                new Evento(){
                    EventoId = 1,
                    Tema = "Angular 11 e .NET 5",
                    Local = "Rio de Janeiro",
                    Lote = "1º Lote",
                    QtdPessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/aaaa"),
                    ImagemURL = "Imagem.png"
                },
                new Evento(){
                    EventoId = 2,
                    Tema = "Angular 11 e .NET 5 e Suas novidades",
                    Local = "São paulo",
                    Lote = "º Lote",
                    QtdPessoas = 2550,
                    DataEvento = DateTime.Now.AddDays(3).ToString(),
                    ImagemURL = "Imagem.png"
                }
        };
        public EventoController()
        {
    
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
           return _evento;
        }
        [HttpGet("{id}")]
        public IEnumerable<Evento> Get(int id)
        {
           return _evento.Where(evento => evento.EventoId == id);
        }
        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";
        }
        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de put com ID = {id}";
        }
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de Delete com ID = {id}";
        }
    }
}
