using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseCalendarController : MonoBehaviour
{
	[SerializeField] private ExerciseCalendarModel exerciseCalendarModel;
	[SerializeField] private CalendarController exerciseCalendarView;
	[SerializeField] private PageSwitcherController exerciseCompletionPage;
	
	public Action OnExerciseCompleted;
	
	private DateTime selectedDateTimeForCompletion;

	private void Start()
	{
		HashSet<DateTime> completedExerciseDates = exerciseCalendarModel.GetCompletedExercisesByDate();
		exerciseCalendarView.ShowCompletedExercisesByDates(completedExerciseDates);
	}

	private void OnEnable()
	{
		exerciseCalendarView.OnClickDateForCompleteExerciseAction += SelectExerciseByDate;
		OnExerciseCompleted += OnCompleteExercise;
	}

	private void OnDisable()
	{
		exerciseCalendarView.OnClickDateForCompleteExerciseAction -= SelectExerciseByDate;
		OnExerciseCompleted -= OnCompleteExercise;
	}

	private void SelectExerciseByDate(DateTime dateTime)
	{
		selectedDateTimeForCompletion = dateTime;
		exerciseCompletionPage.ShowThisPage();
	}

	private void OnCompleteExercise()
	{
		Debug.Log("complete exercise!");
		exerciseCalendarModel.AddCompletedExerciseDate(selectedDateTimeForCompletion);
	}
}
