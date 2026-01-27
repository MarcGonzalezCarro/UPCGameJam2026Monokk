using UnityEngine;

public class ClickablePrefab : MonoBehaviour
{
    public float expandScale = 1.2f; // cuánto crece al aparecer
    public float expandDuration = 0.2f; // duración de la animación

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(ExpandAnimation());
    }

    System.Collections.IEnumerator ExpandAnimation()
    {
        float timer = 0f;
        Vector3 targetScale = originalScale * expandScale;

        while (timer < expandDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / expandDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private void OnMouseDown()
    {
        // Destruye al hacer click
        Destroy(gameObject);

        // Avisamos al manager
        MiniGameManager.Instance.RegisterClick();
    }
}

