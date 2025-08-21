using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpinWheel : MonoBehaviour
{
	public List<BankaObject> prize;
	public List<AnimationCurve> animationCurves;
	
	private bool spinning;	
	private float anglePerItem;	
	private int randomTime;
	private int itemNumber;
	
	public event System.Action<BankaObject> Priz;
	
	private void Start()
	{
		spinning = false;
		anglePerItem = 360 / prize.Count;
	}
	
	public void Run()
	{
		if (!spinning)
		{
			randomTime = Random.Range (1, 4);
			itemNumber = Random.Range (0, prize.Count);
			float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);
			
			StartCoroutine(SpinTheWheel (5 * randomTime, maxAngle));
		}
	}
	
	private IEnumerator SpinTheWheel (float time, float maxAngle)
	{
		spinning = true;
		
		float timer = 0.0f;		
		float startAngle = transform.eulerAngles.z;		
		maxAngle -= startAngle;
		
		int animationCurveNumber = Random.Range (0, animationCurves.Count);
		Debug.Log ("Animation Curve No. : " + animationCurveNumber);
		
		while (timer < time) {
			float angle = maxAngle * animationCurves [animationCurveNumber].Evaluate (timer / time) ;
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
			timer += Time.deltaTime;
			yield return 0;
		}
		
		transform.eulerAngles = new Vector3 (0.0f, 0.0f, maxAngle + startAngle);
		spinning = false;

		Priz?.Invoke(prize[itemNumber]);
	}	
}
