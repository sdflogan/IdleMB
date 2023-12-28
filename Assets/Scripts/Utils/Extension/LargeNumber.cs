/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System;

namespace TinyBytes.Utils.Extension
{
	public static class LargeNumber
	{
		/// <summary>
		/// Displays big number in an easy way to read it.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToAlphabet(this double value)
        {
			// Determine the power of 10 for the given number
			int power = (int)Math.Log10(Math.Abs(value)) / 3;

			// Convert the number to a more readable form
			double formattedNumber = value / Math.Pow(10, power * 3);

			// Format the result as a string
			string result = $"{formattedNumber:0.##}{GetSuffix(power)}";

			return result;
		}

		private static string GetSuffix(int power)
        {
			// Define the suffixes for different powers of 10
			string[] shortSuffixes = { "", "k", "m", "b", "t", "q", "Q", "s", "S", "o", "n", "d" };

			if (power < shortSuffixes.Length)
            {
				return shortSuffixes[power];
            }
            else
            {
				var bigSuffixPower = power - shortSuffixes.Length;
				return GetBigSuffix(bigSuffixPower);
            }
		}

		private static string GetBigSuffix(int power)
        {
			int firstLetter = 0;
			int secondLetter = 0;

			while (power >= 3)
            {
				secondLetter = (int)Math.Min(25, power / 3);
				power -= 3;

				if (power >= 3)
                {
					firstLetter = (firstLetter + 1) % 26;
                }
            }

			return GetLetters(firstLetter, secondLetter);
		}

		private static string GetLetters(int firstLetter, int secondLetter)
        {
			char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
			return $"{alphabet[firstLetter]}{alphabet[secondLetter]}";
		}
	}
}