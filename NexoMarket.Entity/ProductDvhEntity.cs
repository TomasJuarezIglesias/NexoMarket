using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity
{
    public class ProductDvhEntity
    {
        public int Id { get; set; }
        public int Id_Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public byte[] Imagen { get; set; }
        public string DVH_Id_Categoria { get; set; }
        public string DVH_Nombre { get; set; }
        public string DVH_Descripcion { get; set; }
        public string DVH_Precio { get; set; }
        public string DVH_Stock { get; set; }
        public string DVH_Imagen { get; set; }
        public string DVH { get; set; }
    }
}
