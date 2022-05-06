using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraOffset;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + cameraOffset);
    }
}