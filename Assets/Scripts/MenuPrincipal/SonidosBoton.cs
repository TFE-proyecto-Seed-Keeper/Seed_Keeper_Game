using UnityEngine;
using UnityEngine.EventSystems; // Necesario para detectar la UI

public class SonidosBoton : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerClickHandler, ISubmitHandler
{
    [Header("Arrastra aqui el EventSystem")]
    public AudioSource reproductorGlobal;

    [Header("Tus Sonidos")]
    public AudioClip sonidoNavegar;
    public AudioClip sonidoClic;

    // Se ejecuta al pasar el rat�n por encima
    public void OnPointerEnter(PointerEventData eventData)
    {
        ReproducirNavegar();
    }

    // Se ejecuta al navegar con W/S, flechas o Joystick
    public void OnSelect(BaseEventData eventData)
    {
        ReproducirNavegar();
    }

    // Se ejecuta al hacer Clic con el rat�n
    public void OnPointerClick(PointerEventData eventData)
    {
        ReproducirClic();
    }

    // Se ejecuta al oprimir Enter, la tecla E, o el bot�n A del mando
    public void OnSubmit(BaseEventData eventData)
    {
        ReproducirClic();
    }

    private void ReproducirNavegar()
    {
        if (reproductorGlobal != null && sonidoNavegar != null)
            reproductorGlobal.PlayOneShot(sonidoNavegar);
    }

    private void ReproducirClic()
    {
        if (reproductorGlobal != null && sonidoClic != null)
            reproductorGlobal.PlayOneShot(sonidoClic);
    }
}