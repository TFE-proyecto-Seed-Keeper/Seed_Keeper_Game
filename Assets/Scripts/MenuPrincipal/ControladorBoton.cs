using System.Collections;
using UnityEngine;

public class ControladorBoton : MonoBehaviour
{
    public Animator miAnimator;
    public GameObject miMenu;
    public string nombreDeLaAnimacion = "NombreDeTuEstado";
    public float segundosDeEspera = 2f;

    public void ActivarTodo()
    {
        // 1. Reproduce la animación inmediatamente
        miAnimator.Play(nombreDeLaAnimacion);

        // 2. Inicia el cronómetro
        StartCoroutine(Cronometro());
    }

    private IEnumerator Cronometro()
    {
        // Espera los segundos que le indiques
        yield return new WaitForSeconds(segundosDeEspera);

        // Enciende el menú
        miMenu.SetActive(true);
    }
}