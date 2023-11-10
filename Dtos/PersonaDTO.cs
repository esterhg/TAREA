using CommunityToolkit.Mvvm.ComponentModel;

namespace TAREA1.Dtos
{
    public partial class PersonaDTO : ObservableObject
    {
        [ObservableProperty]
        public int idPersonas;
        [ObservableProperty]
        public string nombre;
        [ObservableProperty]
        public string apellido;
        [ObservableProperty]
        public int edad;
        [ObservableProperty]
        public string correo;
        [ObservableProperty]
        public string direccion;
    }
}
