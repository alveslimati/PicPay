# **ApiCarteiraInvestimentos**

Bem-vindo ao projeto **ApiCarteiraInvestimentos**! Este projeto é uma API RESTful desenvolvida em **.NET 8+** para gerenciar carteiras de investimentos, incluindo clientes, ativos, e seus respectivos valores. O Swagger é utilizado para expor e testar facilmente as rotas da API.

---

## **🚀 Funcionalidades**

- Gerenciamento de carteiras de investimentos.
- Consulta de carteira por cliente.
- Retorno detalhado de ativos em uma carteira.
- Cálculo de valores totais da carteira.
- Endpoint para tratamento de erros personalizados (404 para clientes inexistentes, carteiras vazias, etc.).

---

## **📋 Pré-requisitos**

### Certifique-se de ter os seguintes programas e ferramentas instalados no ambiente de desenvolvimento:

| - **.NET 8.0 SDK ou superior** [(Download aqui)](https://dotnet.microsoft.com/download) |
|---|


- **Visual Studio 2022** ou Visual Studio Code [(Download aqui)](https://visualstudio.microsoft.com/)
- **Postman** (ou qualquer ferramenta para testar APIs REST) *(opcional)*.
- **Navegador Web** para usar o Swagger (ex.: Chrome, Edge, etc.).

---

## **🔧 Configuração do Projeto**

### Siga os passos abaixo para configurar e executar a API localmente:

| ### **1. Clone o repositório** |
|---|


Utilize o Git para clonar o repositório em sua máquina:

```bash
git clone https://github.com/alveslimati/ApiCarteiraInvestimentos.git
cd ApiCarteiraInvestimentos
```

### **2. Restaure as dependências**
Após entrar no diretório do projeto, restaure as dependências do **NuGet**:

```bash
dotnet restore
```

### **3. Compile o projeto**
Compile o projeto para garantir que ele foi configurado corretamente:

```bash
dotnet build
```

### **4. Execute a API**
Inicie o servidor para rodar a API na sua máquina:

```bash
dotnet run --project ApiCarteiraInvestimentos
```

Por padrão, a aplicação estará acessível na URL: **http://localhost:5240**/

---

## **📖 Como Usar a API via Swagger**

O Swagger UI está configurado automaticamente para oferecer uma interface interativa para testar os endpoints da API.

### **1. Acesse o Swagger**
Após iniciar o projeto, abra o navegador em:

```text
http://localhost:5240/swagger
```

Aqui você encontrará a documentação interativa gerada automaticamente, onde será possível testar diretamente os endpoints da API.

### **2. Teste os Endpoints**
### Na interface do Swagger:

| - Clique no **endpoint** desejado (ex.: `GET /api/carteiras/{clienteId}`). |
|---|


- Preencha os parâmetros necessários, se aplicável.
- Clique no botão **"Try it out"** para enviar a requisição.
- Veja o resultado da resposta diretamente na página.

---

## **🛠 Estrutura Principal do Projeto**

O projeto está organizado em camadas para facilitar o entendimento e a manutenção.

```
ApiCarteiraInvestimentos/
├── Controllers/         # Controladores responsáveis pelos endpoints
│   └── CarteirasController.cs
├── Models/              # Modelos usados na API
│   ├── CarteiraModel.cs
│   ├── AtivoModel.cs
│   └── CarteiraResponse.cs
├── Repositories/        # Camada de repositório que simula o acesso aos dados
│   ├── ICarteiraRepository.cs
│   ├── CarteiraRepository.cs
│   ├── IAtivoRepository.cs
│   └── AtivoRepository.cs
├── Services/            # Camada de lógica de negócio
│   └── CarteiraService.cs
├── Mocks/               # Mock de dados para simular persistência
│   └── CarteiraMock.cs
├── Tests/               # Testes unitários para validar a aplicação
│   ├── CarteiraControllerTests.cs
│   ├── CarteiraRepositoryTests.cs
│   └── CarteiraServiceTests.cs
├── Program.cs           # Configuração principal e registro de dependências
└── README.md            # Este arquivo!
```

---

## **🛣️ Endpoints Disponíveis**

### Aqui estão os principais endpoints disponibilizados pela API:

| ### **Carteiras** |
|---|


1. **Consultar Carteira por Cliente**  
   **GET /api/carteiras/{clienteId}**  
   Retorna os dados da carteira de investimentos de um cliente específico.

   - **Exemplo de Request:**  
     `GET /api/carteiras/123`

   - **Exemplo de Response Sucesso (200):**
     ```json
     {
       "clienteId": "123",
       "ativos": [
         { "codigo": "CDB001", "quantidade": 10 },
         { "codigo": "FII002", "quantidade": 5 }
       ],
       "valorTotal": 1500.0
     }
     ```

   - **Exemplo de Response Erro (404):**
     ```json
     {
       "mensagem": "Cliente não encontrado ou sem carteira."
     }
     ```

---

## **✅ Testes**

Os testes foram configurados utilizando o **xUnit** e o **Moq**. Existem testes para cada camada da aplicação, como controllers, serviços, e repositórios.

### **Executando os Testes**
Você pode executar os testes usando o seguinte comando:

```bash
dotnet test
```

Se tudo estiver correto, o resultado será:

```text
Passed!
```

---

## **📦 Dependências Utilizadas**

### Aqui estão os principais pacotes NuGet utilizados no projeto:

| - **Swashbuckle.AspNetCore**: Geração automática e UI do Swagger. |
|---|


- **Moq**: Framework para criação de mocks nos testes unitários.
- **xUnit**: Framework de testes utilizado para validar a aplicação.

---

## **🛡️ Boas Práticas e Observações**

1. **Desacoplamento:** 
   Todo o código segue princípios de desacoplamento, como o uso de **interfaces** para os repositórios e injeção de dependências no projeto.

2. **Mocks de Dados:** 
   Os dados de exemplo estão disponíveis em **`CarteiraMock`**, usados para simular persistência.

3. **Escalabilidade:** 
   O projeto foi organizado em camadas para facilitar a adição de novas funcionalidades, como endpoints adicionais ou persistência real no banco de dados.

---
