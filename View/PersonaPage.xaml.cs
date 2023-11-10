namespace TAREA1.View;
using TAREA1.ViewModels;

public partial class PersonaPage : ContentPage
{
    public PersonaPage(PersonaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}