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

    public void SaveCurrentPage()
    {
        pages[currentPageIndex].faceState = preview.GetCurrentState();

    }

    void LoadPage(int index)
    {
        index = Mathf.Clamp(index, 0, pages.Count - 1);

        if (hasLoadedFirstPage)
            SaveCurrentPage();

        hasLoadedFirstPage = true;

        currentPageIndex = index;
        FacePage page = pages[currentPageIndex];

        preview.ApplyState(page.faceState);
        pageNameText.text = page.pageName;

        ApplyNoteVisibility(page);
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

    public void AddPage(string name) {
        pages.Add(new FacePage(name));
    }
    void ApplyNoteVisibility(FacePage page)
    {
        noteAText.gameObject.SetActive(page.noteAUnlocked);
        noteBText.gameObject.SetActive(page.noteBUnlocked);
        noteCText.gameObject.SetActive(page.noteCUnlocked);

        if (page.noteAUnlocked) noteAText.text = page.noteA;
        if (page.noteBUnlocked) noteBText.text = page.noteB;
        if (page.noteCUnlocked) noteCText.text = page.noteC;
    }

    public void UnlockNote(string pageName, int noteIndex, string text = "")
    {
        FacePage page = pages.Find(p => p.pageName == pageName);

        if (page == null)
        {
            Debug.LogWarning("Página no encontrada: " + pageName);
            return;
        }

        switch (noteIndex)
        {
            case 0:
                page.noteAUnlocked = true;
                if (!string.IsNullOrEmpty(text)) page.noteA = text;
                break;

            case 1:
                page.noteBUnlocked = true;
                if (!string.IsNullOrEmpty(text)) page.noteB = text;
                break;

            case 2:
                page.noteCUnlocked = true;
                if (!string.IsNullOrEmpty(text)) page.noteC = text;
                break;

            default:
                Debug.LogWarning("Índice de nota inválido");
                return;
        }

        // Si la página está abierta, refrescamos UI
        if (pages[currentPageIndex] == page)
            ApplyNoteVisibility(page);
    }
}
