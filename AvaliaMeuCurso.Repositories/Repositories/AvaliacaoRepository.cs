using Dapper;
using System.Data;
using AvaliaMeuCurso.Models;
using MySql.Data.MySqlClient;
using AvaliaMeuCurso.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AvaliaMeuCurso.Infrastructure.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AvaliacaoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        private IDbConnection CriarConexaoBancoDeDados => new MySqlConnection(_connectionString);

        public async Task<Avaliacao> CriarNovaAvaliacao(Avaliacao avaliacao)
        {
            string sql = @"INSERT INTO Avaliacoes (Estrelas, Comentario, DataHora, CursoId, EstudanteId) VALUES (@Estrelas, @Comentario, @DataHora, @CursoId, @EstudanteId); SELECT LAST_INSERT_ID();";
            using var db = CriarConexaoBancoDeDados;
            var novoIdAvaliacao = await db.ExecuteScalarAsync<int>(sql, avaliacao);
            avaliacao.Id = novoIdAvaliacao;
            return avaliacao;
        }

        public async Task<bool> AtualizarAvaliacao(Avaliacao avaliacao, int avaliacaoId)
        {
            avaliacao.Id = avaliacaoId;
            string sql = @"UPDATE Avaliacoes SET Estrelas = @Estrelas, Comentario = @Comentario, DataHora = @DataHora, CursoId = @CursoId, EstudanteId = @EstudanteId WHERE Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            var avaliacaoFoiAtualizada = await db.ExecuteAsync(sql, avaliacao);
            return avaliacaoFoiAtualizada > 0;
        }

        public async Task<Avaliacao> BuscarAvaliacaoPorId(int avaliacaoId)
        {
            string sql = @"SELECT * FROM Avaliacoes WHERE Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            return await db.QueryFirstOrDefaultAsync<Avaliacao>(sql, new { Id = avaliacaoId });
        }

        public async Task<bool> ExcluirAvaliacao(int avaliacaoId)
        {
            string sql = @"DELETE FROM Avaliacoes WHERE Id = @Id";
            using var db = CriarConexaoBancoDeDados;
            var avaliacaoFoiExcluida = await db.ExecuteAsync(sql, new { Id = avaliacaoId });
            return avaliacaoFoiExcluida > 0;
        }

        public async Task<IEnumerable<Avaliacao>> BuscarTodasAvaliacoes()
        {
            string sql = @"SELECT * FROM Avaliacoes";
            using var db = CriarConexaoBancoDeDados;
            return await db.QueryAsync<Avaliacao>(sql);
        }
    }
}
