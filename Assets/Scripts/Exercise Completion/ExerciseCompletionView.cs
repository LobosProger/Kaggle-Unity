using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseCompletionView : MonoBehaviour
{
	public Action OnAddTimeClicked;
	public Action OnRemoveTimeClicked;
	public Action OnStartClicked;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button addMinuteTimeButton;
    [SerializeField] private Button removeMinuteTimeButton;
    [SerializeField] private Button startCompletingExerciseButton;
	[SerializeField] private CanvasGroup setupingPanelOfTimer;
	[SerializeField] private Image[] exerciseImages;

	private void Start()
	{
		addMinuteTimeButton.onClick.AddListener(OnAddTimeButtonClicked);
		removeMinuteTimeButton.onClick.AddListener(OnRemoveTimeButtonClicked);
		startCompletingExerciseButton.onClick.AddListener(OnStartButtonClicked);
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

	public void ShowTimeOnTimer(float time)
	{
		TimeSpan formatTime = TimeSpan.FromSeconds(time);
		timerText.text = formatTime.ToString("mm':'ss");
	}

	private void OnAddTimeButtonClicked()
	{
		OnAddTimeClicked?.Invoke();
	}

	private void OnRemoveTimeButtonClicked()
	{
		OnRemoveTimeClicked?.Invoke();
	}

	private void OnStartButtonClicked()
	{
		OnStartClicked?.Invoke();
	}
}
