# MotoVision - ASP.NET Core com Oracle e EF Core

API RESTful desenvolvida em **.NET 8**, utilizando **Clean Architecture**, **DDD** e **EF Core com Oracle**, para gerenciar **Motos, Pátios e Usuários de Pátio**.  
Inclui **Swagger**, **AutoMapper**, **FluentValidation**, **Middleware de Erros** e **Value Objects** para regras de negócio.

---

## 🛠 Tecnologias Utilizadas
- ASP.NET Core 8.0
- Entity Framework Core + Oracle
- AutoMapper
- Swagger
- FluentValidation
- Clean Architecture + DDD

---

## 📂 Estrutura do Projeto
src
 ┣ 📂 Api             -> Controllers, Middleware, Validations, Program.cs
 ┣ 📂 Application     -> DTOs, Interfaces, UseCases, Mapping
 ┣ 📂 Domain          -> Entidades, Value Objects, Enums, Regras de Negócio
 ┗ 📂 Infrastructure  -> DbContext, Repositories, Migrations

- **Entidades Ricas**: Moto, Patio, UsuarioPatio  
- **Agregado Raiz**: Patio (gerencia coleção de Motos)  
- **Value Objects**: Placa (valida formato), SetorCor (define setor/cor pelo status)  
- **Enum**: StatusMoto (DISPONIVEL, RESERVADA, etc.)  
- **Middleware**: tratamento global de erros  
- **FluentValidation**: validação de DTOs  

---

## 📦 Entidades

### Moto
| Campo      | Tipo     | Descrição |
|------------|----------|-----------|
| Id         | int      | Identificador |
| Modelo     | string   | Modelo da moto |
| Placa      | VO       | Placa validada (7 caracteres) |
| Status     | enum     | DISPONIVEL, RESERVADA, etc. |
| Setor      | VO       | Definido pelo Status (A-G) |
| Cor        | VO       | Definido pelo Status |
| Patio      | ref      | Referência ao pátio |

### Patio
| Campo | Tipo   | Descrição |
|-------|--------|-----------|
| Id    | int    | Identificador |
| Nome  | string | Nome do pátio |
| Motos | lista  | Coleção de motos (Agregado Raiz) |

### UsuarioPatio
| Campo   | Tipo   | Descrição |
|---------|--------|-----------|
| Id      | int    | Identificador |
| Nome    | string | Nome do usuário |
| Email   | string | E-mail |
| Funcao  | string | Função |
| PatioId | int    | FK para Pátio |

---

## 🚀 Endpoints

### MotoController
- `GET /api/moto`
- `GET /api/moto/{id}`
- `POST /api/moto`
- `PUT /api/moto/{id}`
- `DELETE /api/moto/{id}`
- `PUT /api/moto/{id}/status/{novoStatus}`

### PatioController
- `GET /api/patio`
- `GET /api/patio/{id}`
- `POST /api/patio`
- `PUT /api/patio/{id}`
- `DELETE /api/patio/{id}`
- `GET /api/patio/setor/{setor}/contagem`
- `GET /api/patio/moto/{placa}/status`
- `GET /api/patio/status`

### UsuarioPatioController
- `GET /api/usuariopatio`
- `GET /api/usuariopatio/{id}`
- `POST /api/usuariopatio`
- `PUT /api/usuariopatio/{id}`
- `DELETE /api/usuariopatio/{id}`

---

## 🧠 Regras de Negócio
- Moto em **SINISTRO** não pode mudar de status.
- Placa deve ter exatamente 7 caracteres.
- Pátio não pode ter duas motos com a mesma placa.
- Setor e cor são derivados automaticamente do status.

---

## ⚙️ Execução

### 1. Configurar conexão Oracle ou Sql Server etc.
`appsettings.json`:
{
  "ConnectionStrings": {
    "OracleDb": "User Id=system;Password=oracle;Data Source=//localhost:1521/XEPDB1"
  }
}

### 2. Criar banco/tabelas
- DEV: `EnsureCreated()` cria automaticamente se vazio.
- Prod: usar migrations.

```bash
dotnet ef migrations add InitialCreate --startup-project src/Api --project src/Infrastructure
dotnet ef database update --startup-project src/Api --project src/Infrastructure
```

### 3. Executar a API
```bash
dotnet run --project src/Api
```

Swagger: `https://localhost:5001/swagger`

---

## 📌 Exemplo de JSON

### Criar Moto
```json
{
  "modelo": "Honda Biz",
  "placa": "ABC1234",
  "status": "DISPONIVEL",
  "nomePatio": "Pátio Butantã"
}
```

---

## 👥 Integrantes
- RM555871 – Eduardo Miguel Forato Monteiro  
- RM556996 – Cícero Gabriel Oliveira Serafim  
- RM557183 - MURILLO ARI FERREIRA SANT'ANNA

---

