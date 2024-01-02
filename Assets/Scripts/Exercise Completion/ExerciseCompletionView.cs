using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseCompletionView : MonoBehaviour
{
    [SerializeField] private Button startAndStopCompletingExercises;
	[SerializeField] private Image progressBarOfCompletion;
	[SerializeField] private Image markOfCompletion;
	[SerializeField] private Image[] exerciseImages;

	public Action OnSwitchableTimerButtonClicked;

	private void Start()
	{
		startAndStopCompletingExercises.onClick.AddListener(() => OnSwitchableTimerButtonClicked?.Invoke());
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

	public void ChangeProgressBarOfCompletion(float fillAmountOfProgressBar) => progressBarOfCompletion.fillAmount = fillAmountOfProgressBar;

	public void SetVisibillityOfMarkCompletion(bool visible) => markOfCompletion.enabled = visible;
}
