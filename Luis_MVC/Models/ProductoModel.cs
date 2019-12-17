using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Luis_MVC.Models
{
    public class ProductoModel
    {
        public int idProducto { get; set; }

        [DisplayName("Nombre del Producto")]
        public string nombre { get; set; }

        [DisplayName("Precio")]
        public decimal precio { get; set; }

        [DisplayName("Cantidad")]
        public int cantidad { get; set; }
    }
}