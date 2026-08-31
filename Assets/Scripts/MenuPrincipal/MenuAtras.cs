using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuAtras : MonoBehaviour
{
    public InputActionReference accionAtras;
    public Button botonAtrasEnPantalla;
    public GameObject botonPrincipal;

    [Header("Sonido de Atrás")]
    public AudioSource reproductorGlobal;
    public AudioClip sonidoAtras;

    void OnEnable() { accionAtras.action.performed += EjecutarAtras; }
    void OnDisable() { accionAtras.action.performed -= EjecutarAtras; }

    private void EjecutarAtras(InputAction.CallbackContext contexto)
    {
        // Reproducimos el sonido especial de "Atrás"
        if (reproductorGlobal != null && sonidoAtras != null)
        {
            reproductorGlobal.PlayOneShot(sonidoAtras);
        }

        if (botonAtrasEnPantalla != null)
            botonAtrasEnPantalla.onClick.Invoke();

        if (botonPrincipal != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(botonPrincipal);
        }
    }
}