using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using NexoMarket.Service;
using NexoMarket.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BusinessDigitoVerificador
    {
        private readonly UserRepository _userRepository;
        private readonly ProductoRepository _productoRepository;
        private readonly DigitoVerificadorVerticalRepository _digitoVerificadorVerticalRepository;

        public BusinessDigitoVerificador()
        {
            _userRepository = new UserRepository();
            _productoRepository = new ProductoRepository();
            _digitoVerificadorVerticalRepository = new DigitoVerificadorVerticalRepository();
        }

        public BusinessResponse<List<DbInconsistencyErrorEntity>> Verificar()
        {
            var errorList = new List<DbInconsistencyErrorEntity>();



            return new BusinessResponse<List<DbInconsistencyErrorEntity>>(errorList, ok: errorList.Count == 0);
        }

        public async Task Restaurar()
        {
            // TODO: Hacer metodo reutilizable para calculo y guardado de DVH
            var productList = await _productoRepository.GetAll();
            productList = DigitoVerificadorService<ProductDvhEntity>.CalcularDVH(productList);
            await _productoRepository.SaveRange(productList);

            var usuariosList = await _userRepository.GetAll();
            usuariosList = DigitoVerificadorService<UserDvhEntity>.CalcularDVH(usuariosList);
            await _userRepository.SaveRange(usuariosList);

            var digitosVerticalesList = await _digitoVerificadorVerticalRepository.GetAll();

            var digitosUpdated = new List<DigitoVerificadorVerticalEntity>();
            digitosUpdated.AddRange(DigitoVerificadorService<ProductDvhEntity>.CalcularDVV(productList, "Producto", digitosVerticalesList));
            digitosUpdated.AddRange(DigitoVerificadorService<UserDvhEntity>.CalcularDVV(usuariosList, "Usuarios", digitosVerticalesList)); 

            await _digitoVerificadorVerticalRepository.SaveRange(digitosUpdated);
        }
    }
}
