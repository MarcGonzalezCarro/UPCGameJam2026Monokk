using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject libreta;
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
}
