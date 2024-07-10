using UnityEngine;

public class CellManagerScript : MonoBehaviour
{
    TextMesh numberText;
    GameManagerScript gameManager;
    public int mineCount = 0;
    public bool isMine;
    public bool isOpen;
    public bool isFlag;
    bool isQuestion;
    int vPosition;
    int hPosition;
    int rightClickLoop;
    public GameObject flagSprite;
    public GameObject mineSprite;
    public GameObject coverSprite;
    public GameObject boomSprite;
    public GameObject questionSprite;

    void Start()
    {
        isMine = false;
        isOpen = false;
        isFlag = false;
        isQuestion = false;
        rightClickLoop = 0;
        numberText = GetComponentInChildren<TextMesh>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        string[] cellPos = this.transform.name.Split(',');
        vPosition = int.Parse(cellPos[0]);
        hPosition = int.Parse(cellPos[1]);
    }

    public void LeftClicked()
    {
        if ((!isOpen && !isFlag) && !isQuestion)
        {
            isOpen = true;
            coverSprite.gameObject.SetActive(false);
            gameManager.OpenCell(vPosition, hPosition, mineCount, isMine);
        }
        else if (isOpen)
        {
            gameManager.AOpen(vPosition, hPosition, mineCount);
        }
    }

    public void Around()
    {
        if ((!isOpen && !isFlag) && !isQuestion)
        {
            isOpen = true;
            coverSprite.gameObject.SetActive(false);
            gameManager.OpenCell(vPosition, hPosition, mineCount, isMine);
        }
    }

    public void InitLeftClicked()
    {
        gameManager.SetMine(vPosition, hPosition);
    }

    public void RightClicked()
    {
        if (!isOpen)
        {
            switch (rightClickLoop)
            {
                case 0: rightClickLoop = 1;
                        isFlag = true;
                        flagSprite.gameObject.SetActive(true);
                        gameManager.OnFlagSet(true);
                        break;

                case 1: rightClickLoop = 2;
                        isFlag = false;
                        isQuestion = true;
                        flagSprite.gameObject.SetActive(false);
                        gameManager.OnFlagSet(false);
                        questionSprite.gameObject.SetActive(true);
                        break;

                case 2: rightClickLoop = 0;
                        isQuestion = false;
                        questionSprite.gameObject.SetActive(false);
                        break;
            }
        } 
        else
        {
            gameManager.AFlag(vPosition, hPosition, mineCount);
        }
    }

    public void AroundFlag()
    {
        if(!isOpen && !isFlag)
        {
            rightClickLoop = 1;
            isFlag = true;
            isQuestion = false;
            questionSprite.gameObject.SetActive(false);
            flagSprite.gameObject.SetActive(true);
            gameManager.OnFlagSet(true);
        }
    }

    public void setDisplay()
    {
        if (isMine)
        {
            mineSprite.gameObject.SetActive(true);
        }
        else
        {
            switch (mineCount)
            {
                case 1:
                    numberText.text = "1";
                    numberText.color = new Color32(0, 0, 255, 255);
                    break;
                case 2:
                    numberText.text = "2";
                    numberText.color = new Color32(0, 128, 0, 255);
                    break;
                case 3:
                    numberText.text = "3";
                    numberText.color = new Color32(255, 0, 0, 255);
                    break;
                case 4:
                    numberText.text = "4";
                    numberText.color = new Color32(0, 0, 128, 255);
                    break;
                case 5:
                    numberText.text = "5";
                    numberText.color = new Color32(128, 0, 0, 255);
                    break;
                case 6:
                    numberText.text = "6";
                    numberText.color = new Color32(0, 128, 128, 255);
                    break;
                case 7:
                    numberText.text = "7";
                    numberText.color = new Color32(0, 0, 0, 255);
                    break;
                case 8:
                    numberText.text = "8";
                    numberText.color = new Color32(128, 128, 128, 255);
                    break;
            }
        }
    }

    public void OnGameOver()
    {
        if (isMine)
        {
            if (isFlag)
            {
                flagSprite.gameObject.SetActive(false);
            }
            coverSprite.gameObject.SetActive(false);
            questionSprite.gameObject.SetActive(false);
        }
    }
}
