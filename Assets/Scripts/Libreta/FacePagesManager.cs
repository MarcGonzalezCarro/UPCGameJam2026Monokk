using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FacePagesManager : MonoBehaviour
{
    [Header("Referencias")]
    public FacePreviewController preview;
    public TMP_Text pageNameText;
    // TMP_Text si usas TextMeshPro

    [Header("Páginas")]
    public List<FacePage> pages = new List<FacePage>();

    private int currentPageIndex = 0;

    void Start()
    {
        // Crear páginas si no hay ninguna
        if (pages.Count == 0)
        {
            pages.Add(new FacePage("Cara 1"));
            pages.Add(new FacePage("Cara 2"));
        }

        LoadPage(0);
    }

    void LoadPage(int index)
    {
        index = Mathf.Clamp(index, 0, pages.Count - 1);

        // Guardar estado actual
        if (pages[currentPageIndex].faceState != null)
        {
            pages[currentPageIndex].faceState = preview.GetCurrentState();
        }

        currentPageIndex = index;

        // Cargar nueva página
        preview.ApplyState(pages[currentPageIndex].faceState);

        // Actualizar texto
        pageNameText.text = pages[currentPageIndex].pageName;
    }

    public void NextPage()
    {
        if (currentPageIndex < pages.Count - 1)
            LoadPage(currentPageIndex + 1);
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
            LoadPage(currentPageIndex - 1);
    }
}
