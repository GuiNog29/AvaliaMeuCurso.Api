using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using AvaliaMeuCurso.Domain.Entities;
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
            const string sql = @"INSERT INTO Estudantes (Nome, Email) VALUES (@Nome, @Email); 
                                 SELECT LAST_INSERT_ID();";

            using var db = CriarConexaoBancoDeDados;
            estudante.Id = await db.ExecuteScalarAsync<int>(sql, estudante);
            return estudante;
        }

        public async Task<bool> AtualizarEstudante(Estudante estudante, int estudanteId)
        {
            estudante.Id = estudanteId;
            const string sql = @"UPDATE Estudantes SET Nome = @Nome, Email = @Email WHERE Id = @Id";

            using var db = CriarConexaoBancoDeDados;
            var resultado = await db.ExecuteAsync(sql, estudante);
            return resultado > 0;
        }

        public async Task<Estudante> BuscarEstudantePorId(int estudanteId)
        {
            const string sql = @"SELECT * FROM Estudantes WHERE Id = @Id";

            using var db = CriarConexaoBancoDeDados;
            return await db.QueryFirstOrDefaultAsync<Estudante>(sql, new { Id = estudanteId });
        }

        public async Task<bool> ExcluirEstudante(int estudanteId)
        {
            const string sql = @"DELETE FROM Estudantes WHERE Id = @Id";

            using var db = CriarConexaoBancoDeDados;
            var resultado = await db.ExecuteAsync(sql, new { Id = estudanteId });
            return resultado > 0;
        }

        public async Task<IEnumerable<Estudante>> BuscarTodosEstudantes()
        {
            const string sql = @"SELECT * FROM Estudantes ORDER BY Nome";

            using var db = CriarConexaoBancoDeDados;
            return await db.QueryAsync<Estudante>(sql);
        }

        public async Task<Estudante> BuscarEstudantePorEmail(string email)
        {
            const string sql = @"SELECT * FROM Estudantes WHERE Email = @Email;";

            using var db = CriarConexaoBancoDeDados;
            return await db.QueryFirstOrDefaultAsync<Estudante>(sql, new { Email = email });
        }

    }
}
