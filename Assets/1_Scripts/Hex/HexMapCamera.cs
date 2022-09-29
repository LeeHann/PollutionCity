﻿using UnityEngine;

public class HexMapCamera : MonoBehaviour {

	public float stickMinZoom, stickMaxZoom;

	public float swivelMinZoom, swivelMaxZoom;

	public float moveSpeedMinZoom, moveSpeedMaxZoom;

	public float rotationSpeed;

	Transform swivel, stick;

	public HexGrid grid;

	float zoom = 1f;

	float rotationAngle;

	static HexMapCamera instance;

	public static bool Locked {
		set {
			instance.enabled = !value;
		}
	}

	public static void ValidatePosition () {
		instance.AdjustPosition(0f, 0f);
	}

	void Awake () {
		swivel = transform.GetChild(0);
		stick = swivel.GetChild(0);
	}

	void OnEnable () {
		instance = this;
		ValidatePosition();
	}

	void Update () {
#if UNITY_ANDROID
		int touchCount = Input.touchCount;
		// ray; UI 있는 곳에서는 패닝, 줌 무시

		// zoom
		if (touchCount >= 2)
		{
			float zoomDelta = CalculateZoomValue();
			if (zoomDelta != 0f) {
				AdjustZoom(zoomDelta);
			}
		}

		// panning
		if (touchCount >= 1)
		{
			float xDelta = Input.GetTouch(0).deltaPosition.x * -0.05f;
			float zDelta = Input.GetTouch(0).deltaPosition.y * -0.05f;
			if (xDelta != 0f || zDelta != 0f) {
				AdjustPosition(xDelta, zDelta);
			}
		}
#else
		float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
		if (zoomDelta != 0f) {
			AdjustZoom(zoomDelta);
		}
		
		float rotationDelta = Input.GetAxis("Rotation");
		if (rotationDelta != 0f) {
			AdjustRotation(rotationDelta);
		}

		float xDelta = Input.GetAxis("Horizontal");
		float zDelta = Input.GetAxis("Vertical");
		if (xDelta != 0f || zDelta != 0f) {
			AdjustPosition(xDelta, zDelta);
		}
#endif
	}

	float CalculateZoomValue()
	{
		var curTouchAPos = Input.GetTouch(0).position;                      // 현재 터치 중인 터치 1번 손가락
        var curTouchBPos = Input.GetTouch(1).position;                      // 현재 터치 중인 터치 2번 손가락
        var prevTouchAPos = curTouchAPos - Input.GetTouch(0).deltaPosition; // 이전 프레임 1번 손가락
        var prevTouchBPos = curTouchBPos - Input.GetTouch(1).deltaPosition; // 이전 프레임 2번 손가락
        var deltaDistance =
        Vector2.Distance(Normalize(curTouchAPos), Normalize(curTouchBPos))
        - Vector2.Distance(Normalize(prevTouchAPos), Normalize(prevTouchBPos));

		return deltaDistance;
	}

	 private Vector2 Normalize(Vector2 position) { // 해상도 표준화
        var normalizedPos = new Vector2(
            (position.x - Screen.width * 0.5f) / (Screen.width * 0.5f),
            (position.y - Screen.height * 0.5f) / (Screen.height * 0.5f));
        return normalizedPos;
    }


	void AdjustZoom (float delta) {
		zoom = Mathf.Clamp01(zoom + delta);

		float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
		stick.localPosition = new Vector3(0f, 0f, distance);

		float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
		swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
	}

	void AdjustRotation (float delta) {
		rotationAngle += delta * rotationSpeed * Time.deltaTime;
		if (rotationAngle < 0f) {
			rotationAngle += 360f;
		}
		else if (rotationAngle >= 360f) {
			rotationAngle -= 360f;
		}
		transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
	}

	void AdjustPosition (float xDelta, float zDelta) {
		Vector3 direction =
			transform.localRotation *
			new Vector3(xDelta, 0f, zDelta).normalized;
		float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
		float distance =
			Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) *
			damping * Time.deltaTime;

		Vector3 position = transform.localPosition;
		position += direction * distance;
		transform.localPosition =
			grid.wrapping ? WrapPosition(position) : ClampPosition(position);
	}

	public Vector3 ClampPosition (Vector3 position) {
		float xMax = (grid.cellCountX - 0.5f) * HexMetrics.innerDiameter;
		position.x = Mathf.Clamp(position.x, 0f, xMax);

		float zMax = (grid.cellCountZ - 1) * (1.5f * HexMetrics.outerRadius);
		position.z = Mathf.Clamp(position.z, 0f, zMax);

		return position;
	}

	public Vector3 WrapPosition (Vector3 position) {
		float width = grid.cellCountX * HexMetrics.innerDiameter;
		while (position.x < 0f) {
			position.x += width;
		}
		while (position.x > width) {
			position.x -= width;
		}

		float zMax = (grid.cellCountZ - 1) * (1.5f * HexMetrics.outerRadius);
		position.z = Mathf.Clamp(position.z, 0f, zMax);

		grid.CenterMap(position.x);
		return position;
	}
}