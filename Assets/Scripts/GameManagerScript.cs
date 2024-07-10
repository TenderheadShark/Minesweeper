using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    static int fieldWidth;
    static int fieldHeight;
    static int mineQuantity;
    CellManagerScript[,] cellManagers = new CellManagerScript[fieldHeight, fieldWidth];
    int[,] field = new int[fieldHeight, fieldWidth];
    public GameObject cellPrefab;
    public GameObject mainCamObj;
    public GameObject clearText;
    TextMeshProUGUI mineCounterText;
    Camera _camera;
    int flagCount = 0;
    int openCount = 0;
    public static bool isGameOver;
    public static bool isGameClear;
    int cellCountWithoutMine = (fieldHeight * fieldWidth) - mineQuantity;
    TimerScript timerScript;
    Vector3 preMousePos;

    public static void SetDifficulty(int width, int height, int quantity)
    {
        fieldWidth = width;
        fieldHeight = height;
        mineQuantity = quantity;
    }

    void Start()
    {
        isGameOver = false;
        isGameClear = false;
        for (int i = 0; i < fieldHeight; i++)
        {
            for (int j = 0; j < fieldWidth; j++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3((float)j * 1.05f, (float)i * -1.05f, 0), Quaternion.identity) as GameObject;
                cell.name = i + "," + j;
                cellManagers[i, j] = GameObject.Find(i + "," + j).GetComponent<CellManagerScript>();
            }
        }
        GameObject upperLeft = GameObject.Find(0 + "," + 0);
        GameObject bottomRight = GameObject.Find((fieldHeight - 1) + "," + (fieldWidth - 1));
        _camera = mainCamObj.GetComponent<Camera>();
        mainCamObj.transform.position = new Vector3((upperLeft.transform.position.x + bottomRight.transform.position.x) / 2.0f, (upperLeft.transform.position.y + bottomRight.transform.position.y) / 2.0f, -10f);
        _camera.orthographicSize = Mathf.Abs((upperLeft.transform.position.y + bottomRight.transform.position.y) / 2.0f) + 3.5f;
        mineCounterText = GameObject.Find("MineCounter").GetComponent<TextMeshProUGUI>();
        mineCounterText.text = (mineQuantity - flagCount).ToString();
        timerScript = GameObject.Find("Timer").GetComponent<TimerScript>();
    }

    public void SetMine(int v, int h)
    {
        timerScript.timerStop = false;
        int c = 0;
        bool u = false;
        bool d = false;
        bool l = false;
        bool r = false;
        if (v == 0) u = true;
        if (v == (fieldHeight - 1)) d = true;
        if (h == 0) l = true;
        if (h == (fieldWidth - 1)) r = true;
        while (c < mineQuantity)
        {
            int _v = Random.Range(0, fieldHeight);
            int _h = Random.Range(0, fieldWidth);
            bool _u = false;
            bool _d = false;
            bool _l = false;
            bool _r = false;
            if (_v == 0) _u = true;
            if (_v == (fieldHeight - 1)) _d = true;
            if (_h == 0) _l = true;
            if (_h == (fieldWidth - 1)) _r = true;
            if (((!u && _v == (v - 1)) || (!d && _v == (v + 1)) || _v == v) && ((!l && _h == (h - 1)) || (!r && _h == (h + 1)) || _h == h)) continue;
            if (cellManagers[_v, _h].isMine) continue;
            cellManagers[_v, _h].isMine = true;
            if (!_l) cellManagers[_v, _h - 1].mineCount++;
            if (!_r) cellManagers[_v, _h + 1].mineCount++;
            if (!_u) cellManagers[_v - 1, _h].mineCount++;
            if (!_d) cellManagers[_v + 1, _h].mineCount++;
            if (!_u && !_l) cellManagers[_v - 1, _h - 1].mineCount++;
            if (!_u && !_r) cellManagers[_v - 1, _h + 1].mineCount++;
            if (!_d && !_l) cellManagers[_v + 1, _h - 1].mineCount++;
            if (!_d && !_r) cellManagers[_v + 1, _h + 1].mineCount++;
            c++;
        }
        for (int i = 0; i < fieldHeight; i++)
        {
            for (int j = 0; j < fieldWidth; j++)
            {
                cellManagers[i, j].setDisplay();
            }
        }
    }

    public void OpenCell(int v, int h, int mineCount, bool isMine)
    {
        bool u = false;
        bool d = false;
        bool l = false;
        bool r = false;
        if (v == 0) u = true;
        if (v == (fieldHeight - 1)) d = true;
        if (h == 0) l = true;
        if (h == (fieldWidth - 1)) r = true;
        if (isMine)
        {
            cellManagers[v, h].boomSprite.gameObject.SetActive(true);
            isGameOver = true;
        }
        else
        {
            if (mineCount == 0)
            {
                if (!l) cellManagers[v, h - 1].Around();
                if (!r) cellManagers[v, h + 1].Around();
                if (!u) cellManagers[v - 1, h].Around();
                if (!d) cellManagers[v + 1, h].Around();
                if (!u && !l) cellManagers[v - 1, h - 1].Around();
                if (!u && !r) cellManagers[v - 1, h + 1].Around();
                if (!d && !l) cellManagers[v + 1, h - 1].Around();
                if (!d && !r) cellManagers[v + 1, h + 1].Around();
            }
            openCount++;
        }
        CheckGameClear();
    }

    public void OnFlagSet(bool isFlag)
    {
        flagCount = isFlag ? flagCount + 1 : flagCount - 1;
        CheckGameClear();
        mineCounterText.text = (mineQuantity - flagCount).ToString();

    }

    public void AOpen(int v, int h, int mineCount)
    {
        int cnt = 0;
        bool u = false;
        bool d = false;
        bool l = false;
        bool r = false;
        if (v == 0) u = true;
        if (v == (fieldHeight - 1)) d = true;
        if (h == 0) l = true;
        if (h == (fieldWidth - 1)) r = true;
        if (!l && cellManagers[v, h - 1].isFlag) cnt++;
        if (!r && cellManagers[v, h + 1].isFlag) cnt++;
        if (!u && cellManagers[v - 1, h].isFlag) cnt++;
        if (!d && cellManagers[v + 1, h].isFlag) cnt++;
        if ((!u && !l) && cellManagers[v - 1, h - 1].isFlag) cnt++;
        if ((!u && !r) && cellManagers[v - 1, h + 1].isFlag) cnt++;
        if ((!d && !l) && cellManagers[v + 1, h - 1].isFlag) cnt++;
        if ((!d && !r) && cellManagers[v + 1, h + 1].isFlag) cnt++;
        if (mineCount == cnt)
        {
            if (!l) cellManagers[v, h - 1].Around();
            if (!r) cellManagers[v, h + 1].Around();
            if (!u) cellManagers[v - 1, h].Around();
            if (!d) cellManagers[v + 1, h].Around();
            if (!u && !l) cellManagers[v - 1, h - 1].Around();
            if (!u && !r) cellManagers[v - 1, h + 1].Around();
            if (!d && !l) cellManagers[v + 1, h - 1].Around();
            if (!d && !r) cellManagers[v + 1, h + 1].Around();
        }
    }
    public void AFlag(int v, int h, int mineCount)
    {
        int cnt = 0;
        bool u = false;
        bool d = false;
        bool l = false;
        bool r = false;
        if (v == 0) u = true;
        if (v == (fieldHeight - 1)) d = true;
        if (h == 0) l = true;
        if (h == (fieldWidth - 1)) r = true;
        if (!l && !cellManagers[v, h - 1].isOpen) cnt++;
        if (!r && !cellManagers[v, h + 1].isOpen) cnt++;
        if (!u && !cellManagers[v - 1, h].isOpen) cnt++;
        if (!d && !cellManagers[v + 1, h].isOpen) cnt++;
        if ((!u && !l) && !cellManagers[v - 1, h - 1].isOpen) cnt++;
        if ((!u && !r) && !cellManagers[v - 1, h + 1].isOpen) cnt++;
        if ((!d && !l) && !cellManagers[v + 1, h - 1].isOpen) cnt++;
        if ((!d && !r) && !cellManagers[v + 1, h + 1].isOpen) cnt++;
        if (mineCount == cnt)
        {
            if (!l) cellManagers[v, h - 1].AroundFlag();
            if (!r) cellManagers[v, h + 1].AroundFlag();
            if (!u) cellManagers[v - 1, h].AroundFlag();
            if (!d) cellManagers[v + 1, h].AroundFlag();
            if (!u && !l) cellManagers[v - 1, h - 1].AroundFlag();
            if (!u && !r) cellManagers[v - 1, h + 1].AroundFlag();
            if (!d && !l) cellManagers[v + 1, h - 1].AroundFlag();
            if (!d && !r) cellManagers[v + 1, h + 1].AroundFlag();
        }
    }

    void CheckGameClear()
    {
        if (isGameOver)
        {
            timerScript.timerStop = true;
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    cellManagers[i, j].OnGameOver();
                }
            }
        }
        else if (openCount == cellCountWithoutMine && flagCount == mineQuantity)
        {
            isGameClear = true;
            timerScript.timerStop = true;
            clearText.gameObject.SetActive(true);
        }
    }

    public void OnRestartButtonDown()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnBackButtonDown()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y * Time.deltaTime * 50;
        if (_camera.orthographicSize > 0.1)
        {
            _camera.orthographicSize += scroll;
        }
        else
        {
            _camera.orthographicSize = 0.11f;
        }
        if (Input.GetMouseButtonDown(2))
        {
            preMousePos = Input.mousePosition;
            Debug.Log(Input.mousePosition);
        }
        MouseDrag(Input.mousePosition);
    }
    void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - preMousePos;
        Debug.Log(diff);

        if (diff.magnitude < Vector3.kEpsilon) return;

        if (Input.GetMouseButton(2))
        {
            mainCamObj.transform.Translate(-diff * Time.deltaTime * 5.0f);
        }
        preMousePos = mousePos;
    }
}
