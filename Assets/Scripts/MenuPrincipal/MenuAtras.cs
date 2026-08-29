using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Necesario para re-seleccionar botones

public class MenuAtras : MonoBehaviour
{
    public InputActionReference accionAtras;
    public Button botonAtrasEnPantalla;

    [Header("Botón a seleccionar al regresar")]
    public GameObject botonPrincipal; // El botón que recibirá el foco

    void OnEnable()
    {
        accionAtras.action.performed += EjecutarAtras;
    }

    void OnDisable()
    {
        accionAtras.action.performed -= EjecutarAtras;
    }

    private void EjecutarAtras(InputAction.CallbackContext contexto)
    {
        // 1. Ejecuta la animación (el clic virtual)
        if (botonAtrasEnPantalla != null)
        {
            botonAtrasEnPantalla.onClick.Invoke();
        }

        // 2. Obliga al sistema a seleccionar el botón del menú principal
        if (botonPrincipal != null)
        {
            // Limpiamos la selección actual para evitar bugs de Unity
            EventSystem.current.SetSelectedGameObject(null);
            // Asignamos el nuevo botón
            EventSystem.current.SetSelectedGameObject(botonPrincipal);
        }
    }
}