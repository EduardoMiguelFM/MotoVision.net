# 🚀 Mottu API - Sistema de Gestão de Compartilhamento de Motos

API RESTful desenvolvida em .NET 8 para gestão da startup Mottu, uma empresa de compartilhamento de motos.

## 👥 Integrantes da Equipe

- **Eduardo Miguel Forato Monteiro** – RM 555871
- **Cícero Gabriel Oliveira Serafim** – RM 556996
- **Murillo Ari Ferreira Sant'Anna** – RM 557183

## 🏗️ Arquitetura do Projeto

O projeto segue uma arquitetura em camadas (Clean Architecture) com as seguintes estruturas:

```
Mottu/
├── Domain/                 # Entidades e Regras de Negócio
│   ├── Entities/          # Moto, Patio, Usuario
│   ├── Enums/            # StatusMoto
│   └── ValueObjects/     # Placa, SetorCor
├── Application/           # DTOs, Interfaces e Mapeamentos
│   ├── DTOs/             # DTOs de entrada e saída
│   ├── Interfaces/       # Contratos dos repositórios
│   └── Mapping/          # Configurações do AutoMapper
├── Infrastructure/        # Acesso a Dados
│   ├── Data/             # DbContext e Configurações
│   └── Repositories/     # Implementação dos repositórios
└── API/                  # Camada de Apresentação
    ├── Controllers/      # Controllers REST
    ├── Services/         # Serviços de aplicação
    ├── Middleware/       # Middlewares customizados
    └── Validations/      # Validações com FluentValidation
```

## 🎯 Funcionalidades Principais

### 🏍️ Gestão de Motos

- CRUD completo de motos
- Sistema automático de setores e cores baseado no status
- Filtros avançados (status, setor, cor)
- Busca por placa
- Paginação em todas as listagens
- Contagem de motos por setor

### 🏢 Gestão de Pátios

- CRUD completo de pátios
- Status geral do pátio com estatísticas
- Controle de endereços

### 👤 Gestão de Usuários

- CRUD completo de usuários
- Controle de funções e permissões

## 🔄 Sistema de Status Automático

O sistema define automaticamente o setor e cor da moto baseado no status:

| Status            | Setor   | Cor      |
| ----------------- | ------- | -------- |
| DISPONIVEL        | Setor A | Verde    |
| RESERVADA         | Setor B | Azul     |
| MANUTENCAO        | Setor C | Amarelo  |
| FALTA_PECA        | Setor D | Laranja  |
| INDISPONIVEL      | Setor E | Cinza    |
| DANOS_ESTRUTURAIS | Setor F | Vermelho |
| SINISTRO          | Setor G | Preto    |

## 🚀 Como Executar

### Pré-requisitos

- .NET 8 SDK
- PostgreSQL 12+ (configurado no appsettings.json)

### Configuração do Banco de Dados

1. **Instalar PostgreSQL** (se não estiver instalado)
2. **Criar o banco de dados**:
   ```sql
   CREATE DATABASE "MotoVisionNet";
   ```
3. **Configurar a connection string** no `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "PostgreSQL": "Host=localhost;Port=5432;Database=MotoVisionNet;Username=postgres;Password=dudu0602"
     }
   }
   ```

### Comandos de Execução

```bash
# Restaurar dependências
dotnet restore

# Executar migrações
dotnet ef database update

# Executar API
dotnet run

# Executar testes (se houver)
dotnet test
```

### Acessar a API

- **Swagger UI**: http://localhost:8080/swagger
- **API Base URL**: http://localhost:8080/api

## 📋 Endpoints Principais

### Motos

- `GET /api/motos` - Lista todas (com paginação)
- `GET /api/motos/{id}` - Busca por ID
- `GET /api/motos/placa/{placa}` - Busca por placa
- `GET /api/motos/status/{status}` - Filtra por status
- `GET /api/motos/filtro?status=&setor=&cor=` - Filtro avançado
- `POST /api/motos` - Criar moto
- `PUT /api/motos/{id}` - Atualizar moto
- `DELETE /api/motos/{id}` - Deletar moto

### Pátios

- `GET /api/patios` - Lista todos
- `GET /api/patios/{id}` - Busca por ID
- `GET /api/patios/{id}/status` - Status geral do pátio
- `POST /api/patios` - Criar pátio
- `PUT /api/patios/{id}` - Atualizar pátio
- `DELETE /api/patios/{id}` - Deletar pátio

### Usuários

- `GET /api/usuarios` - Lista todos
- `GET /api/usuarios/{id}` - Busca por ID
- `POST /api/usuarios` - Criar usuário
- `PUT /api/usuarios/{id}` - Atualizar usuário
- `DELETE /api/usuarios/{id}` - Deletar usuário

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Core Web API** 8.0
- **Entity Framework Core** 8.0.8 (Code First)
- **PostgreSQL** 12+ com Npgsql
- **AutoMapper** 12.0.1 para mapeamento de DTOs
- **FluentValidation** 11.3.1 para validações
- **Swagger/OpenAPI** 8.1.2 para documentação interativa
- **Repository Pattern** + Service Layer
- **Data Annotations** para validações de modelo

