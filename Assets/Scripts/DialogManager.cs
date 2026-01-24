using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Animator dialogAnimator;

    private bool isInConversation = false;

    void Update()
    {
        // Iniciar conversación
        if (!isInConversation && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation();
        }

        // Terminar conversación
        if (isInConversation && Input.GetKeyDown(KeyCode.Q))
        {
            EndConversation();
        }
    }

    public void StartConversation()
    {
        isInConversation = true;
        dialogAnimator.SetTrigger("StartConversation");

        Debug.Log("Conversación iniciada");
    }

    public void EndConversation()
    {
        isInConversation = false;
        dialogAnimator.SetTrigger("EndConversation");

        Debug.Log("Conversación terminada");
    }
}
