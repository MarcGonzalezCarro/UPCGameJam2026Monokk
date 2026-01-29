using UnityEngine;
using TMPro;

public class Triggers : MonoBehaviour
{
    public GameObject objetoAActivar;
    public Animator animator;
    public TMP_Text textoUi;
    public string text;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetoAActivar.SetActive(true);
            animator.SetTrigger("Entry");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetoAActivar.SetActive(false);
            animator.SetTrigger("Exit");
        }
    }
}
