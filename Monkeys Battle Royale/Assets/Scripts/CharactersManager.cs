using UnityEngine;

public class CharactersManager : MonoBehaviour {
    public GameObject simpleMonkey;
    private Transform currentCharacterTransform;
    private Transform[,] teamsMembersTransforms;

    private Character[,] characters;

    public int currentTeam;
    public int currentCharacter;

    private int charactersAmount;

    private bool characterChanged = false;

	void Start() 
    {
        currentTeam = 0;
        currentCharacter = 0;
        charactersAmount = 2;

        Debug.Log("Characters Manager Started");
        teamsMembersTransforms = new Transform[2,2];
        characters = new Character[2,2];

        GameObject monkey1 = instantiateSimpleMonkey(0, 0);
        GameObject monkey2 = instantiateSimpleMonkey(1, 0);

        teamsMembersTransforms[0, 0] = monkey1.transform.Find("Character");
        teamsMembersTransforms[0, 1] = monkey2.transform.Find("Character");

        characters[0, 0] = teamsMembersTransforms[0, 0].GetComponent<Character>();
        characters[0, 1] = teamsMembersTransforms[0, 1].GetComponent<Character>();

        characters[currentTeam, currentCharacter].setCurrent(true);
    }
    
    public GameObject instantiateSimpleMonkey(float x, float z) {
        Vector3 position = new Vector3(x, 0f, z);
        Quaternion rotation = Quaternion.identity;
        GameObject instantiatedPrefab = Instantiate(simpleMonkey, position, rotation);
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
    }
}