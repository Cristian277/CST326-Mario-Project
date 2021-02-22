using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParserStarter : MonoBehaviour
{
    public string filename;

    public GameObject Rock;

    public GameObject Brick;

    public GameObject QuestionBox;

    public GameObject Stone;

    public Transform parentTransform;

    private int coinCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        RefreshParse();
    }

    void Update()
    {
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
                }
            }
        }
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
                    SpawnPrefab(letter, new Vector3(column,-row,0));
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
            default:
                return;
        }

        ToSpawn = GameObject.Instantiate(ToSpawn, parentTransform);
        ToSpawn.transform.localPosition = positionToSpawn;
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
