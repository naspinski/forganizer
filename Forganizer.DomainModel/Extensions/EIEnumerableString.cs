using System.Collections.Generic;
using System.Linq;

namespace Forganizer.DomainModel.Extensions
{
    public static class EIEnumerableString
    {
        public static string ToSpacedString(this IEnumerable<string> stringList)
        {
            string concatenatedString = string.Empty;
            string[] stringArray = stringList.Distinct().ToArray();
            for (int i = 0; i < stringArray.Length; i++)
                concatenatedString += (i == 0 ? "" : " ") + stringArray[i];
            return concatenatedString;
        }
    }
}
