using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
//easy:9x9,10,0.12 normal:16x16,40,0.16 hard:30x16,99,0.21
public class SetDifficultyScript : MonoBehaviour
{
    public GameObject easyText;
    public GameObject normalText;
    public GameObject hardText;
    public GameObject customText;
    public GameObject startButton;
    public GameObject sliderX;
    public GameObject sliderY;
    public GameObject sliderBomb;
    TextMeshProUGUI cText;
    Slider sX;
    Slider sY;
    Slider sB;
    int width;
    int height;
    int bomb;

    void Start()
    {
        easyText.gameObject.SetActive(false);
        normalText.gameObject.SetActive(false);
        hardText.gameObject.SetActive(false);
        customText.gameObject.SetActive(false);
        sliderX.gameObject.SetActive(false);
        sliderY.gameObject.SetActive(false);
        sliderBomb.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        cText = customText.GetComponent<TextMeshProUGUI>();
        sX = sliderX.GetComponent<Slider>();
        sY = sliderY.GetComponent<Slider>();
        sB = sliderBomb.GetComponent<Slider>();
        cText.text = "Custom mode\nField : " + width.ToString() + "x" + height.ToString() + "\nMine : " + bomb.ToString();
        width = 9;
        height = 9;
        bomb = 10;
        sB.minValue = 5 * 5 - 9;
        sB.maxValue = width * height - 9;

    }

    public void OnEasyButtonDown()
    {
        width = 9;
        height = 9;
        bomb = 10;
        easyText.gameObject.SetActive(true);
        normalText.gameObject.SetActive(false);
        hardText.gameObject.SetActive(false);
        customText.gameObject.SetActive(false);
        sliderX.gameObject.SetActive(false);
        sliderY.gameObject.SetActive(false);
        sliderBomb.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    public void OnNormalButtonDown()
    {
        width = 16;
        height = 16;
        bomb = 40;
        easyText.gameObject.SetActive(false);
        normalText.gameObject.SetActive(true);
        hardText.gameObject.SetActive(false);
        customText.gameObject.SetActive(false);
        sliderX.gameObject.SetActive(false);
        sliderY.gameObject.SetActive(false);
        sliderBomb.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    public void OnHardButtonDown()
    {
        width = 30;
        height = 16;
        bomb = 99;
        easyText.gameObject.SetActive(false);
        normalText.gameObject.SetActive(false);
        hardText.gameObject.SetActive(true);
        customText.gameObject.SetActive(false);
        sliderX.gameObject.SetActive(false);
        sliderY.gameObject.SetActive(false);
        sliderBomb.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    public void OnCustomButtonDown()
    {
        easyText.gameObject.SetActive(false);
        normalText.gameObject.SetActive(false);
        hardText.gameObject.SetActive(false);
        customText.gameObject.SetActive(true);
        sliderX.gameObject.SetActive(true);
        sliderY.gameObject.SetActive(true);
        sliderBomb.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }

    public void OnSliderXMove()
    {
        width = (int)sX.value;
        sB.maxValue = width * height - 9;
        sB.value = (int)((width * height)*0.2);
        bomb = (int)sB.value;
        cText.text = "Custom mode\nField : " + width.ToString() + "x" + height.ToString() + "\nMine : " + bomb.ToString();
    }
    public void OnSliderYMove()
    {
        height = (int)sY.value;
        sB.maxValue = width * height - 9;
        sB.value = (int)((width * height)*0.2);
        bomb = (int)sB.value;
        cText.text = "Custom mode\nField : " + width.ToString() + "x" + height.ToString() + "\nMine : " + bomb.ToString();
    }
    public void OnSliderBombMove()
    {
        bomb = (int)sB.value;
        cText.text = "Custom mode\nField : " + width.ToString() + "x" + height.ToString() + "\nMine : " + bomb.ToString();
    }

    public void OnStartButtonDown()
    {
        GameManagerScript.SetDifficulty(width, height, bomb);
        SceneManager.LoadScene("Main");
    }
}
