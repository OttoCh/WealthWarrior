using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {

    private Touch touch;
    private bool activated = false;
    public GameObject stage;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject Player;
    public GameObject gameManager;
    public GameObject Stage_GUI;
    public GameObject ShieldCard;
    private int currentStage = 0;
    public float prizeMoney;

    //buat mapnya
    private static int max_x_map = 7;
    private static int max_y_map = 13;
    private float[] coordinateMap_x = new float[max_x_map];
    private float[] coordinateMap_y = new float[max_y_map];

    //set range x dan y serta toleransinya
    public static float xRange = 5.45f;
    public static float yRange = 2.7f;
    private float xRangeTolerance = xRange / 2.0f;
    private float yRangeTolerance = yRange / 2.0f;
    public bool allowMove;
    Player_Move pm = new Player_Move();

	// Use this for initialization
	void Start () {
        createCoordinateMap();
        pm = Player.GetComponent<Player_Move>();
        //playerBody = testCube.GetComponent<Rigidbody2D>();
        //playerBody = Player.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        //touch = Input.GetTouch(0);
        //float x = touch.position.x;
        //float y = touch.position.y;

        if(Input.GetButtonDown("Fire1"))
        {
            if (allowMove)
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 localStagePoint = stage.transform.InverseTransformPoint(worldPoint);
                //Debug.Log("local" + localStagePoint);
                if (localStagePoint.x > -1 * xRangeTolerance && localStagePoint.y > -1 * yRangeTolerance)
                {
                    Vector2 localTruePos = checkCoordinateOnMap(localStagePoint);
                    Vector2 worldTruePos = stage.transform.TransformPoint(localTruePos);
                    playerWalk(worldTruePos);
                }
            }
        }
	}

    void createCoordinateMap()
    {
        float curRange = 0;
        for (int i = 0; i < max_x_map; i++)
        {
            coordinateMap_x[i] = curRange;
            curRange += xRange;
        }
        curRange = 0;
        for (int j = 0; j < max_y_map; j++)
        {
            coordinateMap_y[j] = curRange;
            curRange += yRange;
        }
    }

    Vector2 checkCoordinateOnMap(Vector3 localPoint)
    {
        int x_index = 0;
        int y_index = 0;
        for (int i = 0; i < max_x_map; i++)
        {
            if(localPoint.x < coordinateMap_x[i])
            {
                x_index = i;
                break;
            }
            if(i == max_x_map-1)
            {
                x_index = max_x_map + 1;
            }
        }
        for(int j=0; j<max_y_map; j++)
        {
            if(localPoint.y < coordinateMap_y[j])
            {
                y_index = j;
                break;
            }
            if (j == max_y_map - 1)
            {
                y_index = max_y_map + 1;
            }
        }

        //perform check to make sure which coordinate is closer
        float trueXPos = new float();
        float trueYPos = new float();
        bool checkCoor_X = true;
        bool checkCoor_Y = true;

        if(x_index == max_x_map+1)
        {
            checkCoor_X = false;
            trueXPos = coordinateMap_x[x_index - 2];
        }
        if(y_index == max_y_map+1)
        {
            checkCoor_Y = false;
            trueYPos = coordinateMap_y[y_index - 2];
        }

        if (checkCoor_X)
        {
            if (x_index != 0)
            {
                if ((coordinateMap_x[x_index] - localPoint.x) < (localPoint.x - coordinateMap_x[x_index - 1]))
                {
                    trueXPos = coordinateMap_x[x_index];
                }
                else
                {
                    trueXPos = coordinateMap_x[x_index - 1];
                }
            }
            else
            {
                trueXPos = coordinateMap_x[x_index];
            }
            
        }
        if (checkCoor_Y)
        {
            if (y_index != 0)
            {
                if ((coordinateMap_y[y_index] - localPoint.y) < (localPoint.y - coordinateMap_y[y_index - 1]))
                {
                    trueYPos = coordinateMap_y[y_index];
                }
                else
                {
                    trueYPos = coordinateMap_y[y_index - 1];
                }
            }
            else
            {
                trueYPos = coordinateMap_y[y_index];
            }
        }

        Vector2 trueStageCoordinate = new Vector2(trueXPos, trueYPos);
        return trueStageCoordinate;
    }

    void playerWalk(Vector2 destCoor)
    {
        //buat grafiknya dahulu
        Vector2 initCoor = Player.transform.position;
        //Rigidbody2D playerBody = Player.GetComponent<Rigidbody2D>();
        float moveForce = 100.0f;

        Vector2 deltaMovement = new Vector2((destCoor.x - initCoor.x), (destCoor.y - initCoor.y));
        float radianOfMovement = Mathf.Atan(deltaMovement.y / deltaMovement.x);

        float x_movementSpeed = moveForce * Time.deltaTime * Mathf.Cos(radianOfMovement);
        float y_movementSpeed = moveForce * Time.deltaTime * Mathf.Sin(radianOfMovement);
        if (deltaMovement.x < 0)
        {
            x_movementSpeed *= -1;
        }
        if(deltaMovement.y <0 )
        {
            if (y_movementSpeed > 0) y_movementSpeed *= -1;
        }
        else if(deltaMovement.y > 0)
        {
            if (y_movementSpeed < 0) y_movementSpeed *= -1;
        }
        float deltaStraightMove = Mathf.Sqrt(deltaMovement.x*deltaMovement.x + deltaMovement.y*deltaMovement.y);
        float walkTime = deltaStraightMove / (moveForce * Time.deltaTime);
        Vector2 movementSpeed = new Vector2(x_movementSpeed, y_movementSpeed);

        //debug everything
        /*
        Debug.Log("walk time: " + walkTime);
        Debug.Log("radian of movement" + radianOfMovement);
        Debug.Log("delta movement" + deltaMovement);
        Debug.Log("movement speed" + movementSpeed);
        Debug.Log("delta straight move" + deltaStraightMove);
        */

        //playerBody.velocity = movementSpeed;
        pm.movePlayer(movementSpeed);
        StartCoroutine(stopWalk(walkTime));

        /*
        for(float x=0.0f; x<destCoor.x; x+=0.1f)
        {
            float y = (x - initCoor.x) / (destCoor.x - initCoor.x) * (initCoor.y - destCoor.y) - initCoor.y;
            //playerBody.velocity = 
        }
        */
    }

    private IEnumerator stopWalk(float walkTime)
    {
        yield return new WaitForSeconds(walkTime);
        stopWalking();
    }

    void stopWalking()
    {
        Vector2 zero = new Vector2(0, 0);
        pm.movePlayer(zero);
    }

    public void beginDestroy()
    {
        Basic_GameManager gm = gameManager.GetComponent<Basic_GameManager>();
        gm.changePlayerDrawOrder(-3);
        //Destroy(stage);
        StartCoroutine(FadeoutStage());
        gm.setCamera(false, transform.position);
        ShieldCard.SetActive(true);
        Stage_GUI.SetActive(false);
        gm.setActiveLamp(true);
        gm.turnOffWalk(true);
        gm.setActiveJoystickControl(true);
        gm.givePlayerMoney(prizeMoney);
        GameObject[] allTower = GameObject.FindGameObjectsWithTag("Tower");
        GameObject[] allBullet = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] allEnemyBullet = GameObject.FindGameObjectsWithTag("BulletEnemy");
        foreach (GameObject tower in allTower)
        {
            Destroy(tower);
        }
        foreach(GameObject bullet in allBullet)
        {
            Destroy(bullet);
        }
        foreach (GameObject bullet in allEnemyBullet)
        {
            Destroy(bullet);
        }
        gm.stageFinished(currentStage);
        gm.changeAudio(false);
        currentStage = 0;
        activated = false;
        allowMove = false;
    }

    IEnumerator FadeoutStage()
    {
        float a = 1.0f;
        SpriteRenderer sp = stage.transform.Find("StageBackground").GetComponent<SpriteRenderer>();
        while(a > 0)
        {
            a -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            sp.color = new Color(1f, 1f, 1f, a);
        }
        Destroy(stage);

    }

    public bool beginStage(int numberOfStage)
    {
        if (!activated)
        {
            activated = true;
            currentStage = numberOfStage;
            Stage_GUI.SetActive(true);
            Basic_GameManager gm = gameManager.GetComponent<Basic_GameManager>();
            if (numberOfStage == 1)
            {
                stage = stage1;
                ShieldCard.SetActive(false);
            }
            else if(numberOfStage == 2)
            {
                stage = stage2;
                gm.numberOfStage = numberOfStage;
            }
            else if (numberOfStage == 3)
            {
                stage = stage2;
                //stage.transform.FindChild("StageBackground").gameObject.SetActive(false);
            }
            stage.SetActive(true);

            gm.setActiveJoystickControl(false);
            gm.setActiveLamp(false);
            gm.setActiveTong(false);
            gm.changePlayerDrawOrder(3);
            gm.turnOffWalk(false);
            gm.changeAudio(true);

            GameObject stageRef = stage.transform.Find("camStageReference").gameObject;
            Vector3 cameraStageReference = stage.transform.TransformPoint(stageRef.transform.localPosition);
            gm.setCamera(true, cameraStageReference);
            Vector2 initialPos = new Vector2(0, 0);
            Vector2 localTruePos = checkCoordinateOnMap(initialPos);
            Vector2 worldTruePos = stage.transform.TransformPoint(localTruePos);
            playerWalk(worldTruePos);
            allowMove = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    void activateShield()
    {
        //Instantiate
    }
    
}
