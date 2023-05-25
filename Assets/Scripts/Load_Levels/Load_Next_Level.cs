using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_Next_Level : MonoBehaviour
{
    protected Scene actualScene;

    [SerializeField]
    private GameObject back_Button;

    public int buttonLevel;

    private void Awake()
    {
        actualScene = SceneManager.GetActiveScene();
    }

    private void LoadLevel(int levelNumber)
    {
        switch (levelNumber)
        {
            case 0:
                SceneManager.LoadScene("Main_Menu"); back_Button.SetActive(false);
                break;

            case 1:
                SceneManager.LoadScene("Level1");
                back_Button.SetActive(true);
                break;

            case 2:
                SceneManager.LoadScene("Level2");
                back_Button.SetActive(true);
                break;

            default: break;
        }
    }

    public void LevelTrigger()
    {
        LoadLevel(buttonLevel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && actualScene.name == "Level1")
        {
            SceneManager.LoadScene("Main_Menu");
        }

        if (other.gameObject.layer == 10 && actualScene.name == "Level2")
        {
            StartCoroutine(WaitToLoad());
        }
    }

    private IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Main Menu");
    }

    public void OnQuit()
    {
        Debug.Log("as");
        Application.Quit();
    }
}