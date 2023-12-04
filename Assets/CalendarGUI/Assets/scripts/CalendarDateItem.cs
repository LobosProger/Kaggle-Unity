using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CalendarDateItem : MonoBehaviour {

    [SerializeField] private Text dayText;
	[SerializeField] private CanvasGroup canvasGroup;
    private Button dateItemButton;
    private DateTime dateTime;

	private void Awake()
	{
		dateItemButton = GetComponent<Button>();
        dateItemButton.onClick.AddListener(OnDateCompletingExerciseClick);
	}

	public void ShowDateItem(DateTime dateTime)
    {
		canvasGroup.alpha = 1;
		canvasGroup.blocksRaycasts = true;

		this.dateTime = dateTime;
        dayText.text = dateTime.Day.ToString();
    }

	public void HideItemDatePicker()
	{
		canvasGroup.alpha = 0;
		canvasGroup.blocksRaycasts = false;
	}

	public void OnDateCompletingExerciseClick()
	{
		//dateItemButton.interactable = false;
        CalendarController._calendarInstance.OnClickDateForCompleteExerciseAction?.Invoke(dateTime);
	}

	// Button setuped in way, that when it becomes non interactable, it
	// highlights in needing color
	public void HighlightDateItem() => dateItemButton.interactable = false;
}
