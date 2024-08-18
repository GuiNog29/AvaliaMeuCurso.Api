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

## Sugestões de implementações (Versões Futuras)

### 1. Implementação de Testes Unitários
   - **Descrição:** Adicionar testes unitários para todas as principais funcionalidades do sistema, garantindo que cada componente funcione conforme o esperado.
   - **Ferramentas Sugeridas:** xUnit, NUnit ou MSTest para C#.
   - **Benefício:** Aumenta a confiabilidade do código, facilitando a detecção de bugs e regressões.

### 2. Autenticação e Autorização
   - **Descrição:** Implementar um sistema de autenticação para garantir que apenas usuários autorizados possam acessar determinadas partes do sistema.
   - **Ferramentas Sugeridas:** ASP.NET Core Identity, JWT (JSON Web Tokens).
   - **Benefício:** Melhora a segurança do sistema, garantindo que apenas usuários autenticados possam acessar ou modificar dados.

### 3. Paginação e Filtros para Listagens
   - **Descrição:** Adicionar paginação e filtros às listagens de cursos, estudantes e avaliações, para melhorar a performance e a usabilidade do sistema.
   - **Ferramentas Sugeridas:** Implementação manual ou uso de bibliotecas específicas para paginação.
   - **Benefício:** Otimiza a performance do sistema, especialmente com grandes volumes de dados, e melhora a experiência do usuário.

### 4. Cache para Consultas Frequentes
   - **Descrição:** Implementar cache para consultas frequentes, como a listagem de cursos e avaliações, para melhorar a performance.
   - **Ferramentas Sugeridas:** Redis, MemoryCache.
   - **Benefício:** Reduz a carga no banco de dados e melhora o tempo de resposta para o usuário final.

### 5. Implementação de Logs e Monitoramento
   - **Descrição:** Adicionar logs para monitoramento de atividades e erros no sistema, com alertas em tempo real.
   - **Ferramentas Sugeridas:** Serilog, ELK Stack (Elasticsearch, Logstash, Kibana), Application Insights.
   - **Benefício:** Facilita a detecção e resolução de problemas em tempo real, melhorando a estabilidade e a manutenção do sistema.

### 6. Internacionalização (i18n)
   - **Descrição:** Adicionar suporte a múltiplos idiomas para permitir que o sistema seja utilizado em diferentes regiões.
   - **Ferramentas Sugeridas:** Recursos de internacionalização do ASP.NET Core.
   - **Benefício:** Expande o alcance do sistema para usuários que falam diferentes idiomas.

### 7. Interface Responsiva e Melhorias na UI/UX
   - **Descrição:** Melhorar a interface do usuário para torná-la mais intuitiva e responsiva em dispositivos móveis.
   - **Ferramentas Sugeridas:** Bootstrap, Tailwind CSS, ou desenvolvimento customizado com CSS.
   - **Benefício:** Melhora a experiência do usuário e torna o sistema acessível em uma maior variedade de dispositivos.

### 8. Migração para uma Arquitetura de Microsserviços
   - **Descrição:** Reestruturar o sistema para utilizar uma arquitetura de microsserviços, separando diferentes partes do sistema em serviços independentes.
   - **Ferramentas Sugeridas:** Docker, Kubernetes, Azure Service Fabric.
   - **Benefício:** Facilita a escalabilidade, manutenção e desenvolvimento independente de diferentes partes do sistema.
