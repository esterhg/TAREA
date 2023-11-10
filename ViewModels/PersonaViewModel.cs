
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using TAREA1.DataAccess;
using TAREA1.Dtos;
using TAREA1.Modelos;
using TAREA1.Utilidades;


namespace TAREA1.ViewModels
{
    public partial class PersonaViewModel : ObservableObject, IQueryAttributable
    {
        private readonly PDbContext _dbContext;

        [ObservableProperty]
        private PersonaDTO personaDTO = new PersonaDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int IdPersona;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public PersonaViewModel(PDbContext context)
        {
            _dbContext = context;

        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IdPersona = id;

            if (IdPersona == 0)
            {
                TituloPagina = "Nueva Persona";
            }
            else
            {
                TituloPagina = "Editar Persona";
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.Personas.FirstAsync(e => e.IdPersonas == IdPersona);
                    PersonaDTO.IdPersonas = encontrado.IdPersonas;
                    PersonaDTO.Nombre = encontrado.Nombre;
                    PersonaDTO.Apellido = encontrado.Apellido;
                    PersonaDTO.Correo = encontrado.Correo;
                    PersonaDTO.Edad = encontrado.Edad;
                    PersonaDTO.Direccion = encontrado.Direccion;


                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });
                });
            }
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            PersonaMensaje mensaje = new PersonaMensaje();

            await Task.Run(async () =>
            {
                if (IdPersona == 0)
                {
                    var tbPersona = new Personas
                    {
                        Nombre = PersonaDTO.Nombre,
                        Apellido = PersonaDTO.Apellido,
                        Correo = PersonaDTO.Correo,
                        Edad = PersonaDTO.Edad,
                        Direccion = PersonaDTO.Direccion,


                    };

                    _dbContext.Personas.Add(tbPersona);
                    await _dbContext.SaveChangesAsync();

                    PersonaDTO.IdPersonas = tbPersona.IdPersonas;
                    mensaje = new PersonaMensaje()
                    {
                        EsCrear = true,
                        PersonaDto = PersonaDTO
                    };
                    
                }
                else
                {
                    var encontrado = await _dbContext.Personas.FirstAsync(e => e.IdPersonas == IdPersona);
                    encontrado.Nombre = PersonaDTO.Nombre;
                    encontrado.Apellido = PersonaDTO.Apellido;
                    encontrado.Correo = PersonaDTO.Correo;
                    encontrado.Edad = PersonaDTO.Edad;
                    encontrado.Direccion = PersonaDTO.Direccion;



                    await _dbContext.SaveChangesAsync();

                    mensaje = new PersonaMensaje()
                    {
                        EsCrear = false,
                        PersonaDto = PersonaDTO
                    };

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new PersonaMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                   
                });

            });
        }


    }
}