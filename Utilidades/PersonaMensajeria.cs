using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TAREA1.Utilidades
{
    public class PersonaMensajeria : ValueChangedMessage<PersonaMensaje>
    {
        public PersonaMensajeria(PersonaMensaje value) : base(value)
        {

        }
    }
}
