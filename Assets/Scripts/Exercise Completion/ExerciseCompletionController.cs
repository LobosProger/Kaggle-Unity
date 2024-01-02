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
		exerciseCompletionView.OnSwitchableTimerButtonClicked += SwitchStateCompletionOfExercise;
		ExerciseEvents.OnExerciseSelectedForCompletion += (dateTimeSelected) => { selectedDateTimeForCompletion = dateTimeSelected; };
	}

	private void OnDisable()
	{
		exerciseCompletionView.OnSwitchableTimerButtonClicked -= SwitchStateCompletionOfExercise;
		ExerciseEvents.OnExerciseSelectedForCompletion -= (dateTimeSelected) => { selectedDateTimeForCompletion = dateTimeSelected; };
	}

	private void Update()
	{
		if(isCompletingExercisesStarted)
		{
			remainedOverallTimeOfCompletingExercises -= Time.deltaTime;
			remainedTimeOfCompletingCurrentExercise -= Time.deltaTime;
			
			float fillAmount = Mathf.InverseLerp(exerciseCompletionModel.GetTimeOfCompletionAllExercisesInSeconds(), 0, remainedOverallTimeOfCompletingExercises);
			exerciseCompletionView.ChangeProgressBarOfCompletion(fillAmount);

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

	private void SwitchStateCompletionOfExercise()
	{
		if(!isCompletingExercisesStarted)
		{
			if (exerciseCompletionModel.GetTimeOfCompletionAllExercisesInSeconds() > 0)
			{
				remainedOverallTimeOfCompletingExercises = exerciseCompletionModel.GetTimeOfCompletionAllExercisesInSeconds();
				remainedTimeOfCompletingCurrentExercise = exerciseCompletionModel.GetTimeOfCompletingCurrentExercise();
				currentIndexOfSelectedExercises = 0;
				totalAmountOfCompletedExercisesDuringSession = 1;
				SwitchIndexOfSelectedExercisesAndShowOnUI();
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
		bool[] allExercises = exerciseCompletionModel.GetAllExercises();
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
		float totalAmountOfCompletionInSeconds = exerciseCompletionModel.GetTimeOfCompletionAllExercisesInSeconds() - remainedOverallTimeOfCompletingExercises;
		ExerciseEvents.OnExerciseCompleted?.Invoke(selectedDateTimeForCompletion, totalAmountOfCompletedExercisesDuringSession, totalAmountOfCompletionInSeconds);
		StartCoroutine(ReturningBackToMainPage());
	}

	private IEnumerator ReturningBackToMainPage()
	{
		exerciseCompletionView.ChangeProgressBarOfCompletion(1f);
		exerciseCompletionView.SetVisibillityOfMarkCompletion(true);
		yield return new WaitForSeconds(1.5f);
		mainPage.ShowThisPage();
		exerciseCompletionView.ChangeProgressBarOfCompletion(0f);
		exerciseCompletionView.SetVisibillityOfMarkCompletion(false);
		exerciseCompletionView.HideAllImagesOfExercises();
	}
}
