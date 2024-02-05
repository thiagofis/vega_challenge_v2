# MyApiApp.Console

Este é um projeto de console simples que utiliza a injeção de dependência e o padrão assíncrono para interagir com uma API externa. O código foi estruturado de forma a seguir boas práticas e princípios SOLID de desenvolvimento de software, incluindo a reutilização do `HttpClient`, tratamento básico de erros e separação de responsabilidades.

## Alterações Efetuadas

### 1. Injeção de Dependência
   - A dependência do `HttpClient` foi injetada no construtor da classe `ApiService`, promovendo a reutilização da mesma instância durante toda a vida útil do serviço.

### 2. Padrão Assíncrono
   - Os métodos `AuthenticateAsync` e `GetUnitsAsync` foram marcados como assíncronos (`async` e `await`) para permitir operações assíncronas sem bloquear a thread principal.

### 3. Tratamento de Erros
   - Incluído tratamento básico de erros utilizando blocos `try-catch` para exceções específicas, como `HttpRequestException` e `Exception`. Mensagens de erro foram adicionadas para facilitar a depuração.

### 4. Centralização de Configurações
   - A configuração do `User-Agent` foi movida para o construtor do `ApiService`, proporcionando uma centralização de configurações.

## Como Rodar o Projeto

1. Certifique-se de ter o .NET SDK instalado.
2. Abra um terminal na pasta `MyApiApp.Console`.
3. Execute o comando `dotnet run`.
