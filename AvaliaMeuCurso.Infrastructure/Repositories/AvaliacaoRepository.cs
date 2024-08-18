using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using AvaliaMeuCurso.Domain.Entities;
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
            const string sql = @"INSERT INTO Avaliacoes (Estrelas, Comentario, DataHora, CursoId, EstudanteId) 
                                 VALUES (@Estrelas, @Comentario, @DataHora, @CursoId, @EstudanteId); 
                                 SELECT LAST_INSERT_ID();";

            using var db = CriarConexaoBancoDeDados;
            var novoIdAvaliacao = await db.ExecuteScalarAsync<int>(sql, avaliacao);
            avaliacao.Id = novoIdAvaliacao;
            return avaliacao;
        }

        public async Task<bool> AtualizarAvaliacao(Avaliacao avaliacao, int avaliacaoId)
        {
            const string sql = @"UPDATE Avaliacoes 
                                 SET Estrelas = @Estrelas, Comentario = @Comentario, DataHora = @DataHora, 
                                     CursoId = @CursoId, EstudanteId = @EstudanteId 
                                 WHERE Id = @Id";

            using var db = CriarConexaoBancoDeDados;
            var avaliacaoFoiAtualizada = await db.ExecuteAsync(sql, new
            {
                avaliacao.Estrelas,
                avaliacao.Comentario,
                avaliacao.DataHora,
                avaliacao.CursoId,
                avaliacao.EstudanteId,
                Id = avaliacaoId
            });

            return avaliacaoFoiAtualizada > 0;
        }

        public async Task<Avaliacao> BuscarAvaliacaoPorId(int avaliacaoId)
        {
            const string sql = @"SELECT * FROM Avaliacoes WHERE Id = @Id";

            using var db = CriarConexaoBancoDeDados;
            return await db.QueryFirstOrDefaultAsync<Avaliacao>(sql, new { Id = avaliacaoId });
        }

        public async Task<bool> ExcluirAvaliacao(int avaliacaoId)
        {
            const string sql = @"DELETE FROM Avaliacoes WHERE Id = @Id";

            using var db = CriarConexaoBancoDeDados;
            var avaliacaoFoiExcluida = await db.ExecuteAsync(sql, new { Id = avaliacaoId });
            return avaliacaoFoiExcluida > 0;
        }

        public async Task<IEnumerable<Avaliacao>> BuscarTodasAvaliacoes()
        {
            const string sql = @"SELECT * FROM Avaliacoes ORDER BY Id DESC";

            using var db = CriarConexaoBancoDeDados;
            return await db.QueryAsync<Avaliacao>(sql);
        }
    }
}
