using System;
using System.IO;
using System.Xml.Serialization;

namespace NexoMarket.Service
{
    public class XmlService
    {
        /// <summary>
        /// Serializa un objeto a XML y lo guarda en el path indicado.
        /// Si el archivo existe, lo sobrescribe.
        /// </summary>
        public void SaveXml<T>(string filePath, T data)
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("El path no puede ser vacío.", nameof(filePath));

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var writer = new StreamWriter(filePath, false))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                // Podés loguear acá con tu ZeBoxLog u otro logger
                throw new InvalidOperationException("Error al guardar el archivo XML.", ex);
            }
        }

        /// <summary>
        /// Lee un archivo XML y lo deserializa al tipo indicado.
        /// </summary>
        public T LoadXml<T>(string filePath) where T : class
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("El path no puede ser vacío.", nameof(filePath));

            if (!File.Exists(filePath))
                return null;

            var content = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(content))
                return null;

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var reader = new StreamReader(filePath))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                // Podés decidir: devolver null en lugar de excepción
                // return null;
                throw new InvalidOperationException("Error al leer el archivo XML.", ex);
            }
        }
    }
}
