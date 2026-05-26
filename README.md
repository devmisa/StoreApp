# StoreApp

Uma plataforma de processamento de pedidos em nível empresarial 
construída com .NET 10, demonstrando arquitetura de sistemas distribuídos 
com as melhores práticas em Vertical Slice Architecture (Arquitetura em Fatias Verticais).

## ✨ Funcionalidades

- **REST API** para gerenciamento de pedidos com documentação Swagger
- **Serviço Worker** assíncrono para processamento em background
- **PostgreSQL** com Entity Framework Core para persistência de dados
- **RabbitMQ** para comunicação baseada em eventos
- **.NET Aspire** para orquestração local de serviços
- **Testes Unitários** abrangentes com xUnit, Moq e FluentAssertions

## 🏗️ Arquitetura

- Separação clara de responsabilidades com estrutura baseada em features
- **FluentValidation** para validação robusta de dados
- **Serilog** para logging estruturado com suporte a OpenTelemetry
- **Tratamento de erros** de nível produção

## 🚀 Como Começar

### Pré-requisitos
- .NET 10 SDK
- Docker (para PostgreSQL e RabbitMQ)
- Visual Studio 2026 ou VS Code

### Instalação
- `git clone https://github.com/devmisa/StoreApp.git`
- `cd StoreApp`
- `dotnet run --project StoreApp.AppHost`

O dashboard do Aspire abrirá automaticamente no navegador.

## 🧪 Testes

### Executar todos os testes
- `dotnet test`

## 💡 Conceitos Principais

- **Arquitetura em Microsserviços**: API + Worker separados
- **Processamento Assíncrono**: Via RabbitMQ message queue
- **Database First**: Entity Framework Core com migrations
- **Desenvolvimento Local**: .NET Aspire simplifica configuração

## 🛠️ Stack Tecnológico

| Componente | Tecnologia |
|-----------|-----------|
| Runtime | .NET 10 |
| API Web | ASP.NET Core 10 |
| Banco de Dados | PostgreSQL + EF Core |
| Message Broker | RabbitMQ |
| Testes | xUnit, Moq, FluentAssertions |
| Logging | Serilog + OpenTelemetry |
| Orquestração | .NET Aspire |

## 🎓 Aprendizado

Este projeto é ideal para:
- Aprender arquitetura de microsserviços
- Implementar padrões de design em .NET
- Desenvolver habilidades com tecnologias enterprise
- Servir como base para projetos reais

## 📋 Próximos Passos

- [x] Testes de integração
- [x] Containerização com Docker
- [x] CI/CD pipeline (GitHub Actions)
- [x] Cobertura de testes >80%
- [x] Deploy em Kubernetes

## 📄 Licença

MIT License
