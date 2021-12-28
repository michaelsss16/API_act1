# Dúvidas
# Tarefas
teste de buscar todos os usuários falhando quando tem elementos 
adicionar método para formatação do usuário para entrada 
adicionar projeto com métodos úteis 
gerar testes unitários para o serviço de encriptação 
Conseguir gerar o html contendo as informações de cobertura de teste
adicionar mensagens de retorno para erro dos testes unitários de todos os arquivos de teste
Desenvolver sistema front para acesso ao projeto 
Resolver o problema de retorno nulo para a lista de produtos de uma compra 

# API_act
Descrição da atividade ppara ser executada:
Este exercício consiste em criar uma pequena API capaz de vender produtos a um cliente. 

Funcionalidades Cliente
O cliente deve poder se cadastrar no sistema com os seguintes dados: CPF, Nome, Email
Durante o cadastro, o sistema deve validar se não há outro cliente cadastrado com o mesmo CPF e o mesmo Email
Durante o cadastro, o sistema deve verificar se o CPF é válido. Exemplo de código de validação de CPF: http://www.macoratti.net/11/09/c_val1.htm
Deve ser implementado um endpoint para obter os dados do cliente pelo CPF. O CPF deve poder ser informado com ou sem pontuação.

Funcionalidades Produto
Deve ser possível criar um Produto, com as seguintes informações: Nome, Descrição, Preço, Quantidade Em Estoque
Durante o cadastro, o sistema deve gerar um Id único para o produto, no formato Guid
Deve ser possível listar o nome, preço e id de todos os produtos.
Deve ser possível buscar um produto por id, obtendo todos os dados do produto.

Funcionalidades de venda
Deve ser possível realizar uma venda de produtos para um cliente.
A venda consiste de um objeto com o cpf do cliente, os id dos produtos adquiridos, e as quantidades adquiridas.
A venda deve possuir um Id único, no formato Guid.
Ao realizar a venda o sistema deve validar se o item possui a quantidade que o cliente deseja comprar em estoque, em caso positivo, o sistema deve subtrair do item a quantidade que o cliente está comprando. Caso não haja em estoque a quantidade comprada pelo cliente, o sistema deve devolver uma mensagem de erro, informando qual produto não possui a quantidade desejada
Deve ser possivel recuperar os detalhes de uma compra usando o Id da compra, incluindo os itens, seus nomes, valores e o valor total da compra.
Deve ser possível listar todas as compras de um cliente através de seu CPF.
Funcionalidades de usuário

Deve ser permitida a criação de usuários
Os usuários devem ser divididos em dois grupos: Clientes e Administradores (enums)
Ambos devem ser cadastrados com as seguintes informações: Nome, Email, CPF, Senha e id
A senha não deve ser armazenada na base de dados sem criptografia
