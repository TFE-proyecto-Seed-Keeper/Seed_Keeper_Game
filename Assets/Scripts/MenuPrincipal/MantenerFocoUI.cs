using UnityEngine;
using UnityEngine.EventSystems; // Necesario para controlar la UI

public class MantenerFocoUI : MonoBehaviour
{
    private GameObject ultimoSeleccionado;

    void Start()
    {
        // Guarda el primer botón que configuraste en el EventSystem al iniciar
        ultimoSeleccionado = EventSystem.current.firstSelectedGameObject;
    }

    void Update()
    {
        // Si el jugador tiene un botón seleccionado actualmente, lo guardamos en la memoria
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            ultimoSeleccionado = EventSystem.current.currentSelectedGameObject;
        }
        // Si el jugador hizo clic afuera y la selección se volvió nula (vacía)...
        else if (ultimoSeleccionado != null)
        {
            // Forzamos al sistema a volver a seleccionar el último botón guardado
            EventSystem.current.SetSelectedGameObject(ultimoSeleccionado);
        }
    }
}
