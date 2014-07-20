using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcronymValidator
{
    public class ProductName
    {
        public Stack<char> Name { get; set; }
        public bool HasBeenChecked { get; set; }
    }

    public class Utility
    {
        static void Main(string[] args) { }

        public static bool isValid(string acronym, List<string> productName)
        {
            var isValid = true;
            var productNamesUsed = new List<ProductName>();

            productName.ForEach(x => 
                productNamesUsed.Add(new ProductName() 
                { 
                    Name = new Stack<char>(x.ToCharArray().Reverse()), 
                    HasBeenChecked = false
                }));

            if (productName.Count > 0)
            {
                for (int i = 0; i < acronym.Length; i++)
                {
                    var nextUnusedProductName = productNamesUsed.Where(x => !x.HasBeenChecked).FirstOrDefault();
                    var lastUsedProductName = productNamesUsed.Where(x => x.HasBeenChecked).LastOrDefault();
                    nextUnusedProductName = nextUnusedProductName ?? lastUsedProductName;
                                       
                    if (WillBeAbletoSatisfyRequiredValues(acronym.Substring(i, acronym.Length - i), productNamesUsed.Where(x => !x.HasBeenChecked)) 
                        && nextUnusedProductName.Name.Contains(acronym[i]))
                    {
                        nextUnusedProductName.HasBeenChecked = true;
                        nextUnusedProductName.Name = PopUntilCharValueFound(acronym[i], nextUnusedProductName.Name);
                        continue;
                    }
                    else
                    {
                        if (lastUsedProductName != null && lastUsedProductName.Name.Contains(acronym[i]))
                        {
                            lastUsedProductName.Name = PopUntilCharValueFound(acronym[i], lastUsedProductName.Name);
                            continue;
                        }
                        else
                        {
                            isValid = false;
                            break;
                        }                     
                    }
                }
            }

            if (productNamesUsed.Count == 0 || productNamesUsed.Any(x => !x.HasBeenChecked))
            {
                isValid = false;
            }

            return isValid;
        }

        private static Stack<char> PopUntilCharValueFound(char currentChar, Stack<char> stackToCompare)
        {
            var stackToPop = new Stack<char>(stackToCompare.Reverse());
            
            foreach (var charToPop in stackToCompare)
            {
                stackToPop.Pop(); 
                if (charToPop == currentChar) { break; }
            }

            return stackToPop;
        }

        private static bool WillBeAbletoSatisfyRequiredValues(string remainderOfAcronym, IEnumerable<ProductName> remainingNames)
        {
            var remainingProductName = string.Join("", remainingNames.Select(x => new String(x.Name.ToArray())));
            var currentCharBeingChecked = remainderOfAcronym.First();

            var numberOfCharNeeded = remainderOfAcronym.Where(x => x == currentCharBeingChecked).Count();
            var numberOfMatchingChars = remainingProductName.Where(x => x == currentCharBeingChecked).Count();
            return numberOfMatchingChars >= numberOfCharNeeded;
        }
    }
}
