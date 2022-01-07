import requests;
tokenCliente = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1pY2hhZWwiLCJyb2xlIjoiY2xpZW50ZSIsIm5hbWVpZCI6IjEzNjAyMTUxNjYyIiwibmJmIjoxNjQxNTc0MjExLCJleHAiOjE2NDE1ODE0MTEsImlhdCI6MTY0MTU3NDIxMX0.XQNKoepaMzHiTCigJnSBB4Jhx8rWWzPBvKa6kdJUs_w"
tokenAdministrador = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImpvbmF0aGFuIiwicm9sZSI6ImFkbWluaXN0cmFkb3IiLCJuYW1laWQiOiIxMzYwMjE1MTY2MiIsIm5iZiI6MTY0MDg4MjkxMSwiZXhwIjoxNjQwODkwMTExLCJpYXQiOjE2NDA4ODI5MTF9.tL2ntHrkXO8VLuVEP7mbqj-l5sscsXqTyvPk8pgkvJE"
headersCliente = {"Authorization": f"Bearer {tokenCliente}"}
headersAdministrador = {"Authorization": f"Bearer {tokenAdministrador}"}

url1 = "http://localhost:11338/API/Clientes"
url2= "http://localhost:11338/API/Produtos"
url3= "http://localhost:11338/API/Vendas"
url4= "http://localhost:11338/API/Usuarios"
url5 = "http://localhost:11338/API/Login"
url6 = "http://localhost:11338/API/Cupom"

print("Sistema de vendas\n")
option = input("0 - Sair \n 1 - Cliente \n 2- Produto \n 3 - Venda \n 4 - Usuario \n 5 - Login\n 6 - Cupom \n")

# Cliente
if (option == '1'):
	option2 = input("0- Sair\n 1- Listar todos os clientes\n 2- Buscar cliente por cpf\n 3- Adicionar cliente\n")
	if(option2 == '0'):
		exit()
	if(option2 == '1'):
		response = requests.get(url1, headers=headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				
	if(option2 == '2'):
		cpf= input('CPF do cliente: ')
		response = requests.get(url1+'/'+cpf, headers=headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				
	if(option2 == '3'):
		nome = input("Nome: ")
		cpf = input("CPF: ")
		email = input("Email: ")
		cliente = {"Nome": nome, "CPF": cpf, "Email": email}		
		response = requests.post(url1, json = cliente, headers = headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				

# Produtos
if (option == '2'):
	option2 = input("0- Sair\n 1- Buscar todos os produtos\n 2- Buscar produto por ID\n 3- Adicionar produto")

	if(option2 == '0'):
		exit()
	if(option2 == '1'):
		response = requests.get(url2, headers = headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				
	if(option2 == '2'):
		id = input ("Id do produto: ")
		response = requests.get(url2+'/'+id, headers = headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				
	if(option2 == '3'):
		nome = input('Nome: ')
		descricao = input('Descrição: ')
		valor= input('Valor: ')
		quantidade = input('Quantidade: ')
		produto = {'Nome':nome, 'Descricao':descricao, 'Valor':valor, 'Quantidade':quantidade}
		response = requests.post(url2, json = produto, headers = headersAdministrador)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				

# Venda
if (option == '3'):
	option2 = input("0- Sair\n 1- Listar todas as vendas\n 2- Buscar venda por ID\n 3- Buscar venda por CPF\n 4- Adicionar venda\n")
	if(option2 == '0'):
		exit()
	if(option2 == '1'):
		response = requests.get(url3, headers = headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				
	if(option2 == '2'):
		id = input('ID da venda: ')
		response = requests.get(url3+'/'+id, headers = headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				
	if(option2 == '3'):
		cpf = input('CPF: ')
		response = requests.get(url3+'/cpf/'+cpf, headers = headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))				
	if(option2 == '4'):
		id = input ('Id do produto: ')
		quantidade = input ('Quantidade: ')
		cpf = input ('CPF: ')
		option3 = input("Deseja adicionar cupom?\n 0- Não\n 1- Sim\n")
		if(option3=='1'):
			cupomId = input ('Id do cupom: ')
			venda = {"ListaProdutos": [{"ProdutoId":id, "Quantidade": quantidade}], "CPF":cpf, "CupomId": cupomId}
			response = requests.post(url3, json = venda, headers = headersCliente)
			print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))					
		if(option3 == '0'):
			venda = {"ListaProdutos": [{"ProdutoId":id, "Quantidade": quantidade}], "CPF":cpf}
			response = requests.post(url3, json = venda, headers = headersCliente)
			print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))							

# Usuários
if(option == '4'):
	option2 = input('0- Sair 1- Listar todos os usuários\n 2- Buscar usuário por ID\n 3- Adicionar usuário\n')
	if(option2 == '0'):
		exit()
	if(option2 == '1'):
		response = requests.get(url4, headers = headersCliente)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))					
	if(option2 == '2'):
		id = input('Id do usuário: ')
		response = requests.get(url4+'/'+id, headers = headersCliente)
	if(option2 == '3'):
		nome = input("Nome: ")
		email = input("Email: ")
		cpf = input("CPF: ")
		senha = input("Senha: ")
		tipo = input("Tipo: ")
		usuario = {'Nome':nome, 'Email':email, 'CPF':cpf, 'Senha':senha, 'Tipo':tipo}
		response = requests.post(url4, json = usuario)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))					
