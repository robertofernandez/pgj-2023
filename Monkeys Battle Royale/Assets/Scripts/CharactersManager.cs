using UnityEngine;

public class CharactersManager : MonoBehaviour {
    
    public int MAX_POWER = 400;

    public float MAX_BANANA_SPEED = 6;

    public GameObject powerBarUI;
    public GameObject simpleMonkey;
    public GameObject banana;
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

    private int currentBananasAmunition = 1;

    public string status= "holding";

    public int power = 1;

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

        teamsMembersTransforms[0, 0] = monkey1.transform.Find("Character");
        teamsMembersTransforms[0, 1] = monkey2.transform.Find("Character");

        teamsMembersTransforms[1, 0] = monkey3.transform.Find("Character");
        teamsMembersTransforms[1, 1] = monkey4.transform.Find("Character");

        characters[0, 0] = teamsMembersTransforms[0, 0].GetComponent<Character>();
        characters[0, 1] = teamsMembersTransforms[0, 1].GetComponent<Character>();
        characters[1, 0] = teamsMembersTransforms[1, 0].GetComponent<Character>();
        characters[1, 1] = teamsMembersTransforms[1, 1].GetComponent<Character>();

        characters[currentTeam, currentCharacter].setCurrent(true);

        timerElement = timerObject.GetComponent<Timer>();
        timerElement.onTimerEnd.AddListener(OnTimerEnd);
    }

    void OnTimerEnd()
    {
        Debug.Log("Timer ended, restaring...");
        timerElement.seconds = 25;
        timerElement.StartTimer();
        characters[currentTeam, currentCharacter].setCurrent(false);
        currentTeam = (currentTeam + 1) % teamsAmount;
        characters[currentTeam, currentCharacter].setCurrent(true);

        currentBananasAmunition = 1;
        status= "holding";
        power = 1;

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


    public Transform getCurrentCharacterTransform() 
    {
        return teamsMembersTransforms[currentTeam, currentCharacter];
    }

    void Update()
    {
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

        if(currentBananasAmunition > 0)
        {
            if(Input.GetKey(KeyCode.Space))
            {
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
                    }
                }
            } else
            {
                if (status == "charging")
                {
                    powerBarUI.SetActive(false);
                    currentBananasAmunition--;
        float bananaX = getCurrentCharacterTransform().position.x + 0.2f;
        float bananaY = getCurrentCharacterTransform().position.y + 0.2f;

        float bananaSpeed = MAX_BANANA_SPEED * power / MAX_POWER;

        Debug.Log("Power: " + power + "Dropping banana with " + bananaSpeed + " speed at " + bananaX + ", " + bananaY);
        GameObject bananaObject = instantiateBanana(bananaX, bananaY);
        Banana banana = bananaObject.GetComponent<Banana>();
        banana.initialSpeed = bananaSpeed;
        status = "fired";
                    //characters[(currentTeam + 1) % 2, currentCharacter].getHit(6, -1);
                }
            }
        }
    }
}