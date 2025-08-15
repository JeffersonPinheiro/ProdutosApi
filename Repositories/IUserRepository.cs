using ProdutosApi.Models;

namespace ProdutosApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
