using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public Transform target;
    public float cooldown = 2f; // Tiempo en segundos entre teletransportes

    private CharacterController controller;
    private bool canTeleport = true; // Controla si se puede teletransportar

    private void OnTriggerEnter(Collider other)
    {
        if (!canTeleport) return; // Salimos si está en cooldown

        controller = other.GetComponent<CharacterController>();
        if (controller != null)
        {
            StartCoroutine(TeleportPlayer(controller, other.gameObject));
        }
    }

    private IEnumerator TeleportPlayer(CharacterController controller, GameObject obj)
    {
        target.GetComponent<Teleport>().canTeleport = false;

        // Teletransportamos
        controller.enabled = false;
        obj.transform.position = target.position;
        controller.enabled = true;

        // Esperamos el cooldown
        yield return new WaitForSeconds(cooldown);

        target.GetComponent<Teleport>().canTeleport = true;
    }
}
