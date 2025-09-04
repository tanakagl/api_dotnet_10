# GraphQL com Clean Architecture - Guia de Organização

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

## 🏗️ Estrutura do Projeto

```
WebApi/
├── Graphql/
│   ├── Queries/           # 📖 Operações de leitura
│   │   └── UserQuery.cs
│   ├── Mutations/         # ✏️ Operações de escrita
│   │   └── UserMutation.cs
│   ├── Subscriptions/     # 🔔 Operações em tempo real
│   │   └── UserSubscription.cs
│   └── Types/            # 📝 Definições de tipos
│       ├── Inputs/       # ⬇️ Dados de entrada
│       │   └── CreateUserInput.cs
│       └── Outputs/      # ⬆️ Dados de saída
│           └── UserType.cs
```

## 🎯 Clean Architecture - Responsabilidades

### **1. WebApi Layer (Apresentação)**
- **Queries**: Definem como buscar dados
- **Mutations**: Definem como modificar dados
- **Types**: Definem estrutura de entrada/saída
- **Resolvers**: Conectam GraphQL com Application Layer

### **2. Application Layer (Casos de Uso)**
- **Services**: Lógica de negócio (`GetAllUsers`, `CreateUser`)
- **Interfaces**: Contratos para Infrastructure

### **3. Domain Layer (Entidades)**
- **Entities**: Modelos de domínio (`User`, `EntityBase`)

### **4. Infrastructure Layer (Dados)**
- **Repositories**: Acesso a dados
- **Context**: EF Core DbContext

## 📝 Exemplo Prático - Criar Usuário

### **1. Input Type (Entrada)**
```csharp
public record CreateUserInput(string Name, string Email);
```

### **2. Mutation (GraphQL)**
```csharp
public class UserMutation
{
    public async Task<User> AddUser(
        CreateUserInput input,
        [Service] CreateUser createUser)
    {
        return await createUser.ExecuteAsync(input.Name, input.Email);
    }
}
```

### **3. Service (Application)**
```csharp
public class CreateUser
{
    public async Task<User> ExecuteAsync(string name, string email)
    {
        var user = new User { Name = name, Email = email };
        return await _userRepository.CreateAsync(user);
    }
}
```

### **4. Query GraphQL Cliente**
```graphql
mutation {
  addUser(input: { name: "João", email: "joao@email.com" }) {
    id
    name
    email
    createdAt
  }
}
```

## 🔄 Fluxo de Dados

```
Cliente GraphQL
    ↓ (Mutation/Query)
WebApi/Graphql (Presentation)
    ↓ (Chama Service)
Application/Services (Use Cases)
    ↓ (Usa Repository)
Infrastructure/Repositories (Data Access)
    ↓ (Acessa Banco)
Database
```

## 🚀 Vantagens desta Organização

1. **Separação Clara**: Cada camada tem responsabilidade única
2. **Testabilidade**: Services podem ser testados independentemente
3. **Flexibilidade**: GraphQL permite queries específicas
4. **Reutilização**: Services podem ser usados em outros contextos
5. **Manutenibilidade**: Estrutura organizada e previsível

## 📊 Subscriptions - Dados em Tempo Real

Para dados que mudam frequentemente, use subscriptions:

```graphql
subscription {
  userCreated {
    id
    name
    email
  }
}
```

## 🛠️ Ferramentas de Desenvolvimento

- **Banana Cake Pop**: Interface gráfica para testar GraphQL (habilitada em desenvolvimento)
- **Schema Explorer**: Documentação automática dos tipos e operações
- **IntelliSense**: Autocompletar nas queries

## 📈 Próximos Passos

1. ✅ Implementar validações nos inputs
2. ✅ Adicionar tratamento de erros
3. ✅ Criar mais operações (Update, Delete)
4. ✅ Implementar paginação
5. ✅ Adicionar filtros nas queries
6. ✅ Configurar autenticação/autorização
