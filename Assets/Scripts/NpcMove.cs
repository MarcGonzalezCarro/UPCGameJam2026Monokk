using UnityEngine;

public class NPCMovimientoHorizontal : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 2f;
    public float distanciaEjeX = 2f; // Cuánto se aleja del centro

    private Animator animator;
    private Vector3 posicionInicial;
    private float puntoDerecho;
    private float puntoIzquierdo;
    private bool moviendoADerecha = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        posicionInicial = transform.position;

        // Calculamos los límites basados en la posición inicial
        puntoDerecho = posicionInicial.x + distanciaEjeX;
        puntoIzquierdo = posicionInicial.x - distanciaEjeX;
    }

    void Update()
    {
        MoverNPC();
    }

    void MoverNPC()
    {
        float posicionActualX = transform.position.x;

        // Decidir dirección
        if (posicionActualX >= puntoDerecho) moviendoADerecha = false;
        if (posicionActualX <= puntoIzquierdo) moviendoADerecha = true;

        // Calcular dirección (-1 o 1)
        float direccionX = moviendoADerecha ? 1f : -1f;

        // Mover el objeto
        transform.Translate(Vector3.right * direccionX * velocidad * Time.deltaTime);

        // ACTUALIZAR EL BLEND TREE
        // Enviamos el valor al parámetro "Horizontal" de tu Animator
        animator.SetFloat("Horizontal", direccionX);
    }
}