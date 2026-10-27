using SQLite;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiSqliteDemo2430744
{
    public class LocalDBservice
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBservice()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            // Indica al sistema que cree/lea la tabla de manera asíncrona
            _connection.CreateTableAsync<Clientes>();
        }

        // Método para listar los registros de la tabla (coincide con MainPage.xaml.cs)
        public async Task<List<Clientes>> GetClientes()
        {
            return await _connection.Table<Clientes>().ToListAsync();
        }

        // Método para obtener un registro por ID
        public async Task<Clientes> GetById(int id)
        {
            return await _connection.Table<Clientes>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        // Método para crear registros
        public async Task Create(Clientes clientes)
        {
            await _connection.InsertAsync(clientes);
        }

        // Método para actualizar registros
        public async Task Update(Clientes clientes)
        {
            await _connection.UpdateAsync(clientes);
        }

        // Método para eliminar registros
        public async Task Delete(Clientes clientes)
        {
            await _connection.DeleteAsync(clientes);
        }
    }
}
