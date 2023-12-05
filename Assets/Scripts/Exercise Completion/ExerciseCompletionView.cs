using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseCompletionView : MonoBehaviour
{
	private const string textWhenStartedCompletionOfExercise = "Стоп";
	private const string textWhenStopedCompletionOfExercise = "Старт!";

	public Action OnAddTimeClicked;
	public Action OnRemoveTimeClicked;
	public Action OnSwitchableTimerButtonClicked;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button addMinuteTimeButton;
    [SerializeField] private Button removeMinuteTimeButton;
    [SerializeField] private Button startAndStopCompletingExerciseButton;
	[SerializeField] private TMP_Text startAndStopStateOfButton;
	[SerializeField] private Image progressBarOfCompletion;
	[SerializeField] private Image iconOfCompletion;
	[SerializeField] private CanvasGroup setupingPanelOfTimer;
	[SerializeField] private Image[] exerciseImages;

	private void Start()
	{
		addMinuteTimeButton.onClick.AddListener(OnAddTimeButtonClicked);
		removeMinuteTimeButton.onClick.AddListener(OnRemoveTimeButtonClicked);
		startAndStopCompletingExerciseButton.onClick.AddListener(OnSwitchableButtonClicked);
	}

	public void HideSetupingPanelOfTimer()
	{
		setupingPanelOfTimer.alpha = 0;
		setupingPanelOfTimer.blocksRaycasts = false;
	}

	public void ShowSetupingPanelOfTimer()
	{
		setupingPanelOfTimer.alpha = 1;
		setupingPanelOfTimer.blocksRaycasts = true;
	}

	public void ShowCurrentCompletingExerciseByIndex(int index)
	{
		foreach (var eachImage in exerciseImages)
		{
			eachImage.enabled = false;
		}

		exerciseImages[index].enabled = true;
	}

	public void HideAllImagesOfExercises()
	{
		foreach (var eachImage in exerciseImages)
		{
			eachImage.enabled = false;
		}
	}

	public void ShowProgressOfCompletion(float remainedTime, float fillAmountOfProgressBar)
	{
		TimeSpan formatTime = TimeSpan.FromSeconds(remainedTime);
		timerText.text = formatTime.ToString("mm':'ss");
		progressBarOfCompletion.fillAmount = fillAmountOfProgressBar;
	}

	public void SetVisibillityOfIconOfCompletedAllExercises(bool visible)
	{
		iconOfCompletion.enabled = visible;
	}

	private void OnAddTimeButtonClicked()
	{
		OnAddTimeClicked?.Invoke();
	}

	private void OnRemoveTimeButtonClicked()
	{
		OnRemoveTimeClicked?.Invoke();
	}

	private void OnSwitchableButtonClicked()
	{
		OnSwitchableTimerButtonClicked?.Invoke();
	}

	public void ChangeStateOfSTartExerciseButton(bool isStartedCompletion)
	{
		if(isStartedCompletion)
		{
			startAndStopStateOfButton.text = textWhenStartedCompletionOfExercise;
		} else
		{
			startAndStopStateOfButton.text = textWhenStopedCompletionOfExercise;
		}
	}
}
