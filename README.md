# Sistema de Notifica��o

Este � um sistema de notifica��es e gerenciamento de eventos desenvolvido com ASP.NET Core Razor Pages e Entity Framework Core. O objetivo do projeto � permitir que administradores gerenciem eventos e enviem notifica��es para usu�rios cadastrados.

## Funcionalidades

- **Gest�o de Eventos**
  - Criar, editar e excluir eventos.
  - Listagem dos eventos cadastrados.

- **Gest�o de Notifica��es**
  - Enviar notifica��es para usu�rios espec�ficos.
  - Listar, editar e excluir notifica��es.
  - Envio manual de notifica��es ainda n�o enviadas.
  - Filtro de notifica��es por usu�rio.

- **Autentica��o**
  - Sistema de autentica��o integrado utilizando ASP.NET Core Identity.

## Tecnologias Utilizadas

- ASP.NET Core Razor Pages
- Entity Framework Core
- SQL Server (ou outro banco de dados configurado)
- ASP.NET Identity

## Estrutura do Projeto

- `Pages/Privacy.cshtml`: P�gina de administra��o para gerenciamento de eventos e notifica��es.
- `Pages/EventCRUD/`: CRUD completo de Eventos.
- `Pages/NotificationCRUD/`: CRUD completo de Notifica��es.
- `Data/`: Contexto do banco de dados e migrations.
- `Models/`: Modelos de dados (Evento, Notificacao, ApplicationUser).
- `Areas/Identity/Pages/Account/`: P�ginas de autentica��o e gerenciamento de conta.

## Como Executar

1. **Clone o reposit�rio:**
   ```bash
   git clone https://github.com/gabriel-fcruzz/SistemaDeNotificacao.git
   cd SistemaDeNotificacao
   ```

2. **Configure o banco de dados:**
   - Altere a string de conex�o em `appsettings.json` conforme necess�rio.

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

- **P�gina Inicial:** Lista os eventos e as notifica��es do usu�rio logado.
- **Administra��o:** Gerenciamento de eventos e notifica��es, dispon�vel para administradores.

## Contribui��o

Contribui��es s�o bem-vindas! Sinta-se � vontade para abrir issues ou pull requests.

## Licen�a

Este projeto est� licenciado sob a licen�a MIT.

---

Desenvolvido por [Gabriel Cruz](https://github.com/gabriel-fcruzz)
