using UnityEngine;
using UnityEngine.EventSystems;

public class BotonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuSeleccionableAnim manager;
    public int indiceBoton;
    public string animHighlighted = "Highlighted";
    public string animNormal = "Normal";

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Solo reproduce Hover si NO es el botón actualmente seleccionado
        if (manager != null && manager.IndiceSeleccionado != indiceBoton)
        {
            anim.Play(animHighlighted);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Al salir el mouse, regresa a Normal solo si NO es el seleccionado
        if (manager != null && manager.IndiceSeleccionado != indiceBoton)
        {
            anim.Play(animNormal);
        }
    }
}