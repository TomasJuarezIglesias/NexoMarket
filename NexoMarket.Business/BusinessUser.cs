using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using NexoMarket.Service.Helpers;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BusinessUser
    {
        private readonly UserRepository _userRepository;
        public BusinessUser()
        {
            _userRepository = new UserRepository();
        }

        public async Task<BusinessResponse<UserEntity>> Login(string username, string password)
        {
            if (!await _userRepository.ExistByUsername(username))
            {
                return new BusinessResponse<UserEntity>(mensaje: "Usuario no existe");
            }

            var passwordEncrypted = CryptoManager.EncryptString(password);
            var user = await _userRepository.Login(username, passwordEncrypted);

            if (user != null && user.IsBlocked)
            {
                return new BusinessResponse<UserEntity>(mensaje: "Usuario Bloqueado");
            }

            if (user == null)
            { 
                return new BusinessResponse<UserEntity>(mensaje: "Credenciales Incorrectas");
            }
            
            return  new BusinessResponse<UserEntity>( data : user, mensaje: "Usuario Logueado", ok: true);
        }

        public async Task<bool> CreateUser(UserCreateEntity user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task BlockUser(string username)
        {
            await _userRepository.BlockUser(username);
        } 
    }
}
