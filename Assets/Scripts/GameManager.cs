using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager : MonoBehaviour
{
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
