using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using NexoMarket.Service;
using NexoMarket.Service.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BusinessUser
    {
        private readonly UserRepository _userRepository;
        private readonly DigitoVerificadorVerticalRepository _digitoVerificadorVerticalRepository;

        public BusinessUser()
        {
            _userRepository = new UserRepository();
            _digitoVerificadorVerticalRepository = new DigitoVerificadorVerticalRepository();
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
            user.Password = CryptoManager.EncryptString(user.Password);
            var result = await _userRepository.CreateUser(user);

            if (result)
            {
                await UpdateDvv();
            }

            return result;
        }

        public async Task BlockUser(string username)
        {
            await _userRepository.BlockUser(username);
        } 

        private async Task UpdateDvv()
        {
            var usuariosList = await _userRepository.GetAll();
            var digitosVerticalesList = await _digitoVerificadorVerticalRepository.GetAll();
            var dvvUser = digitosVerticalesList.FirstOrDefault(u => u.TableName == "Usuarios");

            var digitosUpdated = new List<DigitoVerificadorVerticalEntity>
            {
                DigitoVerificadorService<UserDvhEntity>.CalcularDVV(usuariosList, dvvUser)
            };

            await _digitoVerificadorVerticalRepository.SaveRange(digitosUpdated);
        }
    }
}
