using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class ExerciseCalendarModel : SaveableModel
{
	private const string keyToAccessCompletedExercises = "Completed exercises";
	private HashSet<DateTime> completedDatesByExercise = new HashSet<DateTime>();

	protected override void ReadDataFromMemory()
	{
		string json = PlayerPrefs.GetString(keyToAccessCompletedExercises);

		if (json != "")
		{
			completedDatesByExercise = JsonConvert.DeserializeObject<HashSet<DateTime>>(json);
		}
	}

	protected override void SaveDataInMemory()
	{
		string json = JsonConvert.SerializeObject(completedDatesByExercise);
		PlayerPrefs.SetString(keyToAccessCompletedExercises, json);
	}

	public void AddCompletedExerciseDate(DateTime dateTime) => completedDatesByExercise.Add(dateTime);

	public HashSet<DateTime> GetCompletedExercisesByDate() => completedDatesByExercise;
}
