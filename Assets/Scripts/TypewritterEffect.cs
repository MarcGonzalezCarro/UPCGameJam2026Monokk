using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text dialogoTextDer;      // Texto donde se mostrará
    public TMP_Text dialogoTextIzq;
    public float typingSpeed = 0.05f; // Velocidad de escritura

    private string fullText;           // Texto completo de la línea
    private Coroutine typingCoroutine; // Referencia para detener corutina
    private bool isTyping = false;     // ¿Se está escribiendo?
    private bool skipRequested = false; // ¿Se pidió saltar la escritura?

    // Llama a esto para mostrar un texto
    public void ShowText(string text)
    {
        fullText = text;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        dialogoTextDer.text = "";
        dialogoTextIzq.text = "";

        foreach (char c in fullText)
        {
            if (skipRequested)
            {
                dialogoTextDer.text = fullText;
                dialogoTextIzq.text = fullText;
                break;
            }

            dialogoTextDer.text += c;
            dialogoTextIzq.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        skipRequested = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                skipRequested = true;
            }
            else
            {
                FindFirstObjectByType<DialogueController>().Next();
            }
        }
    }
}
