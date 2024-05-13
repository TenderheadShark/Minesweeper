using UnityEngine;
using UnityEngine.SceneManagement;
//easy:9x9,10,0.12 normal:16x16,40,0.16 hard:30x16,99,0.21
public class SetDifficultyScript : MonoBehaviour
{
    public GameObject easyText;
    public GameObject normalText;
    public GameObject hardText;
    public GameObject startButton;

    void Start()
    {
        easyText.gameObject.SetActive(false);
        normalText.gameObject.SetActive(false);
        hardText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    public void OnEasyButtonDown()
    {
        GameManagerScript.SetDifficulty(9, 9, 10);
        easyText.gameObject.SetActive(true);
        normalText.gameObject.SetActive(false);
        hardText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    public void OnNormalButtonDown()
    {
        GameManagerScript.SetDifficulty(16, 16, 40);
        easyText.gameObject.SetActive(false);
        normalText.gameObject.SetActive(true);
        hardText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    public void OnHardButtonDown()
    {
        GameManagerScript.SetDifficulty(30, 16, 99);
        easyText.gameObject.SetActive(false);
        normalText.gameObject.SetActive(false);
        hardText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }
    
    public void OnStartButtonDown()
    {
        SceneManager.LoadScene("Main");
    }
}
