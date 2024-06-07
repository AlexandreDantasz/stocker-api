using Microsoft.AspNetCore.Mvc;
using stocker.api.Models;
using stocker.api.Requests;
using stocker.api.Requests.User;
using stocker.api.Requests.DataBase;
using stocker.api.Responses;
using stocker.api.Handlers;

List<DataBase> db = new List<DataBase>(); // vai simular um banco de dados enquanto a API estiver sendo executada
Vigia vigia = new Vigia(); // para fazer verificações dentro do "banco"
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Cadastro de usuário
app.MapPost("/createUser", ([FromBody] CreateUserRequest req) => {
    Console.WriteLine("Stocker > Verificando se é possível criar usuário...");
    if (vigia.searchUser(db, req.emailUser) != -1) {
        // esse usuário já existe na base de dados
        Console.WriteLine("Stocker > Usuário já está cadastrado!");
        return new Response(false, null, "Esse usuário já está cadastrado!", 409);
    }
    DataBase temp = new DataBase(req.emailUser, req.password);
    db.Add(temp);
    Console.WriteLine("Stocker > Usuário cadastrado!");
    return new Response(true, null, "Usuário criado com sucesso!", 200);
});

// Adiciona uma coluna no "banco de dados"
app.MapPost("/addColumn", ([FromBody] AddColumnRequest req) => {
    Console.WriteLine("Stocker > Procurando usuário...");
    int index = vigia.searchUser(db, req.emailUser);
    if (index != -1) {
        // esse usuário existe na base de dados
        Console.WriteLine("Stocker > Usuário encontrado!");
        Console.WriteLine("Stocker > Verificando autenticidade...");
        if (db[index].verifyUser(req.emailUser, req.password)) {
            // usuário autenticado
            Console.WriteLine("Stocker > Usuário autenticado!");
            db[index].addColumn(req.column);
            Console.WriteLine("Stocker > Coluna adicionada!");
            return new Response(true, null, "Coluna adicionada com sucesso!", 200);
        }
        Console.WriteLine("Stocker > Senha incorreta!");
        return new Response(false, null, "Usuário não autorizado!", 401);

    }
    Console.WriteLine("Stocker > Usuário não encontrado!");
    return new Response(false, null, "Usuário não cadastrado!", 409);
});

// Adiciona uma linha
app.MapPost("/addRow", ([FromBody] AddRowRequest req) => {
    if (req.row == null) {
        Console.WriteLine("Stocker > Não é possível adicionar a linha!");
        return new Response(false, null, "A linha não pode ser nula!", 400);
    }
    Console.WriteLine("Stocker > Procurando usuário...");
    int index = vigia.searchUser(db, req.emailUser);
    if (index != -1) {
        // esse usuário existe na base de dados
        Console.WriteLine("Stocker > Usuário encontrado!");
        Console.WriteLine("Stocker > Verificando autenticidade...");
        if (db[index].verifyUser(req.emailUser, req.password)) {
            // usuário autenticado
            Console.WriteLine("Stocker > Usuário autenticado!");
            if (req.row.Length != db[index].columns.Count) {
                // o tamanho da linha não está de acordo com a
                // quantidade de colunas estabelecidas
                Console.WriteLine("Stocker > Não é possível adicionar a linha!");
                return new Response(false, null, "O número de itens da linha não coincide com o número de colunas", 400);
            }
            db[index].addRow(req.row);
            Console.WriteLine("Stocker > Linha adicionada!");
            return new Response(true, null, "Linha adicionada com sucesso!", 200);
        }
        Console.WriteLine("Stocker > Senha incorreta!");
        return new Response(false, null, "Usuário não autorizado!", 401);

    }
    Console.WriteLine("Stocker > Usuário não encontrado!");
    return new Response(false, null, "Usuário não cadastrado!", 409);
});

// deleta uma linha no "banco de dados"
app.MapDelete("/deleteRow", ([FromBody] DeleteRowRequest req) => {
    if (req.rowToDelete == null) {
        Console.WriteLine("Stocker > Linha está vazia!");
        return new Response(false, null, "A Linha não pode ser nula!", 400);
    }
    Console.WriteLine("Stocker > Procurando usuário...");
    int index = vigia.searchUser(db, req.emailUser);
    if (index != -1) {
        // esse usuário existe na base de dados
        Console.WriteLine("Stocker > Usuário encontrado!");
        Console.WriteLine("Stocker > Verificando autenticidade...");
        if (db[index].verifyUser(req.emailUser, req.password)) {
            // usuário autenticado
            Console.WriteLine("Stocker > Usuário autenticado!");
            db[index].deleteRow(req.rowToDelete);
            Console.WriteLine("Stocker > Linha excluída!");
            return new Response(true, null, "Linha excluída com sucesso!", 200);
        }
        Console.WriteLine("Stocker > Senha incorreta!");
        return new Response(false, null, "Usuário não autorizado!", 401);

    }
    Console.WriteLine("Stocker > Usuário não encontrado!");
    return new Response(false, null, "Usuário não cadastrado!", 409);
});

