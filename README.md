
# BoletosApi — Instruções de instalação, execução e testes

> Este README descreve **passo a passo** como preparar o ambiente, rodar a API `BoletosApi` (projeto .NET), aplicar migrations, testar via **Swagger** ou **Postman**, e resolver os problemas mais comuns encontrados durante os testes. Também inclui imagens ilustrativas da arquitetura e do fluxo de testes.

---
## Sumário
1. Requisitos
2. Estrutura do projeto
3. Configuração do ambiente
   - Opção A: PostgreSQL local (instalador)
   - Opção B: PostgreSQL via Docker
4. Configurar `appsettings.json`
5. Rodando a aplicação (Visual Studio 2022)
6. Migrations (Entity Framework Core)
7. Testes (Swagger e Postman) — passo a passo completo
8. Cenário de testes sugerido (JSONs)
9. Erros comuns e como corrigir
10. Observações finais

---
## 1. Requisitos
- .NET 6 SDK (ou .NET 6 runtime + Visual Studio 2022 com suporte a .NET 6)
- Visual Studio 2022 (Community/Professional/Enterprise) — recomendado
- PostgreSQL (local) **OU** Docker (com imagem oficial `postgres`)
- (Opcional) Postman para importar coleções
- (Opcional) dotnet-ef: `dotnet tool install --global dotnet-ef`

---
## 2. Estrutura do projeto (resumo)
Os arquivos e pastas principais do projeto gerado são:
```
/BoletosApi
  ├─ Controllers/
  │   ├─ BancosController.cs
  │   └─ BoletosController.cs
  ├─ Data/
  │   └─ AppDbContext.cs
  ├─ Dtos/
  ├─ Models/
  ├─ Profiles/
  ├─ Repositories/
  ├─ Program.cs
  ├─ appsettings.json
  └─ BoletosApi.csproj
```
O projeto expõe endpoints REST via controllers e usa Entity Framework Core + Npgsql para acessar PostgreSQL.

---
## 3. Configuração do ambiente

### Opção A — PostgreSQL local (instalador)
1. Baixe e instale o PostgreSQL: https://www.postgresql.org/download/
2. Durante a instalação, anote a senha do usuário `postgres` e a porta (padrão `5432`).
3. Abra o pgAdmin ou psql e crie o banco `boletosdb` (se ainda não existir):
```sql
CREATE DATABASE boletosdb;
```

### Opção B — PostgreSQL via Docker (recomendado se não quiser instalar)
Execute no terminal (necessário Docker Desktop instalado):
```bash
docker run --name postgres-boletos -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=boletosdb -p 5432:5432 -d postgres
```
Verifique com `docker ps` se o container está rodando.

---
## 4. Configurar `appsettings.json`
No arquivo `appsettings.json`, ajuste a connection string para apontar para seu banco:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=boletosdb;Username=postgres;Password=postgres"
}
```
Se você usou outra senha, porta ou host, ajuste aqui.

---
## 5. Rodando a aplicação (Visual Studio 2022)
1. Abra a pasta do projeto `BoletosApi` no Visual Studio 2022.  
2. Restaure pacotes: `Build -> Restore NuGet Packages` (ou `dotnet restore`).  
3. Ajuste a configuration (se necessário) e rode a aplicação (`F5` ou `Ctrl+F5`).  
4. Ao subir em modo `Development`, o Swagger aparecerá em `https://localhost:{porta}/swagger`.

---
## 6. Migrations (Entity Framework Core)
Se for a primeira execução, crie a migration e atualize o banco:

```bash
# instalar dotnet-ef (se ainda não tiver)
dotnet tool install --global dotnet-ef

# na pasta do projeto (onde está BoletosApi.csproj)
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Verifique no pgAdmin se as tabelas `bancos`, `boletos` e `__efmigrationshistory` foram criadas.

---
## 7. Testes (Swagger e Postman) — passo a passo completo

### Testes via Swagger
1. Acesse `https://localhost:{porta}/swagger` no navegador.  
2. Endpoints disponíveis (exemplo):
   - `POST /api/Bancos` — cria banco
   - `GET /api/Bancos` — lista bancos
   - `GET /api/Bancos/{id}` — busca por id
   - `GET /api/Bancos/codigo/{codigo}` — busca por código
   - `PUT /api/Bancos/{id}` — atualiza banco
   - `DELETE /api/Bancos/{id}` — exclui banco
   - `POST /api/Boletos` — cria boleto
   - `GET /api/Boletos` — lista boletos
   - `GET /api/Boletos/{id}` — busca boleto por id
   - `PUT /api/Boletos/{id}` — atualiza boleto
   - `DELETE /api/Boletos/{id}` — exclui boleto

