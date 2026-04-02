using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxLives = 3;
    public int currentLivers ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLivers = maxLives;
    }

   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("missile"))
        {
            currentLivers--;
            Destroy(other.gameObject);

            if (currentLivers <= 0)
            {
                GameOver(   );
            }
        }



        void GameOver()
        {
            gameObject.SetActive(false);
            Invoke("RestarGame", 3.0f);
        }

        void RestarGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene(). name);
        }


    }


    void Update()
    {
        
    }
}
