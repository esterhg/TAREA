using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TAREA1.DataAccess;
using TAREA1.Dtos;
using TAREA1.Utilidades;
using TAREA1.View;
namespace TAREA1.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly PDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<PersonaDTO> listaPersona = new ObservableCollection<PersonaDTO>();

        public MainViewModel(PDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<PersonaMensajeria>(this, (r, m) =>
            {
                PersonaMensajeRecibido(m.Value);
            });
        }
        public async Task Obtener()
        {

          
            var lista = await _dbContext.Personas.ToListAsync();
            if (lista.Any())
            {
              
                foreach (var item in lista)
                {
                    ListaPersona.Add(new PersonaDTO
                    {

                        IdPersonas = item.IdPersonas,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        Correo = item.Correo,
                        Edad = item.Edad,
                        Direccion = item.Direccion,


                    });
                }
            }

        }


        private void PersonaMensajeRecibido(PersonaMensaje PersonaMensaje)
        {
            var PersonaDto = PersonaMensaje.PersonaDto;

            if (PersonaMensaje.EsCrear)
            {
                ListaPersona.Add(PersonaDto);
            }
            else
            {
                var encontrado = ListaPersona
                    .First(e => e.IdPersonas == PersonaDto.IdPersonas);

                encontrado.Nombre = PersonaDto.Nombre;
                encontrado.Apellido = PersonaDto.Apellido;
                encontrado.Correo = PersonaDto.Correo;
                encontrado.Edad = PersonaDto.Edad;
                encontrado.Direccion = PersonaDto.Direccion;

            }

        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(PersonaPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(PersonaDTO PersonaDto)
        {
            var uri = $"{nameof(PersonaPage)}?id={PersonaDto.IdPersonas}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(PersonaDTO PersonaDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el Persona?", "Si", "No");

            if (answer)
            {
                var encontrado = await _dbContext.Personas
                    .FirstAsync(e => e.IdPersonas == PersonaDto.IdPersonas);

                _dbContext.Personas.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaPersona.Remove(PersonaDto);

            }

        }


    }
}

