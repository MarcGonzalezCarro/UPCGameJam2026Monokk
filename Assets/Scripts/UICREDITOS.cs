using UnityEngine;

public class ControladorCreditos : MonoBehaviour
{
    // Arrastra aquí tu Imagen o Panel de Créditos desde el inspector
    public GameObject objetoCreditos;

    // Asigna esta función al botón
    public void Alternar()
    {
        // Si está activo lo desactiva, y si está desactivado lo activa
        objetoCreditos.SetActive(!objetoCreditos.activeSelf);
    }
}