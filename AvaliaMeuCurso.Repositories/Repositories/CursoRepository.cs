﻿using Dapper;
using System.Data;
using AvaliaMeuCurso.Models;
using MySql.Data.MySqlClient;
using AvaliaMeuCurso.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AvaliaMeuCurso.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CursoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        private IDbConnection CriarConexaoBancoDeDados => new MySqlConnection(_connectionString);

        public async Task<Curso> CriarNovoCurso(Curso curso)
        {
            string sql = @"INSERT INTO Cursos (Nome, Descricao) VALUES (@Nome, @Descricao); SELECT LAST_INSERT_ID();";
            using var db = CriarConexaoBancoDeDados;
            var novoIdCurso = await db.ExecuteScalarAsync<int>(sql, curso);
            curso.Id = novoIdCurso;
            return curso;
        }

        public async Task<bool> AtualizarCurso(Curso curso)
        {
            string sql = @"UPDATE Cursos SET Nome = @Nome, Descricao = @Descricao WHERE Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            var cursoFoiAtualizado = await db.ExecuteAsync(sql, curso);
            return cursoFoiAtualizado > 0;
        }

        public async Task<Curso> BuscarCursoPorId(int cursoId)
        {
            string sql = @"SELECT * FROM Cursos WHERE Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            return await db.QueryFirstOrDefaultAsync<Curso>(sql, new { Id = cursoId });
        }

        public async Task<bool> ExcluirCurso(int cursoId)
        {
            string sql = @"DELETE FROM Cursos WHERE Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            var cursoFoiExcluido = await db.ExecuteAsync(sql, new { Id = cursoId });
            return cursoFoiExcluido > 0;
        }

        public async Task<IEnumerable<Curso>> BuscarTodosCursos()
        {
            string sql = @"SELECT * FROM Cursos";
            using var db = CriarConexaoBancoDeDados;
            return await db.QueryAsync<Curso>(sql);
        }
    }
}
