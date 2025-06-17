# Sistema de Notificação

Este é um sistema de notificações e gerenciamento de eventos desenvolvido com ASP.NET Core Razor Pages e Entity Framework Core. O objetivo do projeto é permitir que administradores gerenciem eventos e enviem notificações para usuários cadastrados.

## Funcionalidades

- **Gestão de Eventos**
  - Criar, editar e excluir eventos.
  - Listagem dos eventos cadastrados.

- **Gestão de Notificações**
  - Enviar notificações para usuários específicos.
  - Listar, editar e excluir notificações.
  - Envio manual de notificações ainda não enviadas.
  - Filtro de notificações por usuário.

- **Autenticação**
  - Sistema de autenticação integrado utilizando ASP.NET Core Identity.

## Tecnologias Utilizadas

- ASP.NET Core Razor Pages
- Entity Framework Core
- SQL Server (ou outro banco de dados configurado)
- ASP.NET Identity

## Estrutura do Projeto

- `Pages/Privacy.cshtml`: Página de administração para gerenciamento de eventos e notificações.
- `Pages/EventCRUD/`: CRUD completo de Eventos.
- `Pages/NotificationCRUD/`: CRUD completo de Notificações.
- `Data/`: Contexto do banco de dados e migrations.
- `Models/`: Modelos de dados (Evento, Notificacao, ApplicationUser).
- `Areas/Identity/Pages/Account/`: Páginas de autenticação e gerenciamento de conta.

## Como Executar

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/gabriel-fcruzz/SistemaDeNotificacao.git
   cd SistemaDeNotificacao
   ```

2. **Configure o banco de dados:**
   - Altere a string de conexão em `appsettings.json` conforme necessário.

3. **Execute as migrations:**
   ```bash
   dotnet ef database update
   ```

4. **Inicie o projeto:**
   ```bash
   dotnet run
   ```

5. **Acesse no navegador:**
   ```
   http://localhost:5000
   ```

## Telas principais

- **Página Inicial:** Lista os eventos e as notificações do usuário logado.
- **Administração:** Gerenciamento de eventos e notificações, disponível para administradores.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## Licença

Este projeto está licenciado sob a licença MIT.

---

Desenvolvido por [Gabriel Cruz](https://github.com/gabriel-fcruzz)
