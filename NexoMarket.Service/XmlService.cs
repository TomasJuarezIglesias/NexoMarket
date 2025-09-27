using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace NexoMarket.Service
{
    public class XmlService
    {
        private const string folder = "/Xml";

        /// <summary>
        /// Serializa un objeto a XML y lo guarda en el path indicado.
        /// Si el archivo existe, lo sobrescribe.
        /// </summary>
        public void SaveXml<T>(string filePath, T data)
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory + folder;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fullPath = Path.Combine(directoryPath, filePath);

            if (string.IsNullOrWhiteSpace(fullPath))
                throw new ArgumentException("El path no puede ser vacío.", nameof(filePath));

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var writer = new StreamWriter(fullPath, false))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al guardar el archivo XML.", ex);
            }
        }

        /// <summary>
        /// Lee un archivo XML y lo deserializa al tipo indicado.
        /// </summary>
        public T LoadXml<T>(string filePath) where T : class
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + folder, filePath);

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
                return null;
            }
        }

        /// <summary>
        /// Elimina un archivo XML puntual.
        /// </summary>
        public void DeleteXml(string filePath)
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + folder, filePath);

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("El path no puede ser vacío.", nameof(filePath));

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar el archivo XML: {filePath}", ex);
            }
        }

        /// <summary>
        /// Elimina todos los archivos con extensión .xml en el directorio base de la aplicación.
        /// </summary>
        public void DeleteAllXml()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory + folder;

            try
            {
                var xmlFiles = Directory.GetFiles(baseDir, "*.xml", SearchOption.TopDirectoryOnly);

                foreach (var file in xmlFiles)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar todos los archivos XML.", ex);
            }
        }
    }
}
