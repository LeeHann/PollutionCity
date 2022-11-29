using UnityEngine;
using System;
public class HexFeatureManager : MonoBehaviour {

	public Transform[] special;

	Transform container;

	public void Clear () {
		if (container) {
			Destroy(container.gameObject);
		}
		container = new GameObject("Features Container").transform;
		container.SetParent(transform, false);
		//walls.Clear();
	}

	public void Apply () {
		// walls.Apply();
	}



    public void AddSpecialFeature(HexCell cell, Vector3 position)
	{
		HexHash hash = HexMetrics.SampleHashGrid(position);
		Transform instance = Instantiate(special[cell.SpecialIndex-1]);
		instance.localPosition = HexMetrics.Perturb(position);
		instance.localRotation = Quaternion.Euler(0f, 360f * hash.e, 0f);
		instance.SetParent(container, false);
	}



	public void AddLivingBuilding(HexCell cell, Vector3 position)
	{
		HexHash hash = HexMetrics.SampleHashGrid(position);
		Transform instance = Instantiate(special[0]);
		instance.localPosition = HexMetrics.Perturb(position);
		instance.localRotation = Quaternion.Euler(0f, 360f * hash.e, 0f);
		instance.SetParent(container, false);
	}

	public void AddResearchBuilding(HexCell cell, Vector3 position)
	{
		HexHash hash = HexMetrics.SampleHashGrid(position);
		Transform instance = Instantiate(special[1]);
		instance.localPosition = HexMetrics.Perturb(position);
		instance.localRotation = Quaternion.Euler(0f, 360f * hash.e, 0f);
		instance.SetParent(container, false);
	}
	public void AddIndustrialBuilding(HexCell cell, Vector3 position)
	{
		HexHash hash = HexMetrics.SampleHashGrid(position);
		Transform instance = Instantiate(special[2]);
		instance.localPosition = HexMetrics.Perturb(position);
		instance.localRotation = Quaternion.Euler(0f, 360f * hash.e, 0f);
		instance.SetParent(container, false);
	}

}