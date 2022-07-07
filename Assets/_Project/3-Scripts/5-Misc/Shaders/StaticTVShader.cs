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

	private void Awake()
	{
		shaderRenderer = GetComponent<Renderer>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && !isStaticOn)
		{
			StaticOn();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && isStaticOn)
		{
			StaticOff();
		}

		if (isStaticOn)
			shaderRenderer.sharedMaterials[midScreen].SetFloat("_yScroll", shaderRenderer.sharedMaterials[midScreen].GetFloat("_yScroll") + 1 * Time.deltaTime);
	}

	public void StaticOn() => StartCoroutine(StaticOn_CO());
	public void StaticOff() => StartCoroutine(StaticOff_CO());

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
		float startYVal = shaderRenderer.sharedMaterials[midScreen].GetFloat("_yScroll");
		float startIntensity = shaderRenderer.sharedMaterials[midScreen].GetFloat("_Intensity");

		while (Time.time < startTime + duration)
		{
			shaderRenderer.sharedMaterials[midScreen].SetFloat("_yScroll", staticOnCurve.Evaluate((Time.time - startTime) / duration) * maxYVal);
			shaderRenderer.sharedMaterials[midScreen].SetFloat("_Intensity", staticOnCurve.Evaluate((Time.time - startTime) / duration) * maxStaticVal);
			yield return null;
		}
	}
}
