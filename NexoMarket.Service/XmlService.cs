using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace NexoMarket.Service
{
    public class XmlService
    {
        private const string folder = "/Xml";
        private const string fileName = "carrito.xml";

        private string GetFullPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory + folder, fileName);
        }

        /// Serializa un objeto a XML y lo guarda en el path indicado. Si el archivo existe, lo sobrescribe.
        public void SaveXml(List<ProductoCarritoEntity> productos)
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory + folder;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string fullPath = GetFullPath();

            using (XmlTextWriter writer = new XmlTextWriter(fullPath, System.Text.Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteStartElement("ArrayOfProductoCarritoEntity");

                foreach (var item in productos)
                {
                    writer.WriteStartElement("ProductoCarritoEntity");

                    writer.WriteStartElement("Product");
                    writer.WriteElementString("Id", item.Product.Id.ToString());
                    writer.WriteElementString("Id_Categoria", item.Product.Id_Categoria.ToString());
                    writer.WriteElementString("Nombre", item.Product.Nombre);
                    writer.WriteElementString("Descripcion", item.Product.Descripcion);
                    writer.WriteElementString("Precio", item.Product.Precio.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteElementString("Stock", item.Product.Stock.ToString());
                    writer.WriteElementString("Imagen", Convert.ToBase64String(item.Product.Imagen ?? new byte[0]));
                    writer.WriteEndElement(); // Product

                    writer.WriteElementString("Cantidad", item.Cantidad.ToString());

                    writer.WriteEndElement(); // ProductoCarritoEntity
                }

                writer.WriteEndElement(); // ArrayOfProductoCarritoEntity
                writer.WriteEndDocument();
                writer.Close();
            }
        }


        /// Lee el archivo XML y lo deserializa
        public List<ProductoCarritoEntity> LoadXml()
        {
            string fullPath = GetFullPath();
            var lista = new List<ProductoCarritoEntity>();

            if (!File.Exists(fullPath))
                return lista;

            using (XmlTextReader reader = new XmlTextReader(fullPath))
            {
                ProductoCarritoEntity carritoItem = null;
                ProductoEntity producto = null;

                while (reader.Read())
                {
                    reader.MoveToElement();

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "ProductoCarritoEntity":
                                carritoItem = new ProductoCarritoEntity();
                                break;
                            case "Product":
                                producto = new ProductoEntity();
                                break;
                            case "Id":
                                producto.Id = reader.ReadElementContentAsInt();
                                break;
                            case "Id_Categoria":
                                producto.Id_Categoria = reader.ReadElementContentAsInt();
                                break;
                            case "Nombre":
                                producto.Nombre = reader.ReadElementContentAsString();
                                break;
                            case "Descripcion":
                                producto.Descripcion = reader.ReadElementContentAsString();
                                break;
                            case "Precio":
                                producto.Precio = reader.ReadElementContentAsDecimal();
                                break;
                            case "Stock":
                                producto.Stock = reader.ReadElementContentAsInt();
                                break;
                            case "Imagen":
                                string base64 = reader.ReadElementContentAsString();
                                producto.Imagen = string.IsNullOrEmpty(base64)
                                    ? new byte[0]
                                    : Convert.FromBase64String(base64);
                                break;
                            case "Cantidad":
                                carritoItem.Cantidad = reader.ReadElementContentAsInt();
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == "Product")
                        {
                            carritoItem.Product = producto;
                        }
                        else if (reader.Name == "ProductoCarritoEntity" && carritoItem != null)
                        {
                            lista.Add(carritoItem);
                        }
                    }
                }

                reader.Close();
            }

            return lista;
        }
    
        /// Elimina un archivo XML puntual.
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


        /// Elimina todos los archivos con extensión .xml en el directorio base de la aplicación.
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
