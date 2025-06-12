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
    public class DigitoVerificadorService<T> where T : class, new()
    {
        public static DigitoVerificadorVerticalEntity CalcularDVV(List<T> entities, DigitoVerificadorVerticalEntity dvvEntity)
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => Type.GetTypeCode(Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType) != TypeCode.Object);

            var sbDvv = new StringBuilder();

            foreach (var prop in props)
            {
                var sb = new StringBuilder();

                foreach (var entity in entities)
                {
                    var value = prop.GetValue(entity)?.ToString() ?? string.Empty;
                    sb.Append(value);
                }

                var dvv = CryptoManager.EncryptString(sb.ToString());

                sbDvv.Append(dvv);
            }

            var tableDvv = CryptoManager.EncryptString(sbDvv.ToString());

            return new DigitoVerificadorVerticalEntity
            {
                Id = dvvEntity.Id,
                TableName = dvvEntity.TableName,
                DVV = tableDvv
            };
        }

        public static List<T> CalcularDVH(List<T> entityList)
        {
            var entityListWithDvh = new List<T>();

            foreach (var entity in entityList)
            {
                entityListWithDvh.Add(CalcularDVH(entity));
            }

            return entityListWithDvh;
        }

        public static T CalcularDVH(T entity)
        {
            var clonedEntity = new T();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();

            var sbParciales = new StringBuilder();

            foreach (var prop in props)
            {
                if (prop.Name.StartsWith("DVH_"))
                    continue;

                var value = prop.GetValue(entity);
                prop.SetValue(clonedEntity, value); // Copia todos los valores

                // Saltear DVH, DVH_<>, y Id
                if (prop.Name == "DVH" || prop.Name == "Id")
                    continue;

                // Calcular DVH_<Prop> si existe esa propiedad
                var dvhPropName = $"DVH_{prop.Name}";
                var dvhProp = props.FirstOrDefault(p => p.Name == dvhPropName && p.CanWrite);
                if (dvhProp != null)
                {
                    string stringValue;

                    if (value is byte[] byteArray)
                    {
                        stringValue = Convert.ToBase64String(byteArray); // Codificamos correctamente
                    }
                    else
                    {
                        stringValue = value?.ToString() ?? string.Empty;
                    }

                    var dvhValue = CryptoManager.EncryptString(stringValue);
                    dvhProp.SetValue(clonedEntity, dvhValue);
                    sbParciales.Append(dvhValue);
                }

            }

            // Finalmente setear el DVH completo usando los parciales
            var dvhFinalProp = typeof(T).GetProperty("DVH");
            if (dvhFinalProp != null && dvhFinalProp.CanWrite)
            {
                dvhFinalProp.SetValue(clonedEntity, CryptoManager.EncryptString(sbParciales.ToString()));
            }

            return clonedEntity;
        }



    }
}
