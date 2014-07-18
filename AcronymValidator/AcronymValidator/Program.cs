using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcronymValidator
{
    public class Utility
    {
        static void Main(string[] args) { }

        public static bool isValid(string acronym, List<string> productName)
        {
            var isValid = true;

            //new dictionary for names with a boolean as a flag to be set if the word has any chars used in the acronym
            var productNamesUsed = productName.ToDictionary(x => x, x => false);

            foreach (var currentChar in acronym)
            {
                var nextUnusedProductName = productNamesUsed
                    .Where(x => x.Value == false)
                    .FirstOrDefault();

                 var lastUsedProductName = productNamesUsed
                        .Where(x => x.Value == true)
                        .LastOrDefault();

                if (string.IsNullOrEmpty(nextUnusedProductName.Key))
                {
                    nextUnusedProductName = lastUsedProductName;
                }

                if (nextUnusedProductName.Key.Contains(currentChar))
                {
                    productNamesUsed[nextUnusedProductName.Key] = true;
                    continue;
                }
                else
                {
                    if (!string.IsNullOrEmpty(lastUsedProductName.Key))
                    {
                        if (lastUsedProductName.Key.Contains(currentChar))
                        {
                            continue;
                        }
                        else
                        {
                            isValid = false;
                            break;
                        }
                    }
                    else
                    {
                        isValid = false;
                        break;
                    } 
                }
            }

            if (productNamesUsed.Any(x => x.Value == false))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
