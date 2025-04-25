# CrudCarros

## Instruções para Configuração e Execução do Projeto

1. **Pré-requisitos:**
   - .NET 9.0 SDK instalado
   - SQL Server LocalDB ou outro SQL Server disponível

2. **Configuração do Banco de Dados:**
   - Verifique a string de conexão em `appsettings.json`.
   - Para criar o banco e aplicar as migrations, execute:
     ```bash
     dotnet ef database update
     ```

3. **Restaurar Dependências:**
   - Execute:
     ```bash
     dotnet restore
     ```

4. **Executar o Projeto:**
   - No terminal, execute:
     ```bash
     dotnet run
     ```
   - O sistema estará disponível em `https://localhost:5001` ou `http://localhost:5000`.

5. **Acessar o Sistema:**
   - Acesse a tela de login.
   - Para registrar um novo usuário, clique em "Registre-se".

6. **Observações:**
   - O projeto utiliza ASP.NET Core MVC, Entity Framework Core e Identity.
   - Para dúvidas, consulte o código ou entre em contato com o desenvolvedor.

## Funcionalidades do Sistema

- **Autenticação de Usuário:**
  - Login e registro de novos usuários com Identity.
  - Modal de registro estilizado e seguro.

- **Gestão de Concessionárias:**
  - Cadastro, listagem e visualização de concessionárias.
  - Busca de endereço por CEP.

- **Gestão de Veículos:**
  - Cadastro de veículos com vínculo ao fabricante.
  - Listagem de veículos cadastrados.

- **Gestão de Fabricantes:**
  - Cadastro e listagem de fabricantes de veículos.

- **Venda de Veículos:**
  - Tela para realizar venda, selecionando concessionária, veículo e preenchendo dados do cliente.
  - Geração de protocolo único para cada venda.

- **Relatórios:**
  - Estrutura pronta para relatórios de vendas e estoque (implementação futura).

- **Validações:**
  - Validação de dados no backend (FluentValidation) e frontend.

- **Interface Moderna:**
  - Layout responsivo com Bootstrap 5.
  - Modais e formulários estilizados para melhor experiência do usuário.

- **APIs REST:**
  - Endpoints para operações CRUD de concessionárias, veículos, fabricantes e usuários.

- **Outros:**
  - Suporte a cache para melhorar performance de listagens.
  - Estrutura preparada para integração com relatórios e gráficos.
