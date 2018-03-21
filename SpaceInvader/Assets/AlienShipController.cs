using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienShipController : MonoBehaviour {
    public float alienTimer;
    public float alienSpeed;
    public float alienShootSpeed;
    public float alienShootTimer;
    public List<GameObject> alienShipAArray1;
    public List<GameObject> alienShipAArray2;
    public List<GameObject> alienShipBArray1;
    public List<GameObject> alienShipBArray2;
    public List<GameObject> alienShipCArray;
    public List<List<GameObject>> alienShipTotal;
    public GameObject alienShipA;
    public GameObject alienShipB;
    public GameObject alienShipC;
    private GameObject alienShipAIndividual;
    public Vector3 originInScreenCoords;
    public float horizontalPos;
    public float verticalPos;
    public float width;
    public float height;
    private int moveRightCount;
    private int moveLeftCount;
    public bool moveRight;
    public bool moveLeft;
    public bool toggleDirection;
    public float movementStep;
    //private List<List<bool>> aliveAliens;
    private List<int> bottomAlienRowNum; // contains 11 elements, stores the row number of the bottom alien in this column
    private int aliensPerRow;
    private int aliensPerCol;
    private System.Random rnd;
    private int totalAliveAliens;
    private float horizontalOffSet;// Horizontal Displacement
    //private float verticalOffSet;
    private int totalMoveCount;

    // Use this for initialization
    void Start () {
        alienTimer = 0;
        alienSpeed = GameObject.Find("AlienStartSpeed").GetComponent<AlienStartSpeed>().alienStartSpeed;
        alienShootSpeed = 3.0f;
        aliensPerRow = 11;
        aliensPerCol = 5;
        totalAliveAliens = aliensPerRow * aliensPerCol;
        rnd = new System.Random();
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        width = Screen.width;
        height = Screen.height;
        Vector3 WorldOriginInScreen = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        Vector3 WorldUnitInScreen = Camera.main.WorldToScreenPoint(new Vector3(1.0f, 0, 0));
        //Vector3 World2UnitInScreen = Camera.main.WorldToScreenPoint(new Vector3(1.5f, 0, 0));
        horizontalOffSet = Vector3.Distance(WorldOriginInScreen, WorldUnitInScreen);
        //verticalOffSet = Vector3.Distance(WorldOriginInScreen, World2UnitInScreen);
        initializeAlienShip(0, ref alienShipAArray1, 10, "A");
        initializeAlienShip(1, ref alienShipAArray2, 10, "A");
        initializeAlienShip(2, ref alienShipBArray1, 20, "B");
        initializeAlienShip(3, ref alienShipBArray2, 20, "B");
        initializeAlienShip(4, ref alienShipCArray, 30, "C");
        alienShipTotal = new List<List<GameObject>>();
        alienShipTotal.Add(alienShipAArray1);
        alienShipTotal.Add(alienShipAArray2);
        alienShipTotal.Add(alienShipBArray1);
        alienShipTotal.Add(alienShipBArray2);
        alienShipTotal.Add(alienShipCArray);
        //Initially bottom rows are all alive
        bottomAlienRowNum = new List<int>();
        for (int col = 0; col<aliensPerRow; col++)
        {
            bottomAlienRowNum.Add(0);
        }
        moveRight = true;
        moveLeft = false;
        toggleDirection = false;
        WorldOriginInScreen = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        WorldUnitInScreen = Camera.main.WorldToScreenPoint(new Vector3(0.5f, 0, 0));
        movementStep = Vector3.Distance(WorldOriginInScreen, WorldUnitInScreen);
        totalMoveCount = (int)((width - 1.8 * horizontalOffSet * aliensPerRow)/ movementStep);
        //Debug.Log("totalMoveCount: " + totalMoveCount);
        moveRightCount = totalMoveCount / 2;
        moveLeftCount = 0;       
    }

    void initializeAlienShip(int row, ref List<GameObject> alienShipArray, int pointVal, string alienShipType)
    {
        
        for (int i = 0; i < aliensPerRow; i++)
        {
            horizontalPos = width / 2 - (5 - i) * 1.5f*horizontalOffSet;
            verticalPos = height / 2 + horizontalOffSet * row;
            if(alienShipType == "A")
            {
                alienShipAIndividual = Instantiate(alienShipA, Camera.main.ScreenToWorldPoint(
                                            new Vector3(horizontalPos,
                                            verticalPos, originInScreenCoords.z)),
                                            Quaternion.identity);
                AlienShipA a = alienShipAIndividual.GetComponent<AlienShipA>();
                a.colNum = i;
                a.setPointValue(pointVal);
            }else if(alienShipType == "B")
            {
                alienShipAIndividual = Instantiate(alienShipB, Camera.main.ScreenToWorldPoint(
                                            new Vector3(horizontalPos,
                                            verticalPos, originInScreenCoords.z)),
                                            Quaternion.identity);
                AlienShipB b = alienShipAIndividual.GetComponent<AlienShipB>();
                b.colNum = i;
                b.setPointValue(pointVal);
            }else if(alienShipType == "C")
            {
                alienShipAIndividual = Instantiate(alienShipC, Camera.main.ScreenToWorldPoint(
                                            new Vector3(horizontalPos,
                                            verticalPos, originInScreenCoords.z)),
                                            Quaternion.identity);
                AlienShipC a = alienShipAIndividual.GetComponent<AlienShipC>();
                a.colNum = i;
                a.setPointValue(pointVal);
            }else
            {
                Debug.LogError("AlienShip Type you indicated did not exist!!");
            }
            
            alienShipArray.Add(alienShipAIndividual);
        }
    }

    public GameObject alientBullet;

    void fireHelper(int row, int col)
    {
        AlienShipA a = alienShipTotal[row][col].GetComponent<AlienShipA>();
        if (a != null)
        {
            a.fire();
        }

        AlienShipB b = alienShipTotal[row][col].GetComponent<AlienShipB>();
        if (b != null)
        {
            b.fire();
        }

        AlienShipC c = alienShipTotal[row][col].GetComponent<AlienShipC>();
        if (c != null)
        {
            c.fire();
        }
    }

    bool chooseAlienToFire(int randomCol)
    {
        bool columnPickedIsAllDie = false;
        int rowNum = bottomAlienRowNum[randomCol];
        int colNumRecount = 0;
        // if the rowNum is -1, meaning the column we randomly picked has all been killed. 
        //We start from 1st colume and find the first available alien and fire
        if (rowNum == -1)
        {
            columnPickedIsAllDie = true;
            while (rowNum == -1 && colNumRecount < 11)
            {
                rowNum = bottomAlienRowNum[colNumRecount];
                colNumRecount++;
            }
            if (colNumRecount < 11)
            {
                try
                {
                    colNumRecount--;
                    fireHelper(rowNum, colNumRecount);
                }
                catch
                {
                    Debug.Log("Catch Exception Error 1 " + "row "+rowNum+" col "+colNumRecount);
                }
            }
        }
        else
        {
            try
            {
                fireHelper(rowNum, randomCol);
            }
            catch
            {
                Debug.Log("Catch Exception Error 2 " + "row " + rowNum + " col " + randomCol);
            }
        }
        return columnPickedIsAllDie;
    }

    // Update is called once per frame
    void Update () {
        //Move Alienships
        alienTimer += Time.deltaTime;
        alienShootTimer += Time.deltaTime;
        if (alienTimer > alienSpeed)
        {
            Vector3 movement = new Vector3(0, 0,0);
            alienTimer = 0;
            if (moveRight)
            {
                if (moveRightCount > totalMoveCount)
                {
                    movement.z = -0.5f;
                    moveRightCount = 0;
                    moveLeftCount = 0;
                    toggleDirection = true;
                }else
                {
                    movement.x = 0.5f;
                    moveRightCount++;
                }
            }
            if (moveLeft)
            {
                if (moveLeftCount > totalMoveCount)
                {
                    movement.z = -0.5f;
                    moveRightCount = 0;
                    moveLeftCount = 0;
                    toggleDirection = true;
                }else
                {
                    movement.x = -0.5f;
                    moveLeftCount++;
                }
            }
            if (toggleDirection)
            {
                moveLeft = !moveLeft;
                moveRight = !moveRight;
                toggleDirection = false;
            }
            for (int row=0; row < alienShipTotal.Count; row++)
            {
                for(int col=0; col < alienShipTotal[row].Count; col++)
                {
                    if (alienShipTotal[row][col] != null)
                    {
                        AlienShipA a = alienShipTotal[row][col].GetComponent<AlienShipA>();
                        if (a != null)
                        {
                            a.MoveAlienShipA(movement);
                        }
                        

                        AlienShipB b = alienShipTotal[row][col].GetComponent<AlienShipB>();
                        if (b != null)
                        {
                            b.MoveAlienShipB(movement);
                        }
                        
                        AlienShipC c = alienShipTotal[row][col].GetComponent<AlienShipC>();
                        if (c != null)
                        {
                            c.MoveAlienShipC(movement);
                        }
                    }
                }                
            }            
        }

        //Control Alienships to fire
        if (alienShootTimer > alienShootSpeed)
        {
            alienShootTimer = 0;
            //Randomly picks 2 colume number and shoot bullets from those alienShips in bottomAlienRow.
            
            int colNum1 = rnd.Next(11);
            int colNum2 = rnd.Next(11);

            if (!chooseAlienToFire(colNum1))
            {
                chooseAlienToFire(colNum2);
            }            
        }
    }

    public void deleteAlienShip(GameObject alienShip)
    {
        int colNum = -1;
        for (int i = 0; i < alienShipTotal.Count; i++)
        {
            colNum = alienShipTotal[i].IndexOf(alienShip);
            if (colNum < 0)
            {
                continue;
            }else
            {
                //update bottomAlienRowNum
                int lastRowInCol = bottomAlienRowNum[colNum];
                while(lastRowInCol < 5 && (alienShipTotal[lastRowInCol][colNum] == null || alienShipTotal[lastRowInCol][colNum]==alienShip))
                {
                    lastRowInCol++;
                }
                if (lastRowInCol < 5)
                {
                    bottomAlienRowNum[colNum] = lastRowInCol;
                }
                if (lastRowInCol >= 5)
                {
                    bottomAlienRowNum[colNum] = -1;//All aliens in this column has been killed
                }

                /*Debug.Log("bottomAlienRowNum: " + bottomAlienRowNum[0] + " " + bottomAlienRowNum[1] + " " + bottomAlienRowNum[2] +
                    " "+bottomAlienRowNum[3] + " " + bottomAlienRowNum[4] + " " + bottomAlienRowNum[5] +
                    " "+bottomAlienRowNum[6] + " " + bottomAlienRowNum[7] + " " + bottomAlienRowNum[8] +
                    " "+bottomAlienRowNum[9] + " " + bottomAlienRowNum[10]);*/
                
                totalAliveAliens--;
                alienSpeed -= 0.01f;
                alienShootSpeed -= 0.03f;
                if (totalAliveAliens < 1)
                {
                    //SceneManager.LoadScene("GameWin");
                    //Application.LoadLevel(Application.loadedLevel);
                    float currentAlienStartSpeed = GameObject.Find("AlienStartSpeed").GetComponent<AlienStartSpeed>().alienStartSpeed;
                    if (currentAlienStartSpeed > 0.6)
                    {
                        GameObject.Find("AlienStartSpeed").GetComponent<AlienStartSpeed>().alienStartSpeed -= 0.05f;
                    }                   
                    Scene loadedLevel = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(loadedLevel.buildIndex);                    
                }
                break;
            }
        }
    }

    public void bossKilled()
    {
        int row = 0, col = 0;
        for(int i=0; i<11; i++)
        {
            row = bottomAlienRowNum[i];
            col = i;
            if (row>=0 && alienShipTotal[row][col] != null)
            {
                if (alienShipTotal[row][col].GetComponent<AlienShipA>() != null)
                {
                    alienShipTotal[row][col].GetComponent<AlienShipA>().Die();
                }
                else if (alienShipTotal[row][col].GetComponent<AlienShipB>() != null)
                {
                    alienShipTotal[row][col].GetComponent<AlienShipB>().Die();
                }
                else
                {
                    alienShipTotal[row][col].GetComponent<AlienShipC>().Die();
                }
            }
        }
    }
}
