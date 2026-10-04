# 💰 Carteira Digital

Aplicação de carteira digital desenvolvida em **C#/.NET**, utilizando **Arquitetura Hexagonal (Ports and Adapters)** como principal abordagem arquitetural.

O projeto tem como objetivo implementar operações financeiras de forma **desacoplada, testável e orientada às regras de negócio**, explorando na prática conceitos de arquitetura de software e desenvolvimento backend.

> 🚧 **Status:** Em desenvolvimento  
> 📚 **Propósito:** Projeto de estudo e portfólio

---

## 🎯 Sobre o projeto

A **Carteira Digital** simula uma aplicação capaz de gerenciar carteiras e realizar operações financeiras entre clientes.

O sistema será desenvolvido de forma incremental, começando pelas regras essenciais do domínio e evoluindo posteriormente para persistência, testes de integração, mensageria, containers e observabilidade.

O principal objetivo não é apenas construir uma API funcional, mas aplicar na prática princípios de **Arquitetura Hexagonal**, mantendo o núcleo da aplicação independente de detalhes de infraestrutura.

### Principais objetivos

- Aplicar Arquitetura Hexagonal na prática
- Separar regras de negócio de detalhes de infraestrutura
- Desenvolver casos de uso de forma desacoplada
- Criar uma aplicação testável
- Trabalhar com Ports and Adapters
- Aplicar princípios SOLID
- Explorar testes unitários e de integração
- Evoluir a aplicação para cenários de mensageria e integração

---

## 💼 Funcionalidades

### Carteiras

- [ ] Criar carteira
- [ ] Consultar carteira
- [ ] Consultar saldo
- [ ] Ativar carteira
- [ ] Inativar carteira

### Operações financeiras

- [ ] Realizar depósito
- [ ] Realizar saque
- [ ] Realizar transferência entre carteiras
- [ ] Consultar histórico de transações

### Regras de negócio

- [ ] Validar valor de depósito
- [ ] Impedir saque superior ao saldo disponível
- [ ] Impedir operações em carteira inativa
- [ ] Validar carteira de destino
- [ ] Impedir transferência para a própria carteira
- [ ] Garantir consistência das movimentações financeiras

> As regras serão definidas e implementadas progressivamente conforme a evolução do domínio.

---

# 🏗️ Arquitetura

O projeto utiliza **Arquitetura Hexagonal**, também conhecida como **Ports and Adapters**.

O objetivo é manter o **Domain no centro da aplicação**, isolando as regras de negócio de frameworks, banco de dados, protocolos de comunicação e serviços externos.

```text
                         ┌─────────────────────┐
                         │       Cliente       │
                         └──────────┬──────────┘
                                    │
                                    ▼
                         ┌─────────────────────┐
                         │         API         │
                         │     Adapter IN      │
                         └──────────┬──────────┘
                                    │
                                    ▼
                         ┌─────────────────────┐
                         │     Application     │
                         │       Use Cases     │
                         └──────────┬──────────┘
                                    │
                                    ▼
                         ┌─────────────────────┐
                         │       Domain        │
                         │   Regras de negócio │
                         └──────────┬──────────┘
                                    ▲
                                    │
                         ┌──────────┴──────────┐
                         │   Infrastructure    │
                         │     Adapters OUT    │
                         └───────┬─────┬───────┘
                                 │     │
                                 ▼     ▼
                              Oracle  Kafka
```

## 🔑 Princípio arquitetural

O **Domain** representa o núcleo do sistema e não deve depender de tecnologias externas.

Por exemplo, as regras de negócio não precisam saber se a aplicação utiliza:

- Oracle
- Entity Framework Core
- Kafka
- HTTP
- ASP.NET Core
- qualquer outro mecanismo de infraestrutura

Esses detalhes são implementados nos **Adapters**.

Dessa forma, o sistema permanece preparado para substituir tecnologias sem alterar o núcleo da aplicação.

---

# 📂 Estrutura da solução

```text
CarteiraDigital
│
├── README.md
├── CarteiraDigital.sln
│
├── CarteiraDigital.Domain
│
├── CarteiraDigital.Application
│
├── CarteiraDigital.Infrastructure
│
└── CarteiraDigital.Api
```

## Domain

Camada responsável pelo **núcleo do negócio**.

Aqui estarão as entidades, objetos de valor, exceções e regras que representam o comportamento da carteira digital.

Exemplo de organização:

```text
CarteiraDigital.Domain
├── Entities
├── ValueObjects
├── Exceptions
└── ...
```

O Domain deve permanecer independente de infraestrutura e frameworks externos.

---

## Application

Camada responsável pelos **casos de uso da aplicação**.

A Application coordena o fluxo das operações e utiliza o Domain para executar as regras de negócio.

Exemplo:

```text
CarteiraDigital.Application
├── UseCases
├── Ports
├── DTOs
└── ...
```

Exemplos de casos de uso:

```text
CreateWallet
Deposit
Withdraw
Transfer
GetBalance
GetTransactionHistory
```

---

## Infrastructure

Camada responsável pelas implementações dos adapters externos.

Exemplo:

```text
CarteiraDigital.Infrastructure
├── Persistence
├── Repositories
├── Messaging
└── ...
```

Nesta camada poderão estar:

- Entity Framework Core
- Oracle
- Kafka
- Implementações de repositories
- Integrações com serviços externos

---

## API

Camada responsável pela comunicação HTTP com o sistema.

A API representa um **Adapter de entrada (Inbound Adapter)**.

Exemplo:

```text
CarteiraDigital.Api
├── Controllers
├── Middlewares
└── Program.cs
```

Responsabilidades:

