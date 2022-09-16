
namespace Construction_Ke.Model
{
    public interface IloginInterface
    {
        Task<SysLogin> Login(string username, string password);
    }
}
