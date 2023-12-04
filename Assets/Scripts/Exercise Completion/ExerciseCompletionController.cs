using System;
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
	private int totalAmountOfCompletedExercisesDuringSession = 1; // After start, we highlight first exercise, which user will complete

	private DateTime selectedDateTimeForCompletion;

	private void OnEnable()
	{
		exerciseCompletionView.OnAddTimeClicked += AddMinuteToTime;
		exerciseCompletionView.OnRemoveTimeClicked += RemoveMinuteFromTime;
		exerciseCompletionView.OnSwitchableTimerButtonClicked += SwitchStateCompletionOfExercise;
		ExerciseEvents.OnExerciseSelectedForCompletion += (dateTimeSelected) => { selectedDateTimeForCompletion = dateTimeSelected; };
	}

	private void OnDisable()
	{
		exerciseCompletionView.OnAddTimeClicked -= AddMinuteToTime;
		exerciseCompletionView.OnRemoveTimeClicked -= RemoveMinuteFromTime;
		exerciseCompletionView.OnSwitchableTimerButtonClicked -= SwitchStateCompletionOfExercise;
		ExerciseEvents.OnExerciseSelectedForCompletion -= (dateTimeSelected) => { selectedDateTimeForCompletion = dateTimeSelected; };
	}

	private void Update()
	{
		if(isCompletingExercisesStarted)
		{
			remainedOverallTimeOfCompletingExercises -= Time.deltaTime;
			remainedTimeOfCompletingCurrentExercise -= Time.deltaTime;
			float fillAmount = Mathf.InverseLerp(exerciseCompletionModel.GetTimeOfCompletionInSeconds(), 0, remainedOverallTimeOfCompletingExercises);
			exerciseCompletionView.ShowProgressOfCompletion(remainedOverallTimeOfCompletingExercises, fillAmount);

			if(remainedTimeOfCompletingCurrentExercise <= 0)
			{
				remainedTimeOfCompletingCurrentExercise = exerciseCompletionModel.GetTimeOfCompletingCurrentExercise();
				SwitchIndexOfSelectedExercisesAndShowOnUI();
			}

			if(remainedOverallTimeOfCompletingExercises <= 0)
			{
				isCompletingExercisesStarted = false;
				OnCompletedExercisesSession();
			}
		}
	}

	private void AddMinuteToTime()
	{
		exerciseCompletionModel.AddMinuteToTime();

		float timeOfCompletion = exerciseCompletionModel.GetTimeOfCompletionInSeconds();
		exerciseCompletionView.ShowProgressOfCompletion(timeOfCompletion, 0f);
	}

	private void RemoveMinuteFromTime()
	{
		exerciseCompletionModel.RemoveMinuteToTime();

		float timeOfCompletion = exerciseCompletionModel.GetTimeOfCompletionInSeconds();
		exerciseCompletionView.ShowProgressOfCompletion(timeOfCompletion, 0f);
	}

	private void SwitchStateCompletionOfExercise()
	{
		if(!isCompletingExercisesStarted)
		{
			if (exerciseCompletionModel.GetTimeOfCompletionInSeconds() > 0)
			{
				remainedOverallTimeOfCompletingExercises = exerciseCompletionModel.GetTimeOfCompletionInSeconds();
				remainedTimeOfCompletingCurrentExercise = exerciseCompletionModel.GetTimeOfCompletingCurrentExercise();
				currentIndexOfSelectedExercises = 0;
				totalAmountOfCompletedExercisesDuringSession = 1;
				SwitchIndexOfSelectedExercisesAndShowOnUI();

				exerciseCompletionView.HideSetupingPanelOfTimer();
				isCompletingExercisesStarted = true;
			}
		} else
		{
			isCompletingExercisesStarted = false;
			OnCompletedExercisesSession();
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
				totalAmountOfCompletedExercisesDuringSession++;
				break;
			}
		}
	}

	private void OnCompletedExercisesSession()
	{
		// By this, we calculate total consumed time on completion during session, NOT selected
		float totalAmountOfCompletionInSeconds = exerciseCompletionModel.GetTimeOfCompletionInSeconds() - remainedOverallTimeOfCompletingExercises;
		ExerciseEvents.OnExerciseCompleted?.Invoke(selectedDateTimeForCompletion, totalAmountOfCompletedExercisesDuringSession, totalAmountOfCompletionInSeconds);
		StartCoroutine(ReturningBackToMainPage());
	}

	private IEnumerator ReturningBackToMainPage()
	{
		exerciseCompletionView.ShowProgressOfCompletion(600, 1f);
		exerciseCompletionView.SetVisibillityOfIconOfCompletedAllExercises(true);
		exerciseCompletionView.ChangeStateOfTimerButtonText();
		exerciseCompletionView.ShowSetupingPanelOfTimer();
		yield return new WaitForSeconds(1.5f);
		mainPage.ShowThisPage();
		exerciseCompletionView.ShowProgressOfCompletion(600, 0f);
		exerciseCompletionView.SetVisibillityOfIconOfCompletedAllExercises(false);
		exerciseCompletionView.HideAllImagesOfExercises();
	}
}
