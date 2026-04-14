using MySqlConnector;
using Dapper;
using System.Data;
using server.Models;

namespace server.Data;

public class DbService {
    private readonly string _connectionString;

    public DbService(string connectionString) {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync() {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryAsync<Product>("SELECT * FROM Products");
    }

    public async Task CreateOrderAsync(decimal totalAmount) {
        using IDbConnection db = new MySqlConnection(_connectionString);
        const string sql = "INSERT INTO Orders (TotalAmount, Status) VALUES (@totalAmount, 'Pending')";
        await db.ExecuteAsync(sql, new { totalAmount });
    }
}