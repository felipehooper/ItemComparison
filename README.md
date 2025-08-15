🛠️ ItemComparison API
📌 Descrição

O ItemComparison API é um backend para comparação de produtos que segue princípios de POO, SOLID, Clean Architecture e Arquitetura Hexagonal.
Os dados são persistidos localmente em JSON, sem uso de banco de dados, e expostos via endpoints REST para:

Comparar itens por IDs.

Consultar o catálogo completo.

CRUD básico de produtos.

Retornar erros claros e padronizados.

🚀 Principais Tecnologias

.NET 8 (C#)

ASP.NET Core Web API

Dependency Injection (DI) nativo do .NET

Persistência em arquivo JSON

Swagger/OpenAPI para documentação

Testes automatizados com xUnit e Moq

🏛️ Arquitetura

A solução segue Clean Architecture com Arquitetura Hexagonal, separando responsabilidades em camadas:

Camadas

Domain:
Entidades, Value Objects, interfaces de repositório (Ports), regras de negócio e invariantes.

Application:
Casos de uso (Commands e Queries), DTOs, estratégias de ordenação (Strategy Pattern), fábricas (Factory Method).

Infrastructure:
Adapters concretos (Driven Adapters), persistência JSON, abstrações de storage (Abstract Factory), provedores Singleton de caminho de arquivos.

API (Interface):
Controllers REST, Facade Pattern para orquestração dos casos de uso.

📂 Estrutura de Pastas
ItemComparison/
├─ src/
│  ├─ ItemComparison.Api/                 # Interface (controllers, facade)
│  ├─ ItemComparison.Application/         # Casos de uso, DTOs, estratégias
│  ├─ ItemComparison.Domain/              # Entidades, VOs, Ports, regras
│  ├─ ItemComparison.Infrastructure/      # Persistência e serviços
│  └─ ItemComparison.Shared/              # Objetos e contratos comuns
├─ tests/
│  ├─ ItemComparison.Tests.Api/           # Testes de endpoints
│  └─ ItemComparison.Tests.Application/   # Testes de casos de uso
└─ data/
   └─ items.json                          # Dados persistidos

🔧 Padrões e Princípios Utilizados

POO (Encapsulamento, Abstração, Polimorfismo, Herança)

SOLID

DRY, KISS e YAGNI

Design Patterns:

Facade

Command / Query

Strategy

Factory Method

Abstract Factory

Singleton

Repository Pattern

Arquitetura Hexagonal (Ports & Adapters)

Clean Architecture

📦 Endpoints Disponíveis
Método	Endpoint	Descrição
GET	/api/items?ids=a,b,c	Compara produtos por IDs
GET	/api/catalog	Lista todo o catálogo
GET	/api/items/{id}	Obtém um produto pelo ID
PUT	/api/items	Cria/atualiza um produto
DELETE	/api/items/{id}	Remove um produto
▶️ Executando o Projeto
1️⃣ Clonar o repositório
git clone https://github.com/seuusuario/ItemComparison.git
cd ItemComparison

2️⃣ Restaurar dependências
dotnet restore

3️⃣ Executar a aplicação
dotnet run --project src/ItemComparison.Api/ItemComparison.Api.csproj

4️⃣ Acessar o Swagger
http://localhost:5000/swagger

🧪 Executando os Testes
dotnet test

📂 Persistência de Dados

Os produtos são armazenados em data/items.json.

Caso o arquivo não exista na primeira execução, será gerado automaticamente com dados de exemplo.

Escrita atômica (gera .tmp e move sobrescrevendo).

⚠️ Validações Importantes

Id: alfanumérico, hífen ou underscore (^[A-Za-z0-9\-_]+$).

Name: obrigatório, mínimo 2 caracteres.

Price: ≥ 0.

Rating: entre 0 e 5.

Especificações (Specs): chave não pode ser vazia.

🛠️ Extensibilidade

Fácil substituição da camada de persistência (ex.: CSV, banco de dados) usando Abstract Factory.

Estratégias de ordenação configuráveis via Strategy Pattern.

Facilidade de adicionar novos endpoints ou integrações sem quebrar o domínio.