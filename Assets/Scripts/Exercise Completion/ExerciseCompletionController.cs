using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseCompletionController : MonoBehaviour
{
    [SerializeField] private ExerciseCompletionModel exerciseCompletionModel;
    [SerializeField] private ExerciseCompletionView exerciseCompletionView;
	[SerializeField] private PageSwitcherController mainPage;

	private bool isCompletingExercisesStarted = false;
	private int currentIndexOfSelectedExercises = 0;
	private float remainedOverallTimeOfCompletingExercises;
	private float remainedTimeOfCompletingCurrentExercise;

	private void OnEnable()
	{
		exerciseCompletionView.OnAddTimeClicked += AddMinuteToTime;
		exerciseCompletionView.OnRemoveTimeClicked += RemoveMinuteFromTime;
		exerciseCompletionView.OnStartClicked += StartCompletionOfExercise;
	}

	private void OnDisable()
	{
		exerciseCompletionView.OnAddTimeClicked -= AddMinuteToTime;
		exerciseCompletionView.OnRemoveTimeClicked -= RemoveMinuteFromTime;
		exerciseCompletionView.OnStartClicked -= StartCompletionOfExercise;
	}

	private void Update()
	{
		if(isCompletingExercisesStarted)
		{
			remainedOverallTimeOfCompletingExercises -= Time.deltaTime;
			remainedTimeOfCompletingCurrentExercise -= Time.deltaTime;
			exerciseCompletionView.ShowTimeOnTimer(remainedOverallTimeOfCompletingExercises);

			if(remainedTimeOfCompletingCurrentExercise <= 0)
			{
				remainedTimeOfCompletingCurrentExercise = exerciseCompletionModel.GetTimeOfCompletingCurrentExercise();
				SwitchIndexOfSelectedExercisesAndShowOnUI();
			}

			if(remainedOverallTimeOfCompletingExercises <= 0)
			{
				isCompletingExercisesStarted = false;
				OnCompletedExercise();
			}
		}
	}

	private void AddMinuteToTime()
	{
		exerciseCompletionModel.AddMinuteToTime();

		float timeOfCompletion = exerciseCompletionModel.GetTimeOfCompletionInSeconds();
		exerciseCompletionView.ShowTimeOnTimer(timeOfCompletion);
	}

	private void RemoveMinuteFromTime()
	{
		exerciseCompletionModel.RemoveMinuteToTime();

		float timeOfCompletion = exerciseCompletionModel.GetTimeOfCompletionInSeconds();
		exerciseCompletionView.ShowTimeOnTimer(timeOfCompletion);
	}

	private void StartCompletionOfExercise()
	{
		if(exerciseCompletionModel.GetTimeOfCompletionInSeconds() > 0)
		{
			remainedOverallTimeOfCompletingExercises = exerciseCompletionModel.GetTimeOfCompletionInSeconds();
			remainedTimeOfCompletingCurrentExercise = exerciseCompletionModel.GetTimeOfCompletingCurrentExercise();
			currentIndexOfSelectedExercises = 0;
			SwitchIndexOfSelectedExercisesAndShowOnUI();
			
			exerciseCompletionView.HideSetupingPanelOfTimer();
			isCompletingExercisesStarted = true;
		}
	}

	private void SwitchIndexOfSelectedExercisesAndShowOnUI()
	{
		bool[] allExercises = exerciseCompletionModel.AllExercises;
		for (int i = currentIndexOfSelectedExercises; i< allExercises.Length; i++)
		{
			if (allExercises[i]) // If this exercise selected from list by user
			{
				currentIndexOfSelectedExercises = i;
				exerciseCompletionView.ShowCurrentCompletingExerciseByIndex(currentIndexOfSelectedExercises);
				currentIndexOfSelectedExercises++;
				break;
			}
		}
	}

	private void OnCompletedExercise()
	{
		exerciseCompletionView.ShowSetupingPanelOfTimer();
		mainPage.ShowThisPage();
	}
}
