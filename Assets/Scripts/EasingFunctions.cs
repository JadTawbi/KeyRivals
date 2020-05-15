using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingFunctions : MonoBehaviour
{
	public float easeNone(float t, float b, float c, float d)
	{
		return c * t / d + b;
	}
	public float easeIn(float t, float b, float c, float d)
	{
		return c * t / d + b;
	}
	public float easeOut(float t, float b, float c, float d)
	{
		return c * t / d + b;
	}
	public float easeInOut(float t, float b, float c, float d)
	{
		return c * t / d + b;
	}
}
