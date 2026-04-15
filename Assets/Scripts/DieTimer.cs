using UnityEngine;

class DieTimer : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}