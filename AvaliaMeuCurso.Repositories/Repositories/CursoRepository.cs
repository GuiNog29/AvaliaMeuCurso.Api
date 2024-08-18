using Dapper;
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
            const string sql = @"INSERT INTO Cursos (Nome, Descricao) VALUES (@Nome, @Descricao); SELECT LAST_INSERT_ID();";

            using var db = CriarConexaoBancoDeDados;
            curso.Id = await db.ExecuteScalarAsync<int>(sql, curso);
            return curso;
        }

        public async Task<bool> AtualizarCurso(Curso curso, int cursoId)
        {
            const string sql = @"UPDATE Cursos SET Nome = @Nome, Descricao = @Descricao WHERE Id = @Id";

            curso.Id = cursoId;
            using var db = CriarConexaoBancoDeDados;
            var cursoFoiAtualizado = await db.ExecuteAsync(sql, curso);
            return cursoFoiAtualizado > 0;
        }

        public async Task<Curso> BuscarCursoPorId(int cursoId)
        {
            const string sql = @"SELECT * FROM Cursos C WHERE C.Id = @Id ORDER BY C.Nome";

            using var db = CriarConexaoBancoDeDados;
            return await db.QueryFirstOrDefaultAsync<Curso>(sql, new { Id = cursoId });
        }

        public async Task<bool> ExcluirCurso(int cursoId)
        {
            const string sql = @"DELETE FROM Cursos WHERE Id = @Id";

            using var db = CriarConexaoBancoDeDados;
            var cursoFoiExcluido = await db.ExecuteAsync(sql, new { Id = cursoId });
            return cursoFoiExcluido > 0;
        }

        public async Task<IEnumerable<Curso>> BuscarTodosCursos()
        {
            const string sql = @"SELECT C.Id, C.Nome, C.Descricao, 
                        A.Id, A.Estrelas, A.Comentario, A.DataHora, 
                        E.Id, E.Nome, E.Email 
                        FROM Cursos C 
                        LEFT JOIN Avaliacoes A ON C.Id = A.CursoId
                        LEFT JOIN Estudantes E ON A.EstudanteId = E.Id
                        ORDER BY C.Nome";

            using var db = CriarConexaoBancoDeDados;

            var cursoDictionary = new Dictionary<int, Curso>();

            var cursos = await db.QueryAsync<Curso, Avaliacao, Estudante, Curso>(
            sql,
            (curso, avaliacao, estudante) =>
            {
                if (!cursoDictionary.TryGetValue(curso.Id, out var cursoAtual))
                {
                    cursoAtual = curso;
                    cursoAtual.Avaliacoes = new List<Avaliacao>();
                    cursoDictionary.Add(cursoAtual.Id, cursoAtual);
                }

                if (avaliacao != null)
                {
                    avaliacao.Estudante = estudante;
                    avaliacao.Curso = curso;
                    cursoAtual.Avaliacoes.Add(avaliacao);
                }

                return cursoAtual;
            },
            splitOn: "Id,Id,Id"
            );

            return cursoDictionary.Values.ToList();
        }
    }
}
