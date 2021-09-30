using UnityEngine;

namespace Knotgames.Extensions
{
    public static class ClipboardExtensions
    {
        /// <summary>
        /// Copies the string into the Clipboard.
        /// </summary>
        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }

        /// <summary>
        /// Gets the string from Clipboard.
        /// </summary>
        public static string GetClipboard()
        {
            return GUIUtility.systemCopyBuffer;
        }
    }
}