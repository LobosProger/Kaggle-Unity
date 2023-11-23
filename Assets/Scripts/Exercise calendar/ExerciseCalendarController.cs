using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseCalendarController : MonoBehaviour
{
	[SerializeField] private ExerciseCalendarModel exerciseCalendarModel;
	[SerializeField] private CalendarController exerciseCalendarView;

	private void Start()
	{
		HashSet<DateTime> completedExerciseDates = exerciseCalendarModel.GetCompletedExercisesByDate();
		exerciseCalendarView.ShowCompletedExercisesByDates(completedExerciseDates);
	}

	private void OnEnable()
	{
		exerciseCalendarView.OnClickDateForCompleteExerciseAction += OnCompletedExerciseByDate;
	}

	private void OnDisable()
	{
		exerciseCalendarView.OnClickDateForCompleteExerciseAction -= OnCompletedExerciseByDate;
	}

	private void OnCompletedExerciseByDate(DateTime dateTime)
	{
		Debug.Log("complete exercise!");
		exerciseCalendarModel.AddCompletedExerciseDate(dateTime);
	}
}