3. Para `POST`, clique em **Try it out**, cole o JSON e clique em **Execute**.
4. Para `GET`/`PUT`/`DELETE`, insira os parâmetros (IDs) e execute.

### Testes via Postman
- Crie uma variável de ambiente `baseUrl` com o valor `https://localhost:{porta}`.
- Importe a coleção (se você tiver a colecao `.json`).  
- Ordem recomendada de testes:
  1. POST /api/Bancos — criar 2 bancos de teste
  2. POST /api/Boletos — criar 2 boletos usando os `BancoId` retornados
  3. GET /api/Bancos — confirmar bancos
  4. GET /api/Boletos — confirmar boletos
  5. GET /api/Boletos/{id} — verificar `valorComJuros` quando vencido
  6. PUT /api/Boletos/{id} — atualizar boleto
  7. DELETE /api/Boletos/{id} — remover boleto
  8. DELETE /api/Bancos/{id} — remover bancos

---
## 8. Cenário de testes sugerido (JSONs)
Use os seguintes JSONs para evitar conflitos com dados já existentes:

**Criar banco - Itaú**
```json
{
  "id": "c5b61230-93f9-4dc9-96c3-11a4c671f011",
  "nome": "Itaú Unibanco",
  "codigo": "341",
  "percentualJuros": 3.2
}
```

**Criar banco - Santander**
```json
{
  "id": "aaf23d4e-f27b-4a92-b0b4-2e3a2a5a9d21",
  "nome": "Santander",
  "codigo": "033",
  "percentualJuros": 2.8
}
```

**Criar boleto - Itaú**
```json
{
  "id": "edbfbb6f-57ac-4e32-a6f3-1f2e212ad001",
  "nomePagador": "Carlos Eduardo",
  "cpfCnpjPagador": "98765432100",
  "nomeBeneficiario": "Tech Solutions Ltda",
  "cpfCnpjBeneficiario": "11223344556677",
  "valor": 2750.50,
  "dataVencimento": "2025-11-15T00:00:00Z",
  "observacao": "Pagamento de consultoria de software",
  "bancoId": "c5b61230-93f9-4dc9-96c3-11a4c671f011"
}
```

**Criar boleto - Santander**
```json
{
  "id": "b9e9e9a3-fb8b-4e8a-96ee-39b38b4b7b71",
  "nomePagador": "Fernanda Lopes",
  "cpfCnpjPagador": "12312312399",
  "nomeBeneficiario": "Loja Digital Market",
  "cpfCnpjBeneficiario": "55443322110099",
  "valor": 899.99,
  "dataVencimento": "2025-12-05T00:00:00Z",
  "observacao": "Venda de equipamento eletrônico",
  "bancoId": "aaf23d4e-f27b-4a92-b0b4-2e3a2a5a9d21"
}
```

---
## 9. Erros comuns e soluções rápidas

### Erro: `relation "Bancos" does not exist` (42P01)
- Causa: tabelas não criadas no banco.
- Solução: rodar `dotnet ef database update` ou aplicar migrations manualmente; verifique se você está conectado ao banco `boletosdb` no pgAdmin.

### Erro: `duplicar valor da chave viola a restrição de unicidade "PK_Bancos"` (23505)
- Causa: inserção de um `Id`/GUID que já existe.
- Solução: usar um novo GUID no JSON de POST.

---
## Imagens neste pacote
- `architecture.png` — diagrama simplificado da arquitetura do projeto.
- `swagger-success.png` — imagem ilustrativa de execução de um teste bem-sucedido no Swagger.

---