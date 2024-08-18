# AvaliaMeuCurso

Este projeto foi desenvolvido para permitir que os alunos avaliem os cursos que participaram, oferecendo feedback valioso para a instituição. O sistema foi criado utilizando **Dapper** como ORM e **MySQL** como banco de dados.

## Visão Geral

O AvaliaMeuCurso é uma aplicação simples que permite aos alunos avaliarem cursos através de uma interface intuitiva. As avaliações incluem uma classificação em estrelas e um comentário opcional, que são armazenados no banco de dados para posterior análise.

## Estrutura do Banco de Dados

O banco de dados foi projetado para suportar as principais funcionalidades do sistema, com três tabelas principais:

- **Cursos**: Armazena informações sobre os cursos disponíveis.
- **Estudantes**: Contém dados dos estudantes que participam dos cursos.
- **Avaliações**: Registra as avaliações feitas pelos estudantes, incluindo a nota (em estrelas), o comentário (caso deseje adicionar) e a data/hora da avaliação.

### Scripts de Criação do Banco de Dados

Para criar o banco de dados e suas respectivas tabelas, siga a sequência de comandos abaixo:

```sql
-- Criação do Banco de Dados
CREATE DATABASE AvaliaMeuCursoDB;

-- Criação da Tabela Cursos
CREATE TABLE Cursos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Descricao TEXT NOT NULL
);

-- Criação da Tabela Estudantes
CREATE TABLE Estudantes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL
);

-- Criação da Tabela Avaliacoes com Relacionamentos e Exclusão em Cascata
CREATE TABLE Avaliacoes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Estrelas INT NOT NULL,
    Comentario TEXT,
    DataHora DATETIME NOT NULL,
    CursoId INT,
    EstudanteId INT,
    CONSTRAINT FK_Avaliacoes_Curso FOREIGN KEY (CursoId) REFERENCES Cursos(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Avaliacoes_Estudante FOREIGN KEY (EstudanteId) REFERENCES Estudantes(Id)
);
````

## Executando o Projeto

Para executar o projeto:

1. Baixe o repositório em sua máquina local.
2. Abra o projeto no seu ambiente de desenvolvimento preferido.
3. Configure a conexão com o banco de dados MySQL.
4. Ir no arquivo **appsettings.json** e mudar a senha que foi configurada no seu banco local, e caso necessário atualizar o usuário
5. Execute o projeto utilizando o perfil **AvaliaMeuCurso**.
