using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject libreta;
    public List<FaceRecipe> hardcodedFaces;
    private List<FacePage> faceRecipes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            if (libreta.activeInHierarchy) {
                FindFirstObjectByType<FacePagesManager>().SaveCurrentPage();
                faceRecipes = FindFirstObjectByType<FacePagesManager>().pages;
            }
            FindFirstObjectByType<ThirdPersonCamera>().canMove = libreta.activeInHierarchy;
            FindFirstObjectByType<ThirdPersonMovement>().canMove = libreta.activeInHierarchy;
            libreta.SetActive(!libreta.activeInHierarchy);
            
        }
    }
    public bool CheckFaceByName(string faceName)
    {
        // Buscar cara hardcodeada
        FaceRecipe target = hardcodedFaces.Find(f => f.faceName == faceName);
        if (target == null)
        {
            Debug.LogWarning("No existe cara hardcodeada con nombre: " + faceName);
            return false;
        }
        foreach (var p in faceRecipes)
        {
            Debug.Log("Player pageName: [" + p.pageName + "]");
        }
        // Buscar página del jugador
        FacePage playerPage = faceRecipes.Find(p => p.pageName == faceName);
        if (playerPage == null)
        {
            Debug.LogWarning("No existe página del jugador con nombre: " + faceName);
            return false;
        }

        FaceState current = playerPage.faceState;

        Debug.Log("voy a comparar: " + current.ojos + "con" + target.ojos);
        Debug.Log("voy a comparar: " + current.nariz + "con" + target.nariz);
        Debug.Log("voy a comparar: " + current.boca + "con" + target.boca);
        Debug.Log("voy a comparar: " + current.cejas + "con" + target.cejas);

        return
            current.ojos == target.ojos &&
            current.nariz == target.nariz &&
            current.boca == target.boca &&
            current.cejas == target.cejas;
    }

}
