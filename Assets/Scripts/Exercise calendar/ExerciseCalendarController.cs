using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseCalendarController : MonoBehaviour
{
	[SerializeField] private ExerciseCalendarModel exerciseCalendarModel;
	[SerializeField] private CalendarController exerciseCalendarView;
	[SerializeField] private PageSwitcherController exerciseCompletionPage;

	private void Start()
	{
		HashSet<DateTime> completedExerciseDates = exerciseCalendarModel.GetCompletedExercisesByDate();
		exerciseCalendarView.ShowCompletedExercisesByDates(completedExerciseDates);
	}

	private void OnEnable()
	{
		exerciseCalendarView.OnClickDateForCompleteExerciseAction += SelectExerciseByDate;
		ExerciseEvents.OnExerciseCompleted += (dateTimeCompletion, _, _) => OnCompleteExercise(dateTimeCompletion);
	}

	private void OnDisable()
	{
		exerciseCalendarView.OnClickDateForCompleteExerciseAction -= SelectExerciseByDate;
		ExerciseEvents.OnExerciseCompleted -= (dateTimeCompletion, _, _) => OnCompleteExercise(dateTimeCompletion);
	}

	private void SelectExerciseByDate(DateTime dateTime)
	{
		ExerciseEvents.OnExerciseSelectedForCompletion?.Invoke(dateTime);
		exerciseCompletionPage.ShowThisPage();
	}

	private void OnCompleteExercise(DateTime dateTimeCompletion)
	{
		exerciseCalendarModel.AddCompletedExerciseDate(dateTimeCompletion);
		Debug.Log("complete exercise!");
	}
}
