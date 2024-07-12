using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSnap_Generic : MonoBehaviour
{
    [SerializeField] Slider slider;     //スライダーのゲームオブジェクトをいれる
    [SerializeField] bool isSliderX;
    [SerializeField] GameObject DifficultyManager;

    [Header("スライダーを分割する数")][SerializeField] int sliderDivNum;  //スライダーを分割する数（分割数）
    [Header("スライダーをゼロ位置にすることを許可")][SerializeField] bool usingZero;    //ゼロを使うか否か
    [Header("スライダーの最大値")][SerializeField] float maxvalue;
    [Header("スライダーの最小値")][SerializeField] float minvalue;
    
    SetDifficultyScript setDifficulty;

    float divAmount;    //1分割あたり分割量



    void Start()
    {
        setDifficulty = DifficultyManager.GetComponent<SetDifficultyScript>();
        slider.maxValue = maxvalue;  //スライダーの最大値をmaxvalueにする
        slider.minValue = minvalue;  //スライダーの最小値をminvalueにする
        divAmount = (maxvalue - minvalue) / (sliderDivNum - 1);  //単位あたり分割量を定義
    }

    public void SliderValueChanged()
    {
        for (int i = 0; i <= sliderDivNum; i++)
        {
            if (i * divAmount >= slider.value && slider.value > (i - 1) * divAmount)
            {
                slider.value = i * divAmount;
            }
        }

        if (slider.value == minvalue)
        {
            if (usingZero == false)
            {
                slider.value = minvalue + divAmount;
            }
            else
            {
                slider.value = minvalue;
            }
        }

        if (isSliderX)
        {
            setDifficulty.OnSliderXMove();
        }
        else
        {
            setDifficulty.OnSliderYMove();
        }
    }
}
