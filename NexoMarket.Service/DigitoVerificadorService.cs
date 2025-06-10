using NexoMarket.Entity;
using NexoMarket.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Service
{
    public class DigitoVerificadorService<T> where T : class
    {
        public static List<DigitoVerificadorVerticalEntity> CalcularDVV(List<T> entities, string tableName, List<DigitoVerificadorVerticalEntity> existentes)
        {
            var result = new List<DigitoVerificadorVerticalEntity>();

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => Type.GetTypeCode(Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType) != TypeCode.Object);

            foreach (var prop in props)
            {
                var sb = new StringBuilder();

                foreach (var entity in entities)
                {
                    var value = prop.GetValue(entity)?.ToString() ?? string.Empty;
                    sb.Append(value);
                }

                var dvv = CryptoManager.EncryptString(sb.ToString());

                var existente = existentes.FirstOrDefault(x =>
                    x.TableName == tableName && x.ColumnName == prop.Name);

                result.Add(new DigitoVerificadorVerticalEntity
                {
                    Id = existente?.Id ?? 0,
                    TableName = tableName,
                    ColumnName = prop.Name,
                    DVV = dvv
                });
            }

            return result;
        }


        public static List<T> CalcularDVH(List<T> entityList)
        {
            foreach (var entity in entityList)
            {
                var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name != "DVH")
                    .Where(p => Type.GetTypeCode(Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType) != TypeCode.Object);

                var sb = new StringBuilder();

                foreach (var prop in props)
                {
                    var value = prop.GetValue(entity)?.ToString() ?? string.Empty;
                    sb.Append(value);
                }

                var dvhProp = typeof(T).GetProperty("DVH");
                if (dvhProp != null && dvhProp.CanWrite)
                {
                    dvhProp.SetValue(entity, CryptoManager.EncryptString(sb.ToString()));
                }
            }

            return entityList;
        }

    }
}
