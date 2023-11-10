using System.ComponentModel.DataAnnotations;
namespace TAREA1.Modelos
{
    public class Personas
    {
        [Key]
        public int IdPersonas { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
    }
}
