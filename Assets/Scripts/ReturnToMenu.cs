using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) { GameManager.WinLevel(); }
    }
}