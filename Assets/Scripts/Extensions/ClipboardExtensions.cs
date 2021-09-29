using UnityEngine;

namespace Knotgames.Extensions
{
    public static class ClipboardExtensions
    {
        /// <summary>
        /// Puts the string into the Clipboard.
        /// </summary>
        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
    }
}