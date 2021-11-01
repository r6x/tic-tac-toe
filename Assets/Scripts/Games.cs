using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Games : MonoBehaviour
{
    [Header("Текстуры")]
    public Sprite nothingTexture;
    public Sprite circleTexture;
    public Sprite crossTexture;
    [Header("Клетки")]
    public Button[] fields;
    [Header("Куда выводить очки")]
    public Text pointsText;
    public int rows = 3;
    public int columns = 3;
    int points;
    private List<Button> freeFields = new List<Button>();

    GenerateId fieldToPlace;
    private List<GenerateId> generateIds = new List<GenerateId>(); 

    private bool playerTurn = true;
    private string[] final = {"0", "1", "2", "3", "4", "5", "6", "7"};
    private int turnCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<fields.Length; i++)
        {

            fields[i].transform.GetComponent<Image>().sprite = nothingTexture;
            freeFields.Add(fields[i]);
            generateIds.Add(fields[i].GetComponent<GenerateId>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTurn == false && turnCounter < 9)
        {
            Debug.Log(freeFields.Count);
            Debug.Log("turnCounter: " + turnCounter);
            fieldToPlace = freeFields[Random.Range(0, freeFields.Count)].GetComponent<GenerateId>();
            Paint(fields[Random.Range(0, fields.Length)]);
        }
        checkWinner();
    }

    public void Paint(Button buttonClicked)
    {
        GenerateId temp;
        temp = buttonClicked.GetComponent<GenerateId>();
        if (temp.empty == true)
        {
            if (playerTurn == true)
            {
                buttonClicked.transform.GetComponent<Image>().sprite = circleTexture;
                playerTurn = false;
                temp.empty = false;
                temp.symbol = 'o';
            }
            else
            {
                buttonClicked.transform.GetComponent<Image>().sprite = crossTexture;
                playerTurn = true;
                temp.empty = false;
                temp.symbol = 'x';
            }
            turnCounter++;
            Debug.Log("FreeFields\r\n Count: " + freeFields.Count);
            Debug.Log("");
            for (int i = 0; i < freeFields.Count; i++)
            {
                Debug.Log(freeFields[i]);
            }
            for (int i = 0; i < freeFields.Count; i++)
            {
                if (freeFields[i] == buttonClicked)
                {
                    freeFields.RemoveAt(i);
                    break;
                }
            }
        }
    }

    private void clearTable()
    {
        turnCounter = 0;
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].transform.GetComponent<Image>().sprite = nothingTexture;
            fields[i].transform.GetComponent<GenerateId>().empty = true;
            fields[i].transform.GetComponent<GenerateId>().symbol = 'n';
            freeFields.Add(fields[i]);
        }
    }

    private bool checkRows(string x_or_0)
    {
        string result = "";
        for (int i = 0; i < rows*columns; i += columns)
        {
            for (int j = 0; j < columns; j++)
            {
                result += generateIds[i + j].symbol;
            }
            if (result == x_or_0)
            {
                return true;
            }
            result = "";
        }
        return false;
    }

    private bool checkColumns(string x_or_0)
    {
        string result = "";
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows * columns; j+=columns)
            {
                result += generateIds[i + j].symbol;
                if(result == x_or_0)
                {
                    return true;
                }
            }
            result = "";
            //Debug.Log(result);
        }
        return false;
    }

    private bool checkDiagonal(string x_or_0)
    {
        string result = "";
        for (int i = 0; i < columns*rows; i+=columns+1)
        {
            result += generateIds[i].symbol;
        }
        if(result == x_or_0)
        {
            return true;
        }
        result = "";
        for (int i = columns - 1; i < columns * rows; i += columns -1)
        {
            result += generateIds[i].symbol;
        }
       
        if (result == x_or_0)
        {
            Debug.Log(result);
            return true;
        }
        return false;
    }

    private bool chkwin(string x_or_0)
    {

        if (checkColumns(x_or_0))
        {
            Debug.Log("checkcolumns");
            return true;
        }
        if (checkRows(x_or_0))
        {
            Debug.Log("checkRows");
            return true;
        }
        if (checkDiagonal(x_or_0))
        {
            Debug.Log("checkcDiagonal");
            return true;
        }
        return false;
    }

    public void checkWinner()
    {
        bool xwin, owin, draw;
        xwin = chkwin("xxx");
        owin = chkwin("ooo");
        if(turnCounter == 9 || xwin || owin)
        {
            StartCoroutine(pause());
            //гдето здесь показываем результат
            clearTable();
            if (xwin)
            {
                Debug.Log("x win");
                points += 10;
                playerTurn = false;
            }else if (owin)
            {
                Debug.Log("o win");
                points -= 10;
                playerTurn = true;
            }
            else
            {
                Debug.Log("Draw");
                playerTurn = !playerTurn;
            }
            pointsText.text = points.ToString();
        }

    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(10.5f);
    }
}
