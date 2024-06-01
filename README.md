# Stocker API
Essa API foi construída para ser hospedada no [Google App Engine](https://cloud.google.com/appengine?utm_source=google&utm_medium=cpc&utm_campaign=latam-BR-all-pt-dr-BKWS-all-all-trial-e-dr-1707800-LUAC0008673&utm_content=text-ad-none-any-DEV_c-CRE_429691579858-ADGP_Hybrid+%7C+BKWS+-+EXA+%7C+Txt_Compute-App+Engine-KWID_43700040358183168-kwd-372661972204&utm_term=KW_google+app+engine-ST_Google+App+Engine&gad_source=1&gclid=Cj0KCQjw6uWyBhD1ARIsAIMcADrLp9OH7ZanA0SE25fryGhjL1Nvudd8lf0lqZlnxi-DXEaYwtd8s5MaAsfuEALw_wcB&gclsrc=aw.ds&hl=pt_br) <br>

## Sumário
- [Post](#Post)
    - [Post Create User](#PostCreateUser)
    - [Post Add Column](#PostAddColumn)
    - [Post Add Row](#PostAddRow)
- [Get](#Get)
    - [Get Database](#GetDatabase)
    - [Get Search](#GetSearch)
- [Put](#Put)
    - [Put Update](#PutUpdate)
- [Delete](#Delete)
    - [Delete Row](#DeleteRow)

## <a id="Post">Post</a>
- ### <a id="PostCreateUser">Post Create User</a>
    Cadastra um usuário.
    - #### Requisição: 
        - emailUser (string necessária) -> email do usuário
        - password (string necessária) -> senha do usuário
    - #### Resposta: 
        - Data (valor nulo)
        - Message (string) -> representa a mensagem de status a partir da requisição feita
- ### <a id="PostAddColumn">Post Add Column</a>
    Adiciona o nome de uma coluna no banco de dados de um usuário.
    - #### Requisição: 
        - emailUser (string necessária) -> email do usuário
        - password (string necessária) -> senha do usuário
        - column (string necessária) -> nome da coluna para ser adicionado
    - #### Resposta: 
        - Data (valor nulo)
        - Message (string) -> representa a mensagem de status a partir da requisição feita
- ### <a id="PostAddRow">Post Add Row</a>
    Adiciona uma linha no banco de dados de um usuário.
    - #### Requisição: 
        - emailUser (string necessária) -> email do usuário
        - password (string necessária) -> senha do usuário
        - row (vetor de string necessário) -> linha para ser adicionada
    - #### Resposta: 
        - Data (valor nulo)
        - Message (string) -> representa a mensagem de status a partir da requisição feita

## <a id="Get">Get</a>
- ### <a id="GetDatabase">Get Database</a>
    Retorna os dados de um banco de dados de um usuário.
    - #### Requisição: 
        - emailUser (string necessária) -> email do usuário
        - password (string necessária) -> senha do usuário
    - #### Resposta: 
        - Data (Dicionário de chave inteira e valor de vetor de strings)
            - Chave (número inteiro) -> representa o número da linha
            - Valor (vetor de strings) -> representa os valores das colunas da linha
        - Message (string) -> representa a mensagem de status a partir da requisição feita
- ### <a id="GetSearch">Get Search</a>
    Retorna os dados de uma busca no banco de dados de um usuário.
    - #### Requisição: 
        - emailUser (string necessária) -> email do usuário
        - password (string necessária) -> senha do usuário
        - key (string necessária) -> nome da coluna de referência
        - value (string necessária) -> valor da coluna de referência
    - #### Resposta: 
        - Data (Dicionário de chave inteira e valor de vetor de strings)
            - Chave (número inteiro) -> representa o número da linha
            - Valor (vetor de strings) -> representa os valores das colunas da linha
        - Message (string) -> representa a mensagem de status a partir da requisição feita

## <a id="Put">Put</a>
- ### <a id="PutUpdate">Put Update</a>
    Atualiza um elemento no banco de dados do usuário.
    - #### Requisição: 
        - emailUser (string necessária) -> email do usuário
        - password (string necessária) -> senha do usuário
        - key (string necessária) -> nome da coluna de referência
        - value (string necessária) -> valor atual da coluna especificada
        - newInfo (string necessária) -> valor substituto
    - #### Resposta: 
        - Data (valor nulo)
        - Message (string) -> representa a mensagem de status a partir da requisição feita
## <a id="Delete">Delete</a>
- ### <a id="DeleteRow">Delete Row</a>
    Atualiza um elemento no banco de dados do usuário.
    - #### Requisição: 
        - emailUser (string necessária) -> email do usuário
        - password (string necessária) -> senha do usuário
        - key (string necessária) -> nome da coluna de referência
        - value (string necessária) -> valor atual da coluna especificada
        - newInfo (string necessária) -> valor substituto
    - #### Resposta: 
        - Data (valor nulo)
        - Message (string) -> representa a mensagem de status a partir da requisição feita