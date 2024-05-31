using stocker.api.Models;
namespace stocker.api.Handlers;

public class Vigia {
    public int searchUser(List<DataBase> db, string email) { // retorna o endereço do usuário caso ele exista
        int i;
        for (i = 0; i < db.Count && !db[i].verifyUser(email); i++);
        return i < db.Count ? i : -1;
    }

}