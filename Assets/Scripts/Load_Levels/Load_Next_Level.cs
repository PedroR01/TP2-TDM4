using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_Next_Level : MonoBehaviour
{
    protected Scene actualScene;

    [SerializeField]
    private GameObject back_Button;

    [Tooltip("Valor que indica a que nivel dirige el boton")]
    public int buttonLevel;

    private void Awake()
    {
        actualScene = SceneManager.GetActiveScene();
    }

    // Funcion para cambiar de nivel
    private void LoadLevel(int levelNumber)
    {
        switch (levelNumber) // Dependiendo del numero recibido por parametro es al nivel que va a cambiar
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

    // Funcion visible desde el inspector. Es la que llama a la funcion para cambiar de nivel.
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

    // Salir de la aplicacion desde el menu.
    public void OnQuit()
    {
        Application.Quit();
    }
}