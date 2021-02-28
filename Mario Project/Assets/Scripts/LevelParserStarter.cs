using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelParserStarter : MonoBehaviour
{
    public string filename;

    public GameObject Rock;

    public GameObject Brick;

    public GameObject QuestionBox;

    public GameObject Stone;

    public GameObject Water;

    public GameObject Goal;

    public Transform parentTransform;

    [SerializeField] private Text CoinCounterText;
    [SerializeField] private Text TimerText;
    [SerializeField] private Text PointsText;

    public float timeRemaining = 100;
    private int pointsCount = 0;
    private int coinCount = 0;

    void Start()
    {
        RefreshParse();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            TimerText.text = (timeRemaining).ToString("0");
        }
        else
        {
            Debug.Log("Player Failed.");
            Time.timeScale = 0f;
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Brick(Clone)")
                {
                    Destroy(hit.collider.gameObject);
                }else if(hit.collider.gameObject.name == "QuestionBox(Clone)")
                {
                    coinCount++;
                    Debug.Log(coinCount);
                    CoinCounterText.text = coinCount.ToString();
                }
            }
        }
        */
    }
    private void FileParser()
    {
        string fileToParse = string.Format("{0}{1}{2}.txt", Application.dataPath, "/Resources/", filename);

        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            int row = 0;

            while ((line = sr.ReadLine()) != null)
            {
                int column = 0;
                char[] letters = line.ToCharArray();
                foreach (var letter in letters)
                {
                    //Call SpawnPrefab
                    SpawnPrefab(letter, new Vector3(column, -row, (float)-0.5));
                    column++;
                }
                row++;
            }
           
            sr.Close();
        }
    }

    private void SpawnPrefab(char spot, Vector3 positionToSpawn)
    {
        GameObject ToSpawn;

        switch (spot)
        {
            case 'b':
                ToSpawn = Brick;
                break;
            case '?':
                ToSpawn = QuestionBox;
                break;
            case 'x':
                ToSpawn = Rock;
                break;
            case 's':
                ToSpawn = Stone;
                break;
            case 'w':
                ToSpawn = Water;
                break;
            case 'g':
                ToSpawn = Goal;
                break;
            default:
                return;
        }

        ToSpawn = GameObject.Instantiate(ToSpawn, parentTransform);
        ToSpawn.transform.localPosition = positionToSpawn;
    }

    public void increaseCoinCount()
    {
        coinCount++;
        pointsCount += 100;
        CoinCounterText.text = coinCount.ToString();
        PointsText.text = pointsCount.ToString();
    }

    public void increasePointCount()
    {
        pointsCount += 100;
        PointsText.text = pointsCount.ToString();
    }

    public void RefreshParse()
    {
        GameObject newParent = new GameObject();
        newParent.name = "Environment";
        newParent.transform.position = parentTransform.position;
        newParent.transform.parent = this.transform;

        if (parentTransform) Destroy(parentTransform.gameObject);

        parentTransform = newParent.transform;
        FileParser();
    }
}
