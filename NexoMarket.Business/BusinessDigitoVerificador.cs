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

        public async Task Restaurar()
        {
            // TODO: Hacer metodo reutilizable para calculo y guardado de DVH
            var productList = await _productoRepository.GetAll();
            productList = DigitoVerificadorService<ProductDvhEntity>.CalcularDVH(productList);
            await _productoRepository.SaveRange(productList);

            var usuariosList = await _userRepository.GetAll();
            usuariosList = DigitoVerificadorService<UserDvhEntity>.CalcularDVH(usuariosList);
            await _userRepository.SaveRange(usuariosList);

            var dvvCalculated = await GetDvvCalculated(productList, usuariosList);
            await _digitoVerificadorVerticalRepository.SaveRange(dvvCalculated);
        }

        public async Task<BusinessResponse<List<DbInconsistencyErrorEntity>>> Verificar()
        {
            var errorList = new List<DbInconsistencyErrorEntity>();

            // Obtengo digitos verificadores de db
            var dvvListDb = await _digitoVerificadorVerticalRepository.GetAll();
            var usersList = await _userRepository.GetAll();
            var productList = await _productoRepository.GetAll();

            // Genero nuevamente los digitos verificadores
            var productListCalculated = DigitoVerificadorService<ProductDvhEntity>.CalcularDVH(productList);
            var usersListCalculated = DigitoVerificadorService<UserDvhEntity>.CalcularDVH(usersList);
            var dvvCalculated = await GetDvvCalculated(productListCalculated, usersListCalculated);


            //// Validación Digitos verificadores
            //foreach (var dgvGenerated in digitosVerticalesGenerated)
            //{
            //    var dgvDb = digitosVerticales.FirstOrDefault(d => d.Id == dgvGenerated.Id);

            //    if (dgvDb.DVV != dgvGenerated.DVV)
            //    {
            //        var newError = new DbInconsistencyErrorEntity(dgvDb.TableName, dgvDb.ColumnName, "");
            //        errorList.Add(newError);
            //    }
            //}

            //// Validación digito verificadores horizontales
            //foreach (var productGenerated in productListGenerated)
            //{
            //    var productDb = productList.FirstOrDefault(p => p.Id == productGenerated.Id);

            //    if (productDb.DVH != productGenerated.DVH)
            //    {
                    
            //    }
            //}

            //// Validación digito verificadores horizontales
            //foreach (var userGenerated in usersListGenerated)
            //{
            //    var userDb = usersList.FirstOrDefault(u => u.Id == userGenerated.Id);

            //    if (userDb.DVH != userGenerated.DVH)
            //    {
            //        // Error
            //    }
            //}


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
