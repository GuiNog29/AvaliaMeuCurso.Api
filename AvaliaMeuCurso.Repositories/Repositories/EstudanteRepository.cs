using Dapper;
using System.Data;
using AvaliaMeuCurso.Models;
using MySql.Data.MySqlClient;
using AvaliaMeuCurso.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AvaliaMeuCurso.Infrastructure.Repositories
{
    public class EstudanteRepository : IEstudanteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EstudanteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        private IDbConnection CriarConexaoBancoDeDados => new MySqlConnection(_connectionString);

        public async Task<Estudante> CriarNovoEstudante(Estudante estudante)
        {
            string sql = @"INSERT INTO Estudantes (Nome, Email) VALUES (@Nome, @Email); SELECT LAST_INSERT_ID();";
            using var db = CriarConexaoBancoDeDados;
            var novoIdEstudante = await db.ExecuteScalarAsync<int>(sql, estudante);
            estudante.Id = novoIdEstudante;
            return estudante;
        }

        public async Task<bool> AtualizarEstudante(Estudante estudante, int estudanteId)
        {
            estudante.Id = estudanteId;
            string sql = @"UPDATE Estudantes SET Nome = @Nome, Email = @Email WHERE Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            var estudanteFoiAtualizado = await db.ExecuteAsync(sql, estudante);
            return estudanteFoiAtualizado > 0;
        }

        public async Task<Estudante> BuscarEstudantePorId(int estudanteId)
        {
            string sql = @"SELECT * FROM Estudantes E WHERE E.Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            return await db.QueryFirstOrDefaultAsync<Estudante>(sql, new { Id = estudanteId });
        }

        public async Task<bool> ExcluirEstudante(int estudanteId)
        {
            string sql = @"DELETE FROM Estudantes WHERE Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            var estudanteFoiExcluido = await db.ExecuteAsync(sql, new { Id = estudanteId });
            return estudanteFoiExcluido > 0;
        }

        public async Task<IEnumerable<Estudante>> BuscarTodosEstudantes()
        {
            string sql = @"SELECT * FROM Estudantes E ORDER BY E.Nome";
            using var db = CriarConexaoBancoDeDados;
            return await db.QueryAsync<Estudante>(sql);
        }
    }
}
