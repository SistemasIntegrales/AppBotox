using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler
{
	[SerializeField]
	private VideoPlayer VideoPlayer;

	private Image _progress;

	private void Awake()
	{
		_progress = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (VideoPlayer.frameCount > 0)
		{
			_progress.fillAmount = (float) VideoPlayer.frame / (float) VideoPlayer.frameCount;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		TrySkip(eventData);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		TrySkip(eventData);
	}

	private void TrySkip(PointerEventData eventData)
	{
		Vector2 localPoint;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_progress.rectTransform, eventData.position, null, out localPoint))
		{
			float pct = Mathf.InverseLerp(_progress.rectTransform.rect.xMin, _progress.rectTransform.rect.xMax,
				localPoint.x);
			SkipToPercent(pct);
		}
	}

	private void SkipToPercent(float pct)
	{
		var frame = VideoPlayer.frameCount * pct;
		VideoPlayer.frame = (long) frame;
	}
}
