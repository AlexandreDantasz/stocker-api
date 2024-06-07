using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace stocker.api.Models;

public class Data {
    public Dictionary<int, List<string>> data;
    public Data() {
        data = new Dictionary<int, List<string>>();
    }
    public void add(int chave, List<string> valor) {
        data.Add(chave, valor);
    }
}

public class DataBase {
    public List<string> columns;
    public List<List<string>> rows;
    private string email = string.Empty;
    private string password = string.Empty;
    private byte[] saltUser;

    public DataBase(string newEmail, string newPassword) {
        columns = new List<string>();
        rows = new List<List<string>>();
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: newPassword!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
        ));
        password = hashedPassword;
        saltUser = salt;
        email = newEmail;
    }

    public bool verifyUser(string givenEmail, string givenPassword) {
        if (givenEmail != email) return false;
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: givenPassword!,
            salt: saltUser,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
        ));
        return hashedPassword == password;
    }

    public bool verifyUser(string givenEmail) {
        return givenEmail == email;
    }
    
    public void addColumn(string name) {
        columns.Add(name);
    }

    public void addRow(string [] instance) {
        List<string> info = new List<string>();
        for (int i = 0; i < columns.Count; i++) {
            info.Add(instance[i]);
        }
        rows.Add(info);
    }

    public bool deleteRow(string [] instance) {
        List<string> info = new List<string>();
        for (int i = 0; i < instance.Length; i++) {
            info.Add(instance[i]);
        }
        for (int i = 0; i < rows.Count; i++) {
            if (rows[i].SequenceEqual(info)) {
                rows.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public Data showDataBase() {
        Data data = new Data();
        data.add(1, columns);
        int index = 2;
        foreach (List<string> row in rows) data.add(index++, row);
        return data;
    }

    public Data? search(string col, string value) {
        int index = 0;
        for (; index < columns.Count && columns[index] != col; index++);
        if (index == columns.Count) {
            // não foi possível encontrar a coluna especificada
            return null;
        }
        Data dt = new Data();
        dt.add(1, columns);
        index = 2;
        foreach (List<string> line in rows) {
            if (line.Contains(value)) dt.add(index, line);
        }
        return dt.data.Count > 1 ? dt : null;
    }

    public bool update(string col, string value, string info) {
        int index = 0;
        for (; index < columns.Count && columns[index] != col; index++);
        if (index == columns.Count) {
            // não foi possível encontrar a coluna especificada
            return false;
        }
        bool resp = false;
        for (int i = 0; i < rows.Count; i++) {
            if (rows[i][index] == value) {
                rows[i][index] = info;
                resp = true;
            }
        }
        return resp;
    }

    public bool updateCol(string oldName, string newName) {
        int index = 0;
        for (; index < columns.Count && columns[index] != oldName; index++);
        if (index == columns.Count) {
            // não foi possível encontrar a coluna especificada
            return false;
        }
        columns[index] = newName;
        return true;
    }
}