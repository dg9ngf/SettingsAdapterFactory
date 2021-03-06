// Copyright (c) 2015, Yves Goergen, http://unclassified.software/
//
// Copying and distribution of this file, with or without modification, are permitted provided the
// copyright notice and this notice are preserved. This file is offered as-is, without any warranty.

using System;

namespace Unclassified.UI
{
	/// <summary>
	/// This class is used to save the cost of boxing boolean values.
	/// </summary>
	internal static class BooleanBoxes
	{
		/// <summary>
		/// The boxed True value.
		/// </summary>
		public static readonly object TrueBox = true;

		/// <summary>
		/// The boxed False value.
		/// </summary>
		public static readonly object FalseBox = false;

		/// <summary>
		/// Returns a pre-boxed reference value for a boolean value.
		/// </summary>
		/// <param name="value">The boolean value to box.</param>
		/// <returns>The boxed value.</returns>
		public static object Box(bool value)
		{
			if (value)
			{
				return TrueBox;
			}
			else
			{
				return FalseBox;
			}
		}
	}
}
