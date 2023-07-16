using System;
using System.Collections.Generic;

#nullable disable

namespace LabWeb05_APICore.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
