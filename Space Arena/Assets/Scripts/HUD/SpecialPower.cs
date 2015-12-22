using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class SpecialPower : MonoBehaviour {

    public RectTransform powerBar;
    private float yPos;
    private float maxX;
    private float minX;
    private int currentPower;
    public int maxPower;
    public Image visualPower;

    // Use this for initialization
    void Start () {
        yPos = powerBar.position.y;
        maxX = powerBar.position.x;
        minX = powerBar.position.x - powerBar.rect.width;
        currentPower = 0;
    }
	
	// Update is called once per frame
	void Update () {
             
    }

    public void PowerUp() {
        currentPower += 100;
        float currentXvalue = MapValues(currentPower, 0, maxPower, minX, maxX);
        powerBar.position = new Vector3(currentXvalue, yPos);
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * ((outMax - outMin) / (inMax - inMin)) + outMin;
    }



}