// Envia os dados do banco
app.MapPost("/getDatabase", ([FromBody] GetDataBaseRequest req) => {
    Console.WriteLine("Stocker > Procurando usuário...");
    int index = vigia.searchUser(db, req.emailUser);
    if (index != -1) {
        // esse usuário existe na base de dados
        Console.WriteLine("Stocker > Usuário encontrado!");
        Console.WriteLine("Stocker > Verificando autenticidade...");
        if (db[index].verifyUser(req.emailUser, req.password)) {
            // usuário autenticado
            Console.WriteLine("Stocker > Usuário autenticado!");
            Data dt = db[index].showDataBase();
            Console.WriteLine("Stocker > Dados mostrados com sucesso");
            return new Response(true, dt.data, "Dados mostrados com sucesso!", 200);
        }
        Console.WriteLine("Stocker > Senha incorreta!");
        return new Response(false, null, "Usuário não autorizado!", 401);

    }
    Console.WriteLine("Stocker > Usuário não encontrado!");
    return new Response(false, null, "Usuário não cadastrado!", 409);
});

app.MapPost("/search", ([FromBody] SearchRequest req) => {
    Console.WriteLine("Stocker > Procurando usuário...");
    int index = vigia.searchUser(db, req.emailUser);
    if (index != -1) {
        // esse usuário existe na base de dados
        Console.WriteLine("Stocker > Usuário encontrado!");
        Console.WriteLine("Stocker > Verificando autenticidade...");
        if (db[index].verifyUser(req.emailUser, req.password)) {
            // usuário autenticado
            Console.WriteLine("Stocker > Usuário autenticado!");
            Console.WriteLine("Stocker > Buscando no banco de dados...");
            Data? dt = db[index].search(req.key, req.value);
            if (dt == null) {
                Console.WriteLine("Stocker > Nada foi encontrado");
                return new Response(false, null, "Não foi possível encontrar nada com essas especificações", 200);
            }
            Console.WriteLine("Stocker > Enviando os dados encontrados...");
            return new Response(true, dt.data, "Dados encontrados!", 200);
        }
        Console.WriteLine("Stocker > Senha incorreta!");
        return new Response(false, null, "Usuário não autorizado!", 401);

    }
    Console.WriteLine("Stocker > Usuário não encontrado!");
    return new Response(false, null, "Usuário não cadastrado!", 409);
});

app.MapPut("/updateElement", ([FromBody] UpdateRequest req) => {
    Console.WriteLine("Stocker > Procurando usuário...");
    int index = vigia.searchUser(db, req.emailUser);
    if (index != -1) {
        // esse usuário existe na base de dados
        Console.WriteLine("Stocker > Usuário encontrado!");
        Console.WriteLine("Stocker > Verificando autenticidade...");
        if (db[index].verifyUser(req.emailUser, req.password)) {
            // usuário autenticado
            Console.WriteLine("Stocker > Usuário autenticado!");
            if (db[index].update(req.key, req.value, req.newInfo)) {
                Console.WriteLine("Stocker > Dados atualizados!");
                return new Response(true, null, "Os dados foram atualizados!", 200);
            }
            Console.WriteLine("Stocker > Não foi possível atualizar os dados.");
            return new Response(false, null, "Não foi possível atualizar os dados", 401);
        }
        Console.WriteLine("Stocker > Senha incorreta!");
        return new Response(false, null, "Usuário não autorizado!", 401);

    }
    Console.WriteLine("Stocker > Usuário não encontrado!");
    return new Response(false, null, "Usuário não cadastrado!", 409);
});

app.MapPut("/updateColumn", ([FromBody] UpdateColumnRequest req) => {
    Console.WriteLine("Stocker > Procurando usuário...");
    int index = vigia.searchUser(db, req.emailUser);
    if (index != -1) {
        // esse usuário existe na base de dados
        Console.WriteLine("Stocker > Usuário encontrado!");
        Console.WriteLine("Stocker > Verificando autenticidade...");
        if (db[index].verifyUser(req.emailUser, req.password)) {
            // usuário autenticado
            Console.WriteLine("Stocker > Usuário autenticado!");
            if (db[index].updateCol(req.oldName, req.newName)) {
                Console.WriteLine("Stocker > Dados atualizados!");
                return new Response(true, null, "Os dados foram atualizados!", 200);
            }
            Console.WriteLine("Stocker > Não foi possível atualizar os dados.");
            return new Response(false, null, "Não foi possível atualizar os dados", 401);
        }
        Console.WriteLine("Stocker > Senha incorreta!");
        return new Response(false, null, "Usuário não autorizado!", 401);

    }
    Console.WriteLine("Stocker > Usuário não encontrado!");
    return new Response(false, null, "Usuário não cadastrado!", 409);
});


app.Run();
