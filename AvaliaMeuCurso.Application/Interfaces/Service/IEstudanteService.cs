﻿using AvaliaMeuCurso.Application.Models.Estudante;

namespace AvaliaMeuCurso.Application.Interfaces.Service
{
    public interface IEstudanteService
    {
        Task<EstudanteModel> CriarNovoEstudante(EstudanteModel estudanteModel);
        Task<bool> AtualizarEstudante(EstudanteAtualizacaoModel estudanteModel, int estudanteId);
        Task<EstudanteModel> BuscarEstudantePorId(int estudanteId);
        Task<bool> ExcluirEstudante(int estudanteId);
        Task<IEnumerable<EstudanteModel>> BuscarTodosEstudantes();
    }
}
