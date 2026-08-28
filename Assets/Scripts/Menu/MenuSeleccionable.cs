using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSeleccionableAnim : MonoBehaviour
{
    [Header("Lista de Botones")]
    public Button[] botones;

    [Header("Lista de Contenidos / Paneles")]
    [Tooltip("Arrastra los paneles en el mismo orden que los botones (Panel 0 con Botón 0, etc.)")]
    public GameObject[] panelesContenido;

    [Header("Nombres de Estado en el Animator")]
    public string animNormal = "Normal";
    public string animSelected = "Selected";

    public int IndiceSeleccionado { get; private set; } = -1;

    // Se ejecuta automáticamente cada vez que el menú pasa de inactivo a activo (al abrir el panel)
    private void OnEnable()
    {
        ReiniciarMenu();
    }

    /// <summary>
    /// Reinicia el menú activando el primer botón (índice 0) y su panel correspondiente
    /// </summary>
    public void ReiniciarMenu()
    {
        if (botones == null || botones.Length == 0) return;

        // Limpia el objeto enfocado en el EventSystem para evitar estados fantasma
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        // Fuerza la selección completa (animación + activación de contenido) del primer elemento
        SeleccionarBoton(0);
    }

    public void SeleccionarBoton(int indice)
    {
        if (indice < 0 || indice >= botones.Length) return;

        IndiceSeleccionado = indice;

        // 1. Actualizar el estado visual de los botones
        for (int i = 0; i < botones.Length; i++)
        {
            if (botones[i] == null) continue;

            Animator anim = botones[i].GetComponent<Animator>();
            if (anim == null) continue;

            if (i == IndiceSeleccionado)
            {
                botones[i].interactable = true;
                anim.Play(animSelected, 0, 0f);

                if (EventSystem.current != null)
                {
                    EventSystem.current.SetSelectedGameObject(botones[i].gameObject);
                }
            }
            else
            {
                botones[i].interactable = true;
                anim.Play(animNormal, 0, 0f);
            }
        }

        // 2. Actualizar la visibilidad del contenido asociado
        ActualizarContenido(indice);
    }

    private void ActualizarContenido(int indiceActivo)
    {
        if (panelesContenido == null) return;

        for (int i = 0; i < panelesContenido.Length; i++)
        {
            if (panelesContenido[i] != null)
            {
                // Solo se activa el panel cuyo índice coincide con el botón seleccionado
                panelesContenido[i].SetActive(i == indiceActivo);
            }
        }
    }
}