using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public static class MathExtensions
{
	public static void MoveAtAngle(this Vector2 vector, float angle)
	{
		vector.x += (int)Mathf.Cos(angle);
		vector.y += (int)Mathf.Sin(angle);
	}

		// Function to set the value of a specific bit based on the angle
	public static int SetBit(this int integer, int index, int value)
	{
		// Set or clear the bit at the calculated index
		if (value == 1)
			integer |= 1 << index; // Set bit
		else
			integer &= ~(1 << index); // Clear bit
		return integer;
	}
}
