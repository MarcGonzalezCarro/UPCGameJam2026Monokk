using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena
using System.Collections;

public class SplashManager : MonoBehaviour
{
    public float tiempoDeEspera = 3f; // Segundos que durará el logo
    public string nombreDeTuMenu = "NombreDeTuEscenaMenu"; // Escribe el nombre exacto de tu escena de menú

    void Start()
    {
        // Al empezar, arranca una cuenta atrás
        StartCoroutine(PasarAlMenu());
    }

    IEnumerator PasarAlMenu()
    {
        // Espera el tiempo que definas
        yield return new WaitForSeconds(tiempoDeEspera);

        // Carga la escena del menú
        SceneManager.LoadScene(nombreDeTuMenu);
    }
}