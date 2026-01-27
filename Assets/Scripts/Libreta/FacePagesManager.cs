using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class FacePagesManager : MonoBehaviour
{
    [Header("Referencias")]
    public FacePreviewController preview;
    public TMP_Text pageNameText;

    [Header("Post-its (Textos)")]
    public TMP_Text noteAText;
    public TMP_Text noteBText;
    public TMP_Text noteCText;

    [Header("Páginas")]
    public List<FacePage> pages = new List<FacePage>();

    private int currentPageIndex = 0;
    private bool hasLoadedFirstPage = false;

    void Start()
    {
        if (pages.Count == 0)
        {
            pages.Add(new FacePage("Cara 1"));
            pages.Add(new FacePage("Cara 2"));
        }

        LoadPage(0);
    }

    void SaveCurrentPage()
    {
        pages[currentPageIndex].faceState = preview.GetCurrentState();

        pages[currentPageIndex].noteA = noteAText.text;
        pages[currentPageIndex].noteB = noteBText.text;
        pages[currentPageIndex].noteC = noteCText.text;
    }

    void LoadPage(int index)
    {
        index = Mathf.Clamp(index, 0, pages.Count - 1);

        if (hasLoadedFirstPage)
        {
            SaveCurrentPage();
        }

        hasLoadedFirstPage = true;

        currentPageIndex = index;

        preview.ApplyState(pages[currentPageIndex].faceState);
        pageNameText.text = pages[currentPageIndex].pageName;

        noteAText.text = pages[currentPageIndex].noteA;
        noteBText.text = pages[currentPageIndex].noteB;
        noteCText.text = pages[currentPageIndex].noteC;
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
