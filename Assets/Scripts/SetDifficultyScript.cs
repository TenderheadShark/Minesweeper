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
        width = 9;
        height = 9;
        bomb = 10;
        sB.maxValue = width * height - 9;
    }

    void DisplayText(int difficulty)
    {
        easyText.gameObject.SetActive(false);
        normalText.gameObject.SetActive(false);
        hardText.gameObject.SetActive(false);
        customText.gameObject.SetActive(false);
        sliderX.gameObject.SetActive(false);
        sliderY.gameObject.SetActive(false);
        sliderBomb.gameObject.SetActive(false);
        switch (difficulty)
        {
            case 0: easyText.gameObject.SetActive(true);
                    break;
            case 1: normalText.gameObject.SetActive(true);
                    break;
            case 2: hardText.gameObject.SetActive(true);
                    break;
            case 3: customText.gameObject.SetActive(true);
                    sliderX.gameObject.SetActive(true);
                    sliderY.gameObject.SetActive(true);
                    sliderBomb.gameObject.SetActive(true);
                    break;
        }
        startButton.gameObject.SetActive(true);
    }

    public void OnEasyButtonDown()
    {
        width = 9;
        height = 9;
        bomb = 10;
        DisplayText(0);
    
    }

    public void OnNormalButtonDown()
    {
        width = 16;
        height = 16;
        bomb = 40;
        DisplayText(1);
    }

    public void OnHardButtonDown()
    {
        width = 30;
        height = 16;
        bomb = 99;
        DisplayText(2);
    }

    public void OnCustomButtonDown()
    {
        sX.value = width;
        sY.value = height;
        sB.value = bomb;
        sB.maxValue = width * height - 9;
        CustomText();
        DisplayText(3);
    }

    public void OnSliderXMove()
    {
        width = (int)sX.value;
        BombValueFromWidthHeight();
    }

    public void OnSliderYMove()
    {
        height = (int)sY.value;
        BombValueFromWidthHeight();
    }

    public void OnSliderBombMove()
    {
        bomb = (int)sB.value;
        CustomText();
    }

    void BombValueFromWidthHeight()
    {
        sB.maxValue = width * height - 9;
        sB.value = (int)((width * height)*0.2);
        bomb = (int)sB.value;
        CustomText();
    }

    void CustomText()
    {
        cText.text = "Custom mode\nField : " + width.ToString() 
        + "x" + height.ToString() + "\nMine : " + bomb.ToString()
        + "\nMine% : " + ((bomb*100.0f/(width*height))).ToString("0") + "%";
    }

    public void OnStartButtonDown()
    {
        GameManagerScript.SetDifficulty(width, height, bomb);
        SceneManager.LoadScene("Main");
    }
}