## 📊 Dados de Teste

### Pátios

- Pátio Butantã - Rua das Flores, 123
- Pátio Vila Madalena - Av. Paulista, 456
- Pátio Pinheiros - Rua Augusta, 789

### Usuários

- Admin - admin@mottu.com.br
- Supervisor - supervisor@mottu.com.br
- Operador - operador@mottu.com.br

### Motos (exemplos)

- Honda Biz - ABC1234 - DISPONIVEL
- Yamaha Factor - DEF5678 - MANUTENCAO
- Honda CG - GHI9012 - RESERVADA

## 📝 Documentação

A documentação completa da API está disponível através do Swagger UI, incluindo:

- **Descrição detalhada** de todos os endpoints
- **Exemplos de payloads** para POST/PUT com validações
- **Modelos de dados documentados** com comentários XML
- **Parâmetros de query documentados** com tipos e descrições
- **Responses com exemplos** e códigos de status HTTP
- **Sistema de validação** com mensagens em português
- **Informações da equipe** e regras de negócio

### 🎯 Melhorias Implementadas

- ✅ **Documentação XML completa** para todas as entidades e DTOs
- ✅ **Validações robustas** com Data Annotations e FluentValidation
- ✅ **Swagger interativo** com exemplos de request/response
- ✅ **Sistema de status automático** documentado visualmente
- ✅ **Migração para PostgreSQL** com configuração otimizada
- ✅ **Dados de teste** pré-carregados no banco

## 🗄️ Estrutura do Banco de Dados

### Tabelas Principais

| Tabela           | Descrição                | Campos Principais                               |
| ---------------- | ------------------------ | ----------------------------------------------- |
| `motos`          | Motos do sistema         | id, modelo, placa, status, setor, cor, patio_id |
| `patios`         | Pátios de estacionamento | id, nome, endereco                              |
| `usuarios`       | Usuários do sistema      | id, nome, email, senha, cpf, funcao             |
| `usuarios_patio` | Usuários por pátio       | id, nome, email, funcao, patio_id               |

### Relacionamentos

- **Moto** → **Patio** (Many-to-One): Uma moto pertence a um pátio
- **UsuarioPatio** → **Patio** (Many-to-One): Um usuário pode trabalhar em um pátio

### Campos Calculados Automaticamente

- **Setor e Cor**: Calculados baseado no status da moto
- **Status**: Enum com 7 valores possíveis
- **Validações**: Aplicadas via Data Annotations e FluentValidation

## 🏆 Critérios de Avaliação Atendidos

✅ **25 pts - 3 Entidades Principais**: Moto, Pátio, Usuário com domínio justificado
✅ **50 pts - Endpoints CRUD**: CRUD completo + endpoints especiais + paginação + status codes
✅ **15 pts - Swagger/OpenAPI**: Documentação completa com exemplos e descrições
✅ **10 pts - Repositório GitHub**: README.md com integrantes, arquitetura, instruções e exemplos

## 💡 Exemplos de Uso

### Criar uma Nova Moto

```bash
POST /api/motos
Content-Type: application/json

{
  "modelo": "Honda Biz",
  "placa": "ABC1234",
  "status": "DISPONIVEL",
  "patioId": 1
}
```

### Buscar Motos por Status

```bash
GET /api/motos/status/DISPONIVEL
```

### Filtrar Motos Avançado

```bash
GET /api/motos/filtro?status=MANUTENCAO&setor=Setor C&cor=Amarelo
```

### Obter Status de um Pátio

```bash
GET /api/patios/1/status
```

### Criar um Novo Pátio

```bash
POST /api/patios
Content-Type: application/json

{
  "nome": "Pátio Centro",
  "endereco": "Rua das Flores, 123 - Centro"
}
```

### Criar um Usuário

```bash
POST /api/usuarios
Content-Type: application/json

{
  "nome": "João Silva",
  "email": "joao@mottu.com.br",
  "senha": "senha123",
  "cpf": "123.456.789-00",
  "funcao": "Operador"
}
```

## 🔧 Troubleshooting

### Problemas Comuns

1. **Erro de conexão com PostgreSQL**:

   - Verifique se o PostgreSQL está rodando
   - Confirme as credenciais no `appsettings.json`
   - Certifique-se de que o banco `MotoVisionNet` existe

2. **Erro de migration**:

   ```bash
   dotnet ef database update --force
   ```

3. **Porta já em uso**:
   - A aplicação usa a porta 8080 por padrão
   - Verifique se não há outro processo usando a porta

### Logs e Debug

- Os logs da aplicação aparecem no console
- Use `dotnet run --verbosity normal` para mais detalhes
- Verifique o Swagger UI para testar os endpoints
