using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject libreta;
    public List<FaceRecipe> hardcodedFaces;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
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

        // Buscar página del jugador
        FacePage playerPage = FindFirstObjectByType<FacePagesManager>().pages.Find(p => p.pageName == faceName);
        if (playerPage == null)
        {
            Debug.LogWarning("No existe página del jugador con nombre: " + faceName);
            return false;
        }

        FaceState current = playerPage.faceState;

        return
            current.ojos == target.ojos &&
            current.nariz == target.nariz &&
            current.boca == target.boca &&
            current.cejas == target.cejas;
    }

}
