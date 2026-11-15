using UnityEngine;
using UnityEngine.EventSystems; // Necesario para controlar la UI

public class MantenerFocoUI : MonoBehaviour
{
    private GameObject ultimoSeleccionado;

    void Start()
    {
        // Guarda el primer bot�n que configuraste en el EventSystem al iniciar
        ultimoSeleccionado = EventSystem.current.firstSelectedGameObject;
    }

    void Update()
    {
        // Si el jugador tiene un botón seleccionado actualmente, lo guardamos en la memoria
        if (EventSystem.current.currentSelectedGameObject)
        {
            ultimoSeleccionado = EventSystem.current.currentSelectedGameObject;
        }
        // Si el jugador hizo clic afuera y la selección se volvio nula (vacia)...
        else if (ultimoSeleccionado)
        {
            // Forzamos al sistema a volver a seleccionar el ultimo botón guardado
            EventSystem.current.SetSelectedGameObject(ultimoSeleccionado);
        }
    }
}
