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
        if (_playerPos.z > transform.position.z)
        {
            Debug.Log($"{_playerPos} / {transform.position}");
            Destroy(gameObject);
        }
    }
}
