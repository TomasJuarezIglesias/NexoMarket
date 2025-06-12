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
                prop.SetValue(clonedEntity, value);

                // Saltear DVH, DVH_<>, y Id
                if (prop.Name == "DVH" || prop.Name == "Id")
                    continue;

                var dvhPropName = $"DVH_{prop.Name}";
                var dvhProp = props.FirstOrDefault(p => p.Name == dvhPropName && p.CanWrite);
                if (dvhProp != null)
                {
                    string stringValue;

                    if (value is byte[] byteArray)
                    {
                        stringValue = Convert.ToBase64String(byteArray);
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

            var dvhFinalProp = typeof(T).GetProperty("DVH");
            if (dvhFinalProp != null && dvhFinalProp.CanWrite)
            {
                dvhFinalProp.SetValue(clonedEntity, CryptoManager.EncryptString(sbParciales.ToString()));
            }

            return clonedEntity;
        }


        public static List<DbInconsistencyErrorEntity> FindErrors(List<T> entities, List<T> entitiesCalculated, string tableName)
        {
            var errorList = new List<DbInconsistencyErrorEntity>();

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();

            var idProp = props.FirstOrDefault(p => p.Name == "Id");
            var dvhProp = props.FirstOrDefault(p => p.Name == "DVH");

            if (idProp == null || dvhProp == null)
                return errorList;

            foreach (var entity in entities)
            {
                var id = idProp.GetValue(entity);
                var entityCalculated = entitiesCalculated.FirstOrDefault(e => idProp.GetValue(e).Equals(id));

                if (entityCalculated == null)
                    continue;

                var originalDVH = dvhProp.GetValue(entity)?.ToString();
                var calculatedDVH = dvhProp.GetValue(entityCalculated)?.ToString();

                if (originalDVH != calculatedDVH)
                {
                    foreach (var prop in props)
                    {
                        if (!prop.Name.StartsWith("DVH_"))
                            continue;

                        var originalFieldHash = prop.GetValue(entity)?.ToString();
                        var calculatedFieldHash = prop.GetValue(entityCalculated)?.ToString();

                        if (originalFieldHash != calculatedFieldHash)
                        {
                            var propName = prop.Name.Replace("DVH_", "");

                            var errorProp = props.FirstOrDefault(p => p.Name == propName);
                            var value = errorProp.GetValue(entity).ToString();

                            errorList.Add(new DbInconsistencyErrorEntity(tableName, errorProp.Name, value));
                        }
                    }
                }
            }

            return errorList;
        }



    }
}
