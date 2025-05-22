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

namespace NexoMarket.Business
{
    public class BusinessUser
    {
        private readonly UserRepository _userRepository;
        public BusinessUser()
        {
            _userRepository = new UserRepository();
        }
        public async Task<List<UserDto>> GetUsuariosAsync()
        {
            return await _userRepository.GetUsuariosAsync();
        }
        public async Task<UserDto> Login(string username, string password)
        {
            var passwordEncrypted = CryptoManager.EncryptString(password);
            return await _userRepository.Login(username, passwordEncrypted);

        }
    }
}
