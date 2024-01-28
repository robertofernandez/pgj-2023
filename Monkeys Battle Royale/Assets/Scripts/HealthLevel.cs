using UnityEngine;

public class HealthLevel : MonoBehaviour
{
    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
        //setHealth(50f);
    }
    void Update()
    {
        //-0.31
        //-0.04
        //0.3
        //0.07
        /*
        if(transform.localScale.x > 0.07f)
        {
            transform.localScale = new Vector2(transform.localScale.x - 0.001f, transform.localScale.y);
            transform.localPosition = new Vector2(transform.localPosition.x - 0.00117f, transform.localPosition.y);
        } else {
            transform.localScale = new Vector2(0.3f, transform.localScale.y);
            transform.localPosition = originalPosition;
        }
        */
    }

    public void setHealth(float percentage)
    {
        float rangePositionX = 0.31f - 0.04f;
        float rangeScaleX = 0.3f - 0.07f;

        float targetpositionX = originalPosition.x - rangePositionX * (100 - percentage) / 100;
        float targetScaleX = 0.07f + rangeScaleX * percentage / 100;

        Debug.Log("base bar position =" + originalPosition.x);
        Debug.Log("new bar position =" + targetpositionX + "(" + percentage + "%)");

        transform.localScale = new Vector2(targetScaleX, transform.localScale.y);
        transform.localPosition = new Vector2(targetpositionX, transform.localPosition.y);
    }
}