if (option == '6'):
	option2 = input("1- Ver todos os cupons\n 2 - Buscar cupom por id \n 3 -  Adicionar cupom\n")
	if (option2 == '1'):
		response = requests.get(url6);
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))					
	if (option2 == '2'):
		id = input("Id:")
		response = requests.get(url6+'/'+id)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))					
	if (option2 == '3'):
		nome = input('Nome do cupom: ')
		porcentagem= input('Porcentagem de desconto: ')
		cupom = {"Nome":nome, "Porcentagem":porcentagem}
		response = requests.post(url6, json = cupom)
		print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))					



# Login
if(option == '5'):
	nome = input("Nome: ")
	senha = input("Senha: ")
	login = {"Nome": nome, "Senha": senha}
	response = requests.post(url5, json = login)
	print(str(response.status_code )+ ': '+ response.content.decode('utf-8'))					

print("Fim do programa")


########## Chamadas rápidas para teste ##########
cliente1 = {"Nome":"Michael", "CPF":"13602151662", "Email":"Michael@dti"}
cliente2= {"Nome":"Michael", "CPF":"1234", "Email":"Michael@dti"}
cliente3= {"Nome":"Michael", "CPF":"1234", "Email":"michaelSilva@dti"}
cliente4 = {"Nome":"Michael", "CPF":"13602151663", "Email":"michaelSilva@dti"}

produto1 = {"Nome":"Celular", "Descricao":"Iphone 5 velho", "Valor":"1200", "Quantidade":"100"}
produto2 = {"Nome":"carro", "Descricao":"carro velho", "Valor":"10000", "Quantidade":"50"}
produto3= {"Id": "83b00b61-8fed-4656-b40d-970cb8665c68", "Nome":"Iphone", "Descricao":"Iphone 10 novo", "Valor":"1200", "Quantidade":"2"}

venda1 = {"ListaProdutos": [{"ProdutoId":"05355bd5-c267-496a-8033-70216dc71106", "Quantidade": 5}], "CPF":"13602151662"}
venda2 = {"ListaProdutos": [{"ProdutoGuid":"cc165b70-4de3-447d-b2f6-9f2b6f4d8e3e", "Quantidade": 3}], "CPF":"13602151662"}

usuario1 = {"Nome":"Michael", "CPF":"857.453.020-46", "Email":"michaelSilva1@dti", "Senha": "12345", "Tipo":"cliente"}

# Chamadas de clientes
#response = requests.post(url, json = cliente1)
#response = requests.get(url+"/136.021.516-62")

# Chamadas de produtos 
#response = requests.post(url2, json = produto1)
#response = requests.post(url2, json = produto2)
#response = requests.put(url2, json = produtoatualizado)
#response = requests.get(url2+"/c87b44c4-fbf6-4832-af57-dab8324d8c31")
#response = requests.get(url2)

# Chamadas de vendas
#response = requests.post(url3, json = venda1)
#response = requests.post(url3, json = venda2)

# Chamadas de usuarios 
#response = requests.post(url4, json = usuario1)

# chamada de login
login = {"Nome":"Michael", "Senha":"12345"}
#response = requests.post(url5, json = login)
#response = requests.get(url5, headers = headers)
		
#print(response.status_code)
#print(response.content.decode('utf-8'))
#print(response)