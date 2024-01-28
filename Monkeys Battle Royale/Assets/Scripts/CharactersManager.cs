using RavingBots.Water2D;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharactersManager : MonoBehaviour {

    public GameObject water;
    public GameObject camera;

    public int MAX_POWER = 400;

    public float MAX_BANANA_SPEED = 16;

    public GameObject powerBarUI;
    public GameObject simpleMonkey;
    public GameObject banana;

    public GameObject halo;

    public GameObject batProjectile;
    public GameObject timerObject;
    private Transform currentCharacterTransform;
    private Transform[,] teamsMembersTransforms;

    private Character[,] characters;

    public int currentTeam;
    public int currentCharacter;

    private int charactersAmount;

    private int teamsAmount;

    private bool characterChanged = false;

    private Timer timerElement;

    public int currentBananasAmunition = 1;

    public bool reloadInTurn = false;

    public string status = "holding";

    private bool weaponsLocked = false;

    public int power = 1;

    public string currentWeapon = "banana";

    public int[] aliveCount;

	void Start() 
    {
        currentTeam = 0;
        currentCharacter = 0;
        charactersAmount = 2;
        teamsAmount = 2;

        Debug.Log("Characters Manager Started");
        teamsMembersTransforms = new Transform[teamsAmount,charactersAmount];
        characters = new Character[teamsAmount,charactersAmount];

        GameObject monkey1 = instantiateSimpleMonkey(-0.7f, 8f);
        GameObject monkey2 = instantiateSimpleMonkey(-4.3f, 9f);

        GameObject monkey3 = instantiateSimpleMonkey(5f, 6f);
        GameObject monkey4 = instantiateSimpleMonkey(12f, 6f);

        aliveCount = new int[teamsAmount];

        aliveCount[0] = charactersAmount;
        aliveCount[1] = charactersAmount;

        teamsMembersTransforms[0, 0] = monkey1.transform.Find("Character");
        teamsMembersTransforms[0, 1] = monkey2.transform.Find("Character");

        teamsMembersTransforms[1, 0] = monkey3.transform.Find("Character");
        teamsMembersTransforms[1, 1] = monkey4.transform.Find("Character");

        characters[0, 0] = teamsMembersTransforms[0, 0].GetComponent<Character>();
        characters[0, 1] = teamsMembersTransforms[0, 1].GetComponent<Character>();
        characters[1, 0] = teamsMembersTransforms[1, 0].GetComponent<Character>();
        characters[1, 1] = teamsMembersTransforms[1, 1].GetComponent<Character>();

        characters[0, 0].setId(0, 0, this);
        characters[0, 1].setId(0, 1, this);
        characters[1, 0].setId(1, 0, this);
        characters[1, 1].setId(1, 1, this);

        characters[currentTeam, currentCharacter].setCurrent(true);

        timerElement = timerObject.GetComponent<Timer>();
        timerElement.onTimerEnd.AddListener(OnTimerEnd);
    }

    public void characterDies(int teamNumber, int characterNumber)
    {
        Transform t = teamsMembersTransforms[teamNumber, characterNumber];
        instantiateHalo(t.position.x, t.position.y + 0.9f);

        aliveCount[teamNumber]--;
        if(aliveCount[teamNumber] < 1)
        {
            Debug.Log("Team " + teamNumber + " lost");
            SceneManager.LoadScene("GameOver");
            status = "game over";
            return;
        }
        if (currentCharacter == characterNumber && currentTeam == teamNumber)
        {
                characters[currentTeam, currentCharacter].setCurrent(false);
                currentCharacter = (currentCharacter + 1) % charactersAmount;
                characters[currentTeam, currentCharacter].setCurrent(true);
        }
    }

    void OnTimerEnd()
    {
        if(status == "game over")
        {
            return;
        }
        riseWaterLevel();
        Debug.Log("Timer ended, restaring...");
        timerElement.seconds = 25;
        timerElement.StartTimer();
        characters[currentTeam, currentCharacter].setCurrent(false);
        currentTeam = (currentTeam + 1) % teamsAmount;
        characters[currentTeam, currentCharacter].setCurrent(true);

        currentBananasAmunition = 1;
        status= "holding";
        currentWeapon = "banana";
        power = 1;
        weaponsLocked = false;
    }

    // mover a utils
    private IEnumerator SmoothLerp(GameObject target, float time, Vector3 cameraPosition)
    {
        Vector3 cameraTargetPos = new Vector3(cameraPosition.x, water.transform.position.y, cameraPosition.z);

        Vector3 startingPos = target.transform.position;
        Vector3 finalPos = target.transform.position + (transform.up * 0.5f);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            target.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            camera.transform.position = Vector3.Lerp(cameraPosition, cameraTargetPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void riseWaterLevel()
    {
        Vector3 cameraCurrentPos = camera.transform.position;
        //StartCoroutine(SmoothLerp(water, 3f, cameraCurrentPos));
    }
    
    public GameObject instantiateSimpleMonkey(float x, float y) {
        Vector3 position = new Vector3(x, y, 0);
        Quaternion rotation = Quaternion.identity;
        GameObject instantiatedPrefab = Instantiate(simpleMonkey, position, rotation);
        return instantiatedPrefab;
    }

    public GameObject instantiateBanana(float x, float y) {
        Vector3 position = new Vector3(x, y, 0);
        Quaternion rotation = Quaternion.identity;
        GameObject instantiatedPrefab = Instantiate(banana, position, rotation);
        return instantiatedPrefab;
    }

    public GameObject instantiateHalo(float x, float y) {
        Vector3 position = new Vector3(x, y, 0);
        Quaternion rotation = Quaternion.identity;
        GameObject instantiatedPrefab = Instantiate(halo, position, rotation);
        return instantiatedPrefab;
    }


    public GameObject instantiateBatProjectile(float x, float y) {
        Vector3 position = new Vector3(x, y, 0);
        Quaternion rotation = Quaternion.identity;
        GameObject instantiatedPrefab = Instantiate(batProjectile, position, rotation);
        return instantiatedPrefab;
    }

    public Transform getCurrentCharacterTransform() 
    {
        return teamsMembersTransforms[currentTeam, currentCharacter];
    }

    void Update()
    {
        if(status == "game over")
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(!weaponsLocked)
            {
                currentWeapon = "banana";
                Debug.Log("Current Weapon: Banana");
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(!weaponsLocked)
            {
                currentWeapon = "bat";
                Debug.Log("Current Weapon: Baseball bat");
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(!weaponsLocked)
            {
                currentWeapon = "frozen banana";
                Debug.Log("Current Weapon: Frozen Banana");
            }
        }
         if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(!characterChanged)
            {
                characterChanged = true;
                characters[currentTeam, currentCharacter].setCurrent(false);
                currentCharacter = (currentCharacter + 1) % charactersAmount;
                characters[currentTeam, currentCharacter].setCurrent(true);
            } else
            {
                characterChanged = false;
            }
        }

        if(isChargeable(currentWeapon))
        {
            if(Input.GetKey(KeyCode.Space))
            {
                weaponsLocked = true;
                //Debug.Log("shooting");
                if(status == "holding") 
                {
                    status = "charging";
                    powerBarUI.SetActive(true);           
                    power = 1;
                } else if (status == "charging")
                {
                    Animator powerBarUIAnim = powerBarUI.GetComponent<Animator>();
                    powerBarUIAnim.Play("PowerBarUI");
                    power++;
                    if (power > MAX_POWER)
                    {
                        power = MAX_POWER;
                        powerBarUIAnim.speed = 0f;
                    } else
                    {
                        powerBarUIAnim.speed = 1f;
                    }
                }
            } else
            {
                if (status == "charging")
                {
                    powerBarUI.SetActive(false);
                    switch (currentWeapon) 
                    {
                    case "banana":
                        bananaAction();
                        break;
                    case "frozen banana":
                        frozenBananaAction();
                        break;
                    case "bat":
                        batAction();
                        break;
                    default:
                        Debug.Log("UNKNOWN WEAPON: " + currentWeapon);
                        break;
                    }
                }
            }
        }
    }

    public void changeCharacter()
    {
        characterChanged = true;
        characters[currentTeam, currentCharacter].setCurrent(false);
        currentCharacter = (currentCharacter + 1) % charactersAmount;
        characters[currentTeam, currentCharacter].setCurrent(true);
    }

    private void bananaAction()
    {
        currentBananasAmunition--;
        float centerX = getCurrentCharacterTransform().position.x;
        float centerY = getCurrentCharacterTransform().position.y;
        Vector3 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float distanceX = worldMousePosition.x - centerX;
        float distanceY = worldMousePosition.y - centerY;

        Debug.Log("player x: " + centerX + "; mouse x: " + worldMousePosition.x);
        Debug.Log("player y: " + centerY + "; mouse y: " + worldMousePosition.y);

        if(distanceX == 0 && distanceY == 0)
        {
            distanceX = 0.2f;
        }

        Vector2 normalizedDistanceVector = new Vector2(distanceX, distanceY);

        Debug.Log("distance: " + normalizedDistanceVector);

        normalizedDistanceVector.Normalize();

        Debug.Log("normalized distance: " + normalizedDistanceVector);

        float bananaDistance = distanceX;
        if(distanceX < 0)
        {
            bananaDistance = -0.5f;
        } else
        {
            bananaDistance = 0.5f;
        }

        float bananaX = getCurrentCharacterTransform().position.x + bananaDistance;
        float bananaY = getCurrentCharacterTransform().position.y + 0.5f;

        float bananaSpeed = MAX_BANANA_SPEED * power / MAX_POWER;

        Debug.Log("Power: " + power + ". Dropping banana with " + bananaSpeed + " speed at " + bananaX + ", " + bananaY);
        GameObject bananaObject = instantiateBanana(bananaX, bananaY);
        Banana banana = bananaObject.GetComponent<Banana>();
        banana.initialSpeed = bananaSpeed;
        banana.normalizedDirection = normalizedDistanceVector;
        if(reloadInTurn)
        {
            status = "holding";
        } else
        {
            status = "fired";
        }
    }

    private void batAction()
    {
        float centerX = getCurrentCharacterTransform().position.x;
        float centerY = getCurrentCharacterTransform().position.y;
        Vector3 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float distanceX = worldMousePosition.x - centerX;
        float distanceY = worldMousePosition.y - centerY;

        Debug.Log("player x: " + centerX + "; mouse x: " + worldMousePosition.x);
        Debug.Log("player y: " + centerY + "; mouse y: " + worldMousePosition.y);

        if(distanceX == 0 && distanceY == 0)
        {
            distanceX = 0.2f;
        }

        Vector2 normalizedDistanceVector = new Vector2(distanceX, distanceY);

        Debug.Log("distance: " + normalizedDistanceVector);

        normalizedDistanceVector.Normalize();

        Debug.Log("normalized distance: " + normalizedDistanceVector);

        float bananaDistance = distanceX;
        if(distanceX < 0)
        {
            bananaDistance = -0.5f;
        } else
        {
            bananaDistance = 0.5f;
        }

        float bananaX = getCurrentCharacterTransform().position.x + bananaDistance;
        float bananaY = getCurrentCharacterTransform().position.y + 0.5f;

        float bananaSpeed = MAX_BANANA_SPEED * power / MAX_POWER;

        Debug.Log("Power: " + power + ". Hitting with bat with " + bananaSpeed + " speed at " + bananaX + ", " + bananaY);
        GameObject bananaObject = instantiateBatProjectile(bananaX, bananaY);
        BatProjectile banana = bananaObject.GetComponent<BatProjectile>();
        banana.initialSpeed = bananaSpeed;
        banana.normalizedDirection = normalizedDistanceVector;
        banana.sender = characters[currentTeam, currentCharacter];
        banana.initialPosition = getCurrentCharacterTransform().position;
        status = "fired";
    }

    private void frozenBananaAction()
    {
        Debug.Log("Tirar banana congelada");
        status = "fired";
    }

    private bool isChargeable(string weaponName)
    {
        switch (weaponName) 
        {
        case "banana":
        case "frozen banana":
        case "bat":
            return true;
        default:
            return false;
        }
    }
}