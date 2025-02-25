# JWT com .NET - Estudo

Este projeto é um estudo sobre autenticação utilizando **JSON Web Token (JWT)** em uma API .NET.

## 📌 Tecnologias Utilizadas
- .NET 6+
- JWT Bearer Authentication
- ASP.NET Core

## 🚀 Como Executar o Projeto

### 1️⃣ Configurar Secrets
Para garantir a segurança da chave JWT, usamos **User Secrets** para armazenar as credenciais localmente.

Execute os seguintes comandos no terminal:

```sh
# Inicializar User Secrets (caso ainda não tenha feito)
dotnet user-secrets init

# Definir a chave secreta do JWT
dotnet user-secrets set "JwtSettings:SecretKey" "sua-chave-secreta"

dotnet user-secrets set "JwtSettings:Issuer" "sua-issuer"
dotnet user-secrets set "JwtSettings:Audience" "sua-audience"
dotnet user-secrets set "JwtSettings:ExpirationTimeInMinutes" "60"
dotnet user-secrets set "JwtSettings:RefreshExpirationTimeInMinutes" "1440"
```

### 2️⃣ Rodar a API

No terminal, execute:
```sh
dotnet run
```
A API rodará por padrão em `https://localhost:5001`.

## 🔑 Fluxo de Autenticação JWT
1. O cliente faz um `POST` para `/missao/login` enviando o nome do herói.
2. O servidor retorna um **token JWT** e um **refresh token**.
3. O cliente usa o JWT para acessar rotas protegidas (`[Authorize]`).
4. Quando o token expira, o cliente pode renovar o token usando `/missao/refresh-token`.

## 🛠 Endpoints

### 🆔 Autenticação
- **`POST /missao/login`**  
  - Request Body: `{ "Heroi": "NomeDoHeroi" }`
  - Retorna: `{ "Token": "jwt_token", "RefreshToken": "refresh_token" }`

- **`POST /missao/refresh-token`**
  - Request Body: `{ "RefreshToken": "refresh_token" }`
  - Retorna: `{ "Token": "novo_jwt_token", "RefreshToken": "novo_refresh_token" }`

### 🔒 Endpoints Protegidos
- **`GET /missao/somente-heroi`** - Requer autenticação.
- **`GET /missao/pode-voar`** - Apenas heróis com o poder `PODE_VOAR` podem acessar.
- **`GET /missao/investigar-crime`** - Apenas heróis com o poder `SUPER_INTELIGENTE` podem acessar.

## 📖 Conceitos Aprendidos
✅ Como gerar e validar tokens JWT.  
✅ Implementação de **Refresh Token**.  
✅ Uso de **User Secrets** para armazenar informações sensíveis.  
✅ Proteção de rotas com `[Authorize]` e permissões baseadas em papéis.

