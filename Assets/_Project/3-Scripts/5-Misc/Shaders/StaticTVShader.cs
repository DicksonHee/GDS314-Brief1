using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTVShader : MonoBehaviour
{
    private Renderer shaderRenderer;

	public float duration;
	public float maxYVal;
	public float minYVal;
	public float maxStaticVal;
	public float minStaticVal;

	public int leftScreen;
	public int midScreen;
	public int rightScreen;

	public AnimationCurve staticOnCurve;
	public AnimationCurve staticOffCurve;

	private bool isStaticOn;
	private Dictionary<int, bool> staticOnScreens;

	private void Awake()
	{
		shaderRenderer = GetComponent<Renderer>();
		staticOnScreens = new();
	}

	private void Update()
	{
		foreach (var screen in staticOnScreens)
		{
			if(screen.Value) shaderRenderer.sharedMaterials[screen.Key].SetFloat("_yScroll", shaderRenderer.sharedMaterials[midScreen].GetFloat("_yScroll") + 1 * Time.deltaTime);
		}
		
	}

	public void StaticOn(StaticScreenPos pos)
	{
		switch (pos)
		{
			case StaticScreenPos.Left:
				staticOnScreens[leftScreen] = true;
				break;
			case StaticScreenPos.Right:
				staticOnScreens[rightScreen] = true;
				break;
			case StaticScreenPos.Mid:
				staticOnScreens[midScreen] = true;
				break;
		}

		StartCoroutine(StaticOn_CO());
	}
	public void StaticOff(StaticScreenPos pos)
	{
		switch (pos)
		{
			case StaticScreenPos.Left:
				staticOnScreens[leftScreen] = false;
				break;
			case StaticScreenPos.Right:
				staticOnScreens[rightScreen] = false;
				break;
			case StaticScreenPos.Mid:
				staticOnScreens[midScreen] = false;
				break;
		}

		StartCoroutine(StaticOff_CO());
	}

	private IEnumerator StaticOff_CO()
	{
		isStaticOn = false;
		float startTime = Time.time;

		while (Time.time < startTime + duration)
		{
			shaderRenderer.sharedMaterials[midScreen].SetFloat("_yScroll", staticOffCurve.Evaluate((Time.time - startTime) / duration) * maxYVal);
			shaderRenderer.sharedMaterials[midScreen].SetFloat("_Intensity", staticOffCurve.Evaluate((Time.time - startTime) / duration) * maxStaticVal);
			yield return null;
		}

		shaderRenderer.sharedMaterials[midScreen].SetFloat("_yScroll", minYVal);
		shaderRenderer.sharedMaterials[midScreen].SetFloat("_Intensity", minStaticVal);
	}

	private IEnumerator StaticOn_CO()
	{
		isStaticOn = true;
		float startTime = Time.time;

		while (Time.time < startTime + duration)
		{
			shaderRenderer.sharedMaterials[midScreen].SetFloat("_yScroll", staticOnCurve.Evaluate((Time.time - startTime) / duration) * maxYVal);
			shaderRenderer.sharedMaterials[midScreen].SetFloat("_Intensity", staticOnCurve.Evaluate((Time.time - startTime) / duration) * maxStaticVal);
			yield return null;
		}
	}

	private void OnApplicationQuit()
	{
		shaderRenderer.sharedMaterials[0].SetFloat("_yScroll", 0);
		shaderRenderer.sharedMaterials[0].SetFloat("_Intensity", 0);
		shaderRenderer.sharedMaterials[1].SetFloat("_yScroll", 0);
		shaderRenderer.sharedMaterials[1].SetFloat("_Intensity", 0);
		shaderRenderer.sharedMaterials[2].SetFloat("_yScroll", 0);
		shaderRenderer.sharedMaterials[2].SetFloat("_Intensity", 0);
	}

	private void OnDisable()
	{
		shaderRenderer.sharedMaterials[0].SetFloat("_yScroll", 0);
		shaderRenderer.sharedMaterials[0].SetFloat("_Intensity", 0);
		shaderRenderer.sharedMaterials[1].SetFloat("_yScroll", 0);
		shaderRenderer.sharedMaterials[1].SetFloat("_Intensity", 0);
		shaderRenderer.sharedMaterials[2].SetFloat("_yScroll", 0);
		shaderRenderer.sharedMaterials[2].SetFloat("_Intensity", 0);
	}
}

public enum StaticScreenPos
{
	Left,
	Right,
	Mid
}
