using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    public void SalirDelJuego()
    {
        // Muestra un mensaje en la consola para confirmar que el botón funciona
        Debug.Log("Saliendo del juego...");

        // Cierra la aplicación (funciona en el juego final .exe / .apk)
        Application.Quit();

        // Esta parte es solo para que funcione también mientras pruebas en el Editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}