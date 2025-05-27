using NexoMarket.Data.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexoMarket.Entity.Dtos;
using NexoMarket.Service.Helpers;
using NexoMarket.Data;
using NexoMarket.Data.Repository;
using NexoMarket.Entity.Entities;

namespace NexoMarket.Business
{
    public class BusinessUser
    {
        private readonly UserRepository _userRepository;
        private Dictionary<string, int> usuarios_intentos = new Dictionary<string, int>();
        public BusinessUser()
        {
            _userRepository = new UserRepository();
        }

        public async Task<BusinessResponse<UserDto>> Login(string username, string password)
        {
            if (!await _userRepository.ExistByUsername(username))
            {
                return new BusinessResponse<UserDto>(mensaje: "Usuario no existe");
            }

            var passwordEncrypted = CryptoManager.EncryptString(password);
            var user = await _userRepository.Login(username, passwordEncrypted);

            if (user != null && user.IsBlocked)
            {
                return new BusinessResponse<UserDto>(mensaje: "Usuario Bloqueado");
            }

            if (user == null)
            { 
                return new BusinessResponse<UserDto>(mensaje: "Credenciales Incorrectas");
            }
            
            return  new BusinessResponse<UserDto>( data : user, mensaje: "Usuario Logueado", ok: true);
        }
    }
}
