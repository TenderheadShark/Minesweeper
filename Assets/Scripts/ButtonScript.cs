using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    CellManagerScript cellManager;
    public static bool isInit;

    void Start()
    {
        isInit = true;
        cellManager = GetComponentInParent<CellManagerScript>();
    }
    
    void OnMouseOver()
    {
        if (!GameManagerScript.isGameClear&&!GameManagerScript.isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isInit)
                {
                    isInit = false;
                    cellManager.InitLeftClicked();
                }
                cellManager.LeftClicked();
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (!isInit)
                {
                    cellManager.RightClicked();
                }
            }
        }
    }
}
