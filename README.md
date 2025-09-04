# 🚀 API .NET 10 com GraphQL & Docker

Uma API moderna construída com .NET 10, GraphQL, Clean Architecture e Docker.

## 📁 Estrutura Organizada

```
api_dotnet_10/
├── 📁 src/                    # Código fonte
│   ├── WebApi/               # Camada de apresentação
│   ├── Application/          # Casos de uso
│   ├── Domain/               # Entidades de domínio
│   └── Infrastructure/       # Acesso a dados
├── 📁 docker/                # Configurações Docker
│   ├── docker-compose.yml   # Orquestração de serviços
│   └── Dockerfile           # Imagem da aplicação
├── 📁 config/                # Configurações
│   └── .env.example         # Exemplo de variáveis
├── 📁 scripts/               # Scripts úteis
│   ├── start.sh            # Iniciar aplicação
│   ├── stop.sh             # Parar aplicação
│   └── clean.sh            # Limpeza Docker
├── .env                     # Variáveis de ambiente (local)
├── .gitignore              # Arquivos ignorados
└── README.md               # Esta documentação
```

## 🚀 Quick Start

### **1. Configurar Ambiente**
```bash
# Copiar variáveis de ambiente
cp config/.env.example .env

# Editar conforme necessário
nano .env
```

### **2. Executar com Docker**
```bash
# Iniciar tudo (API + Banco)
./scripts/start.sh

# Ou manualmente:
docker compose -f docker/docker-compose.yml up --build
```

### **3. Acessar Aplicação**
- **API GraphQL**: http://localhost:8080/graphql
- **Banana Cake Pop**: http://localhost:8080/graphql (interface visual)
- **Banco PostgreSQL**: localhost:5433

## 🛠️ Comandos Úteis

```bash
# Iniciar aplicação
./scripts/start.sh

# Parar aplicação  
./scripts/stop.sh

# Limpeza completa
./scripts/clean.sh

# Ou usar Docker diretamente
docker compose -f docker/docker-compose.yml up --build

# Logs em tempo real
docker compose -f docker/docker-compose.yml logs -f api
```

## 📋 Conceitos Fundamentais

### **GraphQL vs REST**

| Aspecto | REST | GraphQL |
|---------|------|---------|
| **Endpoints** | Múltiplos (`/api/users`, `/api/orders`) | Único (`/graphql`) |
| **Operações** | HTTP Verbs (GET, POST, PUT, DELETE) | Queries, Mutations, Subscriptions |
| **Dados** | Estrutura fixa por endpoint | Cliente escolhe campos específicos |
| **Requisições** | Múltiplas para dados relacionados | Uma requisição para múltiplas entidades |

### **Operações GraphQL**

1. **Queries** (📖 Leitura): Equivale ao GET em REST
2. **Mutations** (✏️ Escrita): Equivale ao POST/PUT/DELETE em REST
3. **Subscriptions** (🔔 Tempo Real): Para dados em tempo real (WebSockets)

## 🏗️ Clean Architecture

### **Responsabilidades por Camada**

#### **WebApi (Apresentação)**
- Queries/Mutations GraphQL
- Types e Inputs
- Resolvers

#### **Application (Casos de Uso)**  
- Services de negócio
- Interfaces para Infrastructure

#### **Domain (Entidades)**
- Models de domínio
- Regras de negócio

#### **Infrastructure (Dados)**
- Repositories
- DbContext (Entity Framework)

## � Docker

### **Multi-Stage Build**
- **Build Stage**: .NET SDK (compilação)  
- **Runtime Stage**: ASP.NET Runtime (execução)
- **Resultado**: Imagem otimizada (~200MB)

### **Serviços**
- **API**: .NET 10 aplicação
- **PostgreSQL**: Banco de dados
- **Volumes**: Persistência de dados
- **Network**: Comunicação entre containers

## 🔧 Configuração

### **Variáveis de Ambiente (.env)**
```bash
# Database
POSTGRES_DB=api_dotnet_10
POSTGRES_USER=postgres
POSTGRES_PASSWORD=123

# API
ASPNETCORE_ENVIRONMENT=Development
```

### **Portas**
- **API HTTP**: 8080
- **API HTTPS**: 8081  
- **PostgreSQL**: 5433

## 📝 Exemplo de Uso

### **Criar Usuário**
```graphql
mutation {
  addUser(input: { 
    name: "João Silva", 
    email: "joao@email.com" 
  }) {
    id
    name
    email
    createdAt
  }
}
```

### **Buscar Usuários**
```graphql
query {
  users {
    id
    name
    email
    lastModified
  }
}
```

## 🚀 Próximos Passos

- [ ] Implementar autenticação JWT
- [ ] Adicionar validações
- [ ] Criar testes unitários
- [ ] Configurar CI/CD
- [ ] Documentar API completa

## �️ Segurança

- Container roda com usuário não-root
- Health checks configurados
- Variáveis sensíveis em .env
- .env no .gitignore

---

**Desenvolvido com ❤️ usando .NET 10, GraphQL e Docker**
