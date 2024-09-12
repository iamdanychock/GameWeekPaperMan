using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    /// <summary>
    /// send this checkpoint to the game manager if the player enter it
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            GameManager.Instance.SetLastCheckpoint(this);
        }
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
