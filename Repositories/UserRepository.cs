using System.Data;
using Dapper;
using ProdutosApi.Models;

namespace ProdutosApi.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @Username";
            var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
            return user;
        }

        
    }
}
