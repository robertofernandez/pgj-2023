using UnityEngine;

public class CharactersManager : MonoBehaviour {
    public GameObject simpleMonkey;
    private Transform currentCharacterTransform;

	void Start() 
    {
        Debug.Log("Characters Manager Started");
        currentCharacterTransform = instantiateSimpleMonkey(0, 0).transform.Find("Character");
        instantiateSimpleMonkey(1, 0);
    }
    
    public GameObject instantiateSimpleMonkey(float x, float z) {
        Vector3 position = new Vector3(x, 0f, z);
        Quaternion rotation = Quaternion.identity;
        GameObject instantiatedPrefab = Instantiate(simpleMonkey, position, rotation);
        return instantiatedPrefab;
    }

    public Transform getCurrentCharacterTransform() 
    {
        return currentCharacterTransform;
    }
}