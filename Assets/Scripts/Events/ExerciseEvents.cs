using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseEvents : MonoBehaviour
{
    public static Action<DateTime> OnExerciseSelectedForCompletion;
    public static Action<DateTime, int, float> OnExerciseCompleted;
}
