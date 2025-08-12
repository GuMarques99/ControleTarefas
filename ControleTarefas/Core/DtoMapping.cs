using AutoMapper;
using Core.DTOs.Tarefa;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DtoMapping : Profile
    {
        public DtoMapping()
        {
            CreateMap<Tarefa, CriarTarefaDTO>().ReverseMap();
            CreateMap<Tarefa, BuscarTarefaDTO>().ReverseMap();
        }
    }
}
