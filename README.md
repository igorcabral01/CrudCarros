# CrudCarros

Sistema completo para gestão de concessionárias, veículos, fabricantes, usuários e vendas de automóveis.

## Sumário
- [Visão Geral](#visão-geral)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Como Executar](#como-executar)
- [Funcionalidades](#funcionalidades)
- [Regras de Negócio](#regras-de-negócio)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Evidências](#evidências)

---

## Visão Geral
O CrudCarros é uma aplicação ASP.NET Core MVC com Entity Framework Core, focada em operações de cadastro, consulta, atualização e exclusão (CRUD) para concessionárias, veículos, fabricantes, usuários e vendas. O sistema possui autenticação, autorização por perfil, validações robustas e interface moderna.

## Tecnologias Utilizadas
- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- Identity (autenticação e autorização)
- FluentValidation
- Bootstrap 5
- SQL Server (LocalDB ou outro)
- Swagger (documentação de API)
- QuestPDF (relatórios em PDF)
- Chart.js (gráficos interativos)
- SonarQube (análise de qualidade de código)

## Como Executar
1. **Pré-requisitos:**
   - .NET 9.0 SDK
   - SQL Server LocalDB ou outro SQL Server
   - (Opcional) SonarQube Community Edition para análise de qualidade de código

2. **Configuração:**
   - Ajuste a string de conexão em `appsettings.json`.
   - Execute as migrations:
     ```bash
     dotnet ef database update
     ```
   - Restaure dependências:
     ```bash
     dotnet restore
     ```
   - Rode o projeto:
     ```bash
     dotnet run
     ```
   - Acesse: https://localhost:5001 ou http://localhost:5000

3. **Análise de Qualidade de Código (SonarQube):**
   - Instale o SonarQube localmente e inicie o servidor (StartSonar.bat).
   - Instale o SonarScanner para .NET:
     ```bash
     dotnet tool install --global dotnet-sonarscanner
     ```
   - Execute os comandos abaixo na raiz do projeto para rodar a análise:
     ```bash
     dotnet sonarscanner begin /k:"CrudCarris" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="SEU_TOKEN"
     dotnet build CrudCarros.sln
     dotnet sonarscanner end /d:sonar.token="SEU_TOKEN"
     ```
   - Acesse http://localhost:9000 para visualizar o relatório.

4. **Acesso:**
   - Tela de login disponível na raiz.
   - Usuário admin padrão: `admin@admin.com` / senha: `Admin123!`
   - Registre novos usuários conforme necessário.

## Funcionalidades
- **Autenticação e Perfis:**
  - Login, registro, perfis de Administrador e Usuário.
  - Controle de acesso por perfil.
- **Gestão de Concessionárias:**
  - Cadastro, edição, exclusão, listagem e busca por nome/localização.
  - Busca de endereço por CEP.
- **Gestão de Veículos:**
  - Cadastro, edição, exclusão, listagem e busca por fabricante/modelo.
- **Gestão de Fabricantes:**
  - Cadastro, edição, exclusão e listagem.
- **Gestão de Usuários:**
  - Cadastro, edição, exclusão e listagem.
- **Venda de Veículos:**
  - Realização de venda, seleção de concessionária, veículo e preenchimento dos dados do cliente.
  - Geração de protocolo único para cada venda.
- **Validações:**
  - Backend (FluentValidation) e frontend.
- **APIs REST:**
  - Endpoints para todas as entidades principais.
- **Cache:**
  - Listagens otimizadas com cache em memória.
- **Interface Moderna:**
  - Layout responsivo, modais e formulários estilizados.
- **Relatórios:**
  - Estrutura pronta para relatórios de vendas e estoque (implementação futura).

## Regras de Negócio
### Concessionária
- Nome, endereço, cidade, estado, CEP (formato 00000-000), telefone e e-mail obrigatórios.
- Capacidade máxima de veículos deve ser positiva.
- Telefone e e-mail validados por formato.
- Busca de endereço por CEP integrada à BrasilAPI.

### Fabricante
- Nome, país de origem e ano de fundação obrigatórios.
- Ano de fundação não pode ser futuro.
- Website opcional, mas validado se informado.

### Veículo
- Nome, ano de fabricação (>= 1900), preço (> 0), fabricante e tipo obrigatórios.
- Descrição opcional (até 500 caracteres).
- Não é permitido cadastrar veículo com preço negativo.

### Usuário
- Nome, e-mail e senha obrigatórios.
- E-mail único e válido.
- Senha com mínimo de 6 caracteres, pelo menos uma letra maiúscula, uma minúscula e um dígito.
- Perfis: Administrador (pode promover/rebaixar usuários) e Usuário.

### Venda
- Concessionária, fabricante, veículo, nome, CPF (formato 000.000.000-00), telefone, data da venda e preço obrigatórios.
- Data da venda não pode ser futura.
- Preço de venda deve ser positivo e não pode ser maior que o preço do veículo.
- Geração automática de número de protocolo único para cada venda.

## Estrutura do Projeto
```
Controllers/         # Controllers MVC e APIs
Models/              # Modelos de domínio
Repositories/        # Acesso a dados (Entity Framework)
Services/            # Lógica de negócio
Validators/          # Validações (FluentValidation)
Views/               # Views Razor (MVC)
Migrations/          # Migrations do banco de dados
wwwroot/             # Arquivos estáticos (CSS, JS, etc)
Evidencias/          # Fotos do projeto funcionando
```

## Evidências
A pasta `Evidencias` contém imagens do sistema em funcionamento, demonstrando as principais telas e funcionalidades.

---

Para dúvidas, consulte o código ou entre em contato com o desenvolvedor.
