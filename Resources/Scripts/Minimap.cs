using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

	private Transform indicatorTransform;
	private int levelNumber;
	private Transform bearTransform;

	// Use this for initialization
	void Start () 
	{
		bearTransform = GameObject.Find("Bear").GetComponent<Transform>();
		indicatorTransform = GetComponent<Transform>().Find("indicator");
		levelNumber = (int)System.Char.GetNumericValue(Application.loadedLevelName[5]);
	}

	float Remap(float s, float a1, float a2, float b1, float b2)
	{
    return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
 
	// Update is called once per frame
	void Update () 
	{
		float bx = bearTransform.position.x;
		float bz = bearTransform.position.z;

		switch(levelNumber)
		{
			case 1:
				float ix = Remap(bz, 682f, 540f, -45f, 0f);
				float iy = Remap(bx, 364f, 486f, -30f, 8f);
				indicatorTransform.localPosition = new Vector3(ix, iy, 0f);
				break;
			case 2:
				ix = Remap(bz, 680f, 548f, -45f, -2f);
				iy = Remap(bx, 370f, 485f, -30f, 8f);
				indicatorTransform.localPosition = new Vector3(ix, iy, 0f);
				break;
			case 3:
				ix = Remap(bz, 680f, 548f, -45f, -2f);
				iy = Remap(bx, 370f, 485f, -30f, 8f);
				indicatorTransform.localPosition = new Vector3(ix, iy, 0f);
				break;
		}
	}
}
