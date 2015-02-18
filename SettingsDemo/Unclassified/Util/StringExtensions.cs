﻿// Copyright (c) 2015, Yves Goergen, http://unclassified.software/
//
// Copying and distribution of this file, with or without modification, are permitted provided the
// copyright notice and this notice are preserved. This file is offered as-is, without any warranty.

using System;
using System.Text.RegularExpressions;

namespace Unclassified.Util
{
	/// <summary>
	/// Provides extension methods for strings.
	/// </summary>
	public static class StringExtensions
	{
		#region String conversions

		/// <summary>
		/// Converts a string value into a safe file name. Invalid characters and names are
		/// replaced by an underline.
		/// </summary>
		/// <param name="str">Source string to convert.</param>
		/// <returns></returns>
		public static string ToFileName(this string str)
		{
			// Source: http://msdn.microsoft.com/en-us/library/aa365247%28v=vs.85%29.aspx

			// Empty names are not allowed
			if (string.IsNullOrWhiteSpace(str))
			{
				str = "_";
			}

			// Replace reserved characters
			str = Regex.Replace(str, @"[\x00-\x1f<>:""/\|?*]+", "_");

			// Mask reserved names, also with extensions
			if (Regex.IsMatch(str, @"^(\.|\.\.|CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9])(\..*)?$", RegexOptions.IgnoreCase))
			{
				str = "_" + str;
			}

			// Remove spaces at the beginning or end, and dots at the end
			// (Unicode whitespace is not removed here as it's not a problem for the file system.)
			str = str.TrimStart(' ');
			str = str.TrimEnd(' ', '.');

			// The maximum length of a path is 260 chars. We only get a single component of it here
			// and there will probably be added an extension, a counter value or other parts, so
			// we'll be very conservative.
			int maxLength = 100;
			if (str.Length > maxLength)
			{
				str = str.Substring(0, maxLength);
			}

			return str;
		}

		/// <summary>
		/// Returns a new string in which a specified string is replaced with another specified
		/// string if its occurence is at the beginning of the current string.
		/// </summary>
		/// <param name="str">The source string.</param>
		/// <param name="search">The string to be replaced.</param>
		/// <param name="replacement">The string to replace the occurrence of <paramref name="search"/>.</param>
		/// <returns></returns>
		public static string ReplaceStart(this string str, string search, string replacement)
		{
			if (str.StartsWith(search))
			{
				return replacement + str.Substring(search.Length);
			}
			return str;
		}

		/// <summary>
		/// Retrieves a substring from this instance. The substring starts at a specified character
		/// position and has a specified length. If <paramref name="startIndex"/> is behind the
		/// end of the string an empty string is returned. If <paramref name="startIndex"/> is
		/// negative, it is counted backwards from the end of the string.
		/// </summary>
		/// <param name="str">The source string.</param>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance.</param>
		/// <returns></returns>
		public static string SafeSubstring(this string str, int startIndex)
		{
			if (str == null) return null;

			if (startIndex < 0)
			{
				startIndex = str.Length + startIndex;
				if (startIndex < 0)
				{
					startIndex = 0;
				}
			}
			if (startIndex >= str.Length)
			{
				return "";
			}
			return str.Substring(startIndex);
		}

		/// <summary>
		/// Retrieves a substring from this instance. The substring starts at a specified character
		/// position and has a specified length. If <paramref name="startIndex"/> is behind the
		/// end of the string, or if <paramref name="length"/> is longer than the string, an empty
		/// string is returned. If <paramref name="startIndex"/> or <paramref name="length"/> are
		/// negative, they are counted backwards from the end of the string.
		/// </summary>
		/// <param name="str">The source string.</param>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance.</param>
		/// <param name="length">The number of characters in the substring.</param>
		/// <returns></returns>
		public static string SafeSubstring(this string str, int startIndex, int length)
		{
			if (str == null) return null;

			if (startIndex < 0)
			{
				startIndex = str.Length + startIndex;
				if (startIndex < 0)
				{
					startIndex = 0;
				}
			}
			if (length < 0)
			{
				length = str.Length + length - startIndex;
				if (length <= 0)
				{
					return "";
				}
			}
			if (startIndex >= str.Length)
			{
				return "";
			}
			if (startIndex + length >= str.Length)
			{
				length = str.Length - startIndex;
			}
			return str.Substring(startIndex, length);
		}

		#endregion String conversions

		#region Number parsing

		/// <summary>
		/// Converts the current string to an <see cref="Int32"/> value. Throws an exception if the
		/// string cannot be converted.
		/// </summary>
		/// <param name="str">The string to convert.</param>
		/// <returns>The converted value.</returns>
		public static int ToInt32(this string str)
		{
			return Convert.ToInt32(str);
		}

		/// <summary>
		/// Converts the current string to an <see cref="Int32"/> value, if possible, or returns the
		/// fallback value instead.
		/// </summary>
		/// <param name="str">The string to convert.</param>
		/// <param name="fallback">The fallback value to return if the string cannot be converted.</param>
		/// <returns>The converted value.</returns>
		public static int TryToInt32(this string str, int fallback = 0)
		{
			int i;
			if (!int.TryParse(str, out i)) i = fallback;
			return i;
		}

		/// <summary>
		/// Converts the current string to an <see cref="Int64"/> value. Throws an exception if the
		/// string cannot be converted.
		/// </summary>
		/// <param name="str">The string to convert.</param>
		/// <returns>The converted value.</returns>
		public static long ToInt64(this string str)
		{
			return Convert.ToInt64(str);
		}

		/// <summary>
		/// Converts the current string to an <see cref="Int64"/> value, if possible, or returns the
		/// fallback value instead.
		/// </summary>
		/// <param name="str">The string to convert.</param>
		/// <param name="fallback">The fallback value to return if the string cannot be converted.</param>
		/// <returns>The converted value.</returns>
		public static long TryToInt64(this string str, long fallback = 0)
		{
			long l;
			if (!long.TryParse(str, out l)) l = fallback;
			return l;
		}

		#endregion Number parsing
	}
}
