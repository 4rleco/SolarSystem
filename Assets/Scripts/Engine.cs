using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource audioSource;

    public bool IsActive { get; private set; }

    public void Set(bool state)
    {
        if (IsActive == state)
            return;

        IsActive = state;

        if (IsActive)
        {
            particleSystem.gameObject.SetActive(true);
            audioSource.Play();
        }
        else
        {
            particleSystem.gameObject.SetActive(false);
            audioSource.Stop();
        }
    }
}
