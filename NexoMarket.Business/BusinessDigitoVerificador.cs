using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using NexoMarket.Service;
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

        public async Task Recomponer()
        {
            var productListCalculated = await _productoRepository.GetAll();
            productListCalculated = DigitoVerificadorService<ProductDvhEntity>.CalcularDVH(productListCalculated);
            await _productoRepository.SaveRange(productListCalculated);

            var usuariosListCalculated = await _userRepository.GetAll();
            usuariosListCalculated = DigitoVerificadorService<UserDvhEntity>.CalcularDVH(usuariosListCalculated);
            await _userRepository.SaveRange(usuariosListCalculated);

            var dvvListCalculated = await GetDvvCalculated(productListCalculated, usuariosListCalculated);
            await _digitoVerificadorVerticalRepository.SaveRange(dvvListCalculated);
        }

        public async Task<BusinessResponse<List<DbInconsistencyErrorEntity>>> Verificar()
        {
            var errorList = new List<DbInconsistencyErrorEntity>();

            // Obtengo digitos verificadores de db
            var dvvListDb = await _digitoVerificadorVerticalRepository.GetAll();
            var productList = await _productoRepository.GetAll();
            var usersList = await _userRepository.GetAll();

            // Genero nuevamente los digitos verificadores
            var productListCalculated = DigitoVerificadorService<ProductDvhEntity>.CalcularDVH(productList);
            var usersListCalculated = DigitoVerificadorService<UserDvhEntity>.CalcularDVH(usersList);
            var dvvListCalculated = await GetDvvCalculated(productListCalculated, usersListCalculated);

            foreach (var dvv in dvvListDb)
            {
                var dvvCalculated = dvvListCalculated.FirstOrDefault(d => d.Id == dvv.Id);

                // Veifico a nivel general para mejor performance
                if (dvv.DVV == dvvCalculated.DVV)
                    continue;

                if (dvv.TableName == "Producto")
                    errorList.AddRange(DigitoVerificadorService<ProductDvhEntity>.FindErrors(productList, productListCalculated, dvv.TableName));
                else
                    errorList.AddRange(DigitoVerificadorService<UserDvhEntity>.FindErrors(usersList, usersListCalculated, dvv.TableName));
            }

            return new BusinessResponse<List<DbInconsistencyErrorEntity>>(errorList, ok: errorList.Count == 0);
        } 

        private async Task<List<DigitoVerificadorVerticalEntity>> GetDvvCalculated(List<ProductDvhEntity> productDvhEntities, List<UserDvhEntity> userDvhEntities)
        {
            var digitosVerticalesList = await _digitoVerificadorVerticalRepository.GetAll();

            var dvvProducto = digitosVerticalesList.FirstOrDefault(p => p.TableName == "Producto");
            var dvvUser = digitosVerticalesList.FirstOrDefault(u => u.TableName == "Usuarios");

            var digitosUpdated = new List<DigitoVerificadorVerticalEntity>
            {
                DigitoVerificadorService<ProductDvhEntity>.CalcularDVV(productDvhEntities, dvvProducto),
                DigitoVerificadorService<UserDvhEntity>.CalcularDVV(userDvhEntities, dvvUser)
            };

            return digitosUpdated;
        }
    }
}
