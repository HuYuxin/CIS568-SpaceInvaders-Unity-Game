using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FortressController : MonoBehaviour {
    public GameObject fortress;
    public List<GameObject> fortressAArray;
    public List<GameObject> fortressBArray;
    public List<GameObject> fortressCArray;
    public List<GameObject> fortressDArray;
    public List<List<GameObject>> fortressTotal;
    public Vector3 originInScreenCoords;
    public float width;
    public float height;
    public float fortressPieceCount;
    private float horizontalPos;
    private float verticalPos;
    //private float depthPos;
    // Use this for initialization
    void Start () {
        fortressPieceCount = 20; //total piece per fortress will be 10*10
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        width = Screen.width;
        height = Screen.height;
        //initialize fortresses
        initializeFortress(1, ref fortressAArray);
        initializeFortress(2, ref fortressBArray);
        initializeFortress(3, ref fortressCArray);
        initializeFortress(4, ref fortressDArray);
        fortressTotal = new List<List<GameObject>>();
        fortressTotal.Add(fortressAArray);
        fortressTotal.Add(fortressBArray);
        fortressTotal.Add(fortressCArray);
        fortressTotal.Add(fortressDArray);
    }
	
    void initializeFortress(int fortressOrder, ref List<GameObject> fortressArray)
    {
        Vector3 WorldOriginInScreen = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        Vector3 WorldUnitInScreen = Camera.main.WorldToScreenPoint(new Vector3(0.1f, 0, 0));
        float posOffset = Vector3.Distance(WorldOriginInScreen, WorldUnitInScreen);
        //Debug.Log("WorldUnitInScreen: " + WorldUnitInScreen);
        for (int i=0; i<fortressPieceCount/2; i++)
        {
            for (int j = 0; j < fortressPieceCount; j++)
            {
                horizontalPos = fortressOrder * width / 6+ j* posOffset;
                verticalPos = height / 8 + i * posOffset; 
                GameObject fortressPiece = Instantiate(fortress, Camera.main.ScreenToWorldPoint(
                                                    new Vector3(horizontalPos,
                                                    verticalPos, originInScreenCoords.z)),
                                                    Quaternion.identity);                
                fortressArray.Add(fortressPiece);
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void deleteFortressPiece(GameObject f)
    {
        for(int i=0; i< fortressTotal.Count; i++)
        {
            if (!fortressTotal[i].Remove(f))
            {
                continue;
            }else
            {
                if (fortressTotal[i].Count < 1)
                {
                    //GameObject obj = GameObject.Find("GlobalController");
                    //GlobalController g = obj.GetComponent<GlobalController>();                  
                    //PlayerPrefs.SetInt("Player Score", g.score);
                    SceneManager.LoadScene("GameOver");
                }
                break;
            }
        }
    }
}