- Receber requisições HTTP
- Validar entrada
- Acionar casos de uso
- Retornar respostas HTTP

A API não deve concentrar as regras de negócio da aplicação.

---

# 🔌 Ports and Adapters

Uma das principais finalidades deste projeto é compreender na prática a relação entre **Ports** e **Adapters**.

Exemplo de persistência:

```text
                 Application
                      │
                      ▼
             IWalletRepository
                    Port
                      ▲
                      │
                      │ implementação
                      │
             OracleWalletRepository
                    Adapter
                      │
                      ▼
                   Oracle
```

A aplicação conhece apenas o contrato:

```csharp
IWalletRepository
```

A implementação concreta fica em Infrastructure:

```text
OracleWalletRepository
```

Isso permite substituir o mecanismo de persistência sem alterar as regras centrais da aplicação.

---

# 🧪 Estratégia de testes

O projeto terá diferentes níveis de testes.

## Testes unitários

Principalmente utilizados para validar regras do **Domain** e comportamentos isolados da aplicação.

Exemplo:

```text
Depositar R$ 100
        ↓
Saldo anterior: R$ 0
        ↓
Saldo esperado: R$ 100
```

Cenário inválido:

```text
Sacar R$ 150
        ↓
Saldo disponível: R$ 100
        ↓
Operação rejeitada
```

Esses testes devem executar sem depender de Oracle, Kafka ou serviços externos.

---

## Testes de integração

Serão utilizados para validar a integração entre componentes reais.

Exemplo:

```text
Application
     ↓
Repository
     ↓
Oracle
```

Também poderão validar integrações como:

```text
Application
     ↓
Kafka Adapter
     ↓
Kafka
```

---

## Testes de API

Em uma etapa posterior, serão adicionados testes que exercitam a aplicação através de HTTP.

Exemplo:

```text
POST /api/wallets
        ↓
API
        ↓
Application
        ↓
Domain
        ↓
Infrastructure
        ↓
Oracle
```

---

# 🛠️ Tecnologias

### Principais

- **C#**
- **.NET**
- **ASP.NET Core**
- **Oracle Database**
- **Entity Framework Core**
- **xUnit**
- **Git**

### Tecnologias planejadas

- Docker
- Docker Compose
- Apache Kafka
- MediatR
- FluentValidation
- Swagger / OpenAPI
- Observabilidade
- Logs
- Métricas

---

# 🗺️ Roadmap

## Fase 1 — Estrutura

- [x] Criar solução
- [x] Criar projeto Domain
- [x] Criar projeto Application
- [x] Criar projeto Infrastructure
- [x] Criar projeto API
- [ ] Configurar referências entre projetos
- [ ] Definir estrutura inicial

## Fase 2 — Domain

- [ ] Criar entidade Wallet
- [ ] Criar entidade Transaction
- [ ] Definir regras de negócio
- [ ] Criar Value Objects
- [ ] Criar exceções de domínio
- [ ] Implementar operações financeiras
- [ ] Criar testes unitários

## Fase 3 — Application

- [ ] Criar casos de uso
- [ ] Criar Ports
- [ ] Criar DTOs
- [ ] Implementar orquestração dos casos de uso
- [ ] Criar testes da Application

## Fase 4 — Infrastructure

- [ ] Configurar Oracle
- [ ] Configurar Entity Framework Core
- [ ] Criar mappings
- [ ] Implementar repositories
- [ ] Criar testes de integração

## Fase 5 — API

- [ ] Criar endpoints
- [ ] Configurar Swagger / OpenAPI
- [ ] Implementar validações
- [ ] Implementar tratamento global de exceções
- [ ] Criar testes de API

## Fase 6 — Mensageria

- [ ] Publicar eventos de domínio
- [ ] Integrar Kafka
- [ ] Criar Producer
- [ ] Criar Consumer
- [ ] Persistir eventos processados

## Fase 7 — Infraestrutura e observabilidade

- [ ] Docker
- [ ] Docker Compose
- [ ] Logs estruturados
- [ ] Métricas
- [ ] Observabilidade
- [ ] Health Checks

---

# 📚 Conceitos explorados

Durante o desenvolvimento, serão explorados conceitos como:

- Arquitetura Hexagonal
- Ports and Adapters
- Domain-Driven Design
- SOLID
- Dependency Inversion Principle
- Dependency Injection
- Clean Code
- Design Patterns
- Testes unitários
- Testes de integração
- Testes de API
- APIs REST
- Persistência de dados
- Oracle Database
- Mensageria
- Event-Driven Architecture
- Docker
- Observabilidade

---

# 🚀 Evolução do projeto

O projeto será desenvolvido de maneira incremental.

A intenção é começar com um núcleo pequeno e funcional e, posteriormente, adicionar novas capacidades sem comprometer o desacoplamento da arquitetura.

A evolução planejada segue aproximadamente:

```text
Domain
  ↓
Application
  ↓
API
  ↓
Oracle
  ↓
Testes de Integração
  ↓
Kafka
  ↓
Docker
  ↓
Observabilidade
```

Cada etapa será utilizada como oportunidade para estudar e aplicar novos conceitos de arquitetura e desenvolvimento backend.

---

# 🎓 Objetivo de aprendizado

Este projeto é um laboratório prático para desenvolver a capacidade de:

- Identificar regras de negócio
- Modelar um domínio
- Criar casos de uso
- Definir Ports
- Implementar Adapters
- Manter o núcleo da aplicação desacoplado
- Escrever testes automatizados
- Trabalhar com persistência e mensageria
- Evoluir uma aplicação sem acoplá-la à infraestrutura

> **O objetivo final é aprender a projetar software, e não apenas implementar funcionalidades.**

