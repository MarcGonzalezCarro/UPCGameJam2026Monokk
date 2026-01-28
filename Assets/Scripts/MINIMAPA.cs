using UnityEngine;

public class ControladorMapa : MonoBehaviour
{
    [Header("Configuración de UI")]
    [Tooltip("Arrastra aquí el objeto del Canvas que contiene el Mapa")]
    public GameObject objetoMapa;

    [Header("Ajustes de Juego")]
    public bool pausarAlAbrir = true;

    private bool jugadorCerca = false;

    void Update()
    {
        // Detectamos si el jugador está en el área y presiona la tecla M
        if (jugadorCerca && Input.GetKeyDown(KeyCode.M))
        {
            ToggleMapa();
        }
    }

    private void ToggleMapa()
    {
        // 1. Verificación de seguridad para evitar el error de "UnassignedReference"
        if (objetoMapa == null)
        {
            Debug.LogError("ERROR: No has asignado el objeto del Mapa en el Inspector de la señal.");
            return;
        }

        // 2. Invertir el estado (si está activo se apaga, y viceversa)
        bool nuevoEstado = !objetoMapa.activeSelf;
        objetoMapa.SetActive(nuevoEstado);

        // 3. Manejo de pausa (opcional)
        if (pausarAlAbrir)
        {
            Time.timeScale = nuevoEstado ? 0f : 1f;
        }
    }

    // Se activa cuando el jugador entra en el Sphere Collider (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Jugador detectado: Presiona M para abrir el mapa.");
        }
    }

    // Se activa cuando el jugador sale del Sphere Collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            // Si el jugador se va, nos aseguramos de cerrar el mapa y reanudar el tiempo
            if (objetoMapa != null)
            {
                objetoMapa.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}