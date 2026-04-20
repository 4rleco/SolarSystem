using UnityEngine;

class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private PlayerController player;
    [SerializeField] private float timer = 10.0f;

    private float currentTime;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timer)
        {
            Asteroid asteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity, transform);
            asteroid.SetTarget(player.transform.position);
            currentTime -= timer;
        }

    }
}
