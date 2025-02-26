using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Vector3 _playerPos = new();

    private void Start()
    {
        _playerPos = FindObjectOfType<PlayerController>().transform.position;
    }

    private void Update()
    {
        if (_playerPos.z > transform.position.z + 1)
        {
            if (!TryGetComponent(out Rigidbody rb))
                rb = gameObject.AddComponent<Rigidbody>();
            rb.AddForce(Vector3.down, ForceMode.Impulse);
            Destroy(gameObject, 1.0f);
        }
    }
}
