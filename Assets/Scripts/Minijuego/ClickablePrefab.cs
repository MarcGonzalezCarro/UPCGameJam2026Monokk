using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickablePrefab : MonoBehaviour
{
    public float expandScale = 1.2f;
    public float expandDuration = 0.2f;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        StartCoroutine(ExpandAnimation());
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    IEnumerator ExpandAnimation()
    {
        Vector3 originalScale = rectTransform.localScale;
        Vector3 targetScale = originalScale * expandScale;
        float timer = 0f;

        while (timer < expandDuration)
        {
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, timer / expandDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = targetScale;
    }

    void OnClicked()
    {
        MiniGameUIManager.Instance.RegisterClick();
        Destroy(gameObject);
    }
}
