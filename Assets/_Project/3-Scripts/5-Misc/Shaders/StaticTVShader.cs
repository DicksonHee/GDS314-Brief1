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

	private void Awake()
	{
		shaderRenderer = GetComponent<Renderer>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			StaticOn();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			StaticOff();
		}
	}

	public void StaticOn() => StartCoroutine(StaticOn_CO());
	public void StaticOff() => StartCoroutine(StaticOff_CO());

	private IEnumerator StaticOn_CO()
	{
		float startTime = Time.time;
		float startYVal = shaderRenderer.sharedMaterial.GetFloat("yScroll");
		float startIntensity = shaderRenderer.sharedMaterial.GetFloat("Intensity");

		while (Time.time < startTime + duration)
		{
			shaderRenderer.sharedMaterial.SetFloat("yScroll", Mathf.Lerp(startYVal, minYVal, (Time.time - startTime) / duration));
			shaderRenderer.sharedMaterial.SetFloat("Intensity", Mathf.Lerp(startIntensity, minStaticVal, (Time.time - startTime) / duration));
			yield return null;
		}
	}

	private IEnumerator StaticOff_CO()
	{
		float startTime = Time.time;
		float startYVal = shaderRenderer.sharedMaterial.GetFloat("yScroll");
		float startIntensity = shaderRenderer.sharedMaterial.GetFloat("Intensity");

		while (Time.time < startTime + duration)
		{
			shaderRenderer.sharedMaterial.SetFloat("yScroll", Mathf.Lerp(startYVal, maxYVal, (Time.time - startTime) / duration));
			shaderRenderer.sharedMaterial.SetFloat("Intensity", Mathf.Lerp(startIntensity, maxStaticVal, (Time.time - startTime) / duration));
			yield return null;
		}
	}
}
