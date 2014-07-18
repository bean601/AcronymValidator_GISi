using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcronymValidator
{
    public class ProductName
    {
        public int Index { get; set; }
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
            var i = 0;

            productName.ForEach(x =>
                productNamesUsed.Add(new ProductName()
                {
                    Index = i++,
                    Name = new Stack<char>(x.ToCharArray().Reverse()),
                    HasBeenChecked = false
                }));

            if (productName.Count > 0)
            {
                foreach (var currentChar in acronym)
                {
                    var nextUnusedProductName = productNamesUsed.Where(x => !x.HasBeenChecked).FirstOrDefault();
                    var lastUsedProductName = productNamesUsed.Where(x => x.HasBeenChecked).LastOrDefault();

                    if (nextUnusedProductName == null)
                    {
                        //this is very unclear, fix this
                        nextUnusedProductName = lastUsedProductName;
                    }

                    if (nextUnusedProductName.Name.Contains(currentChar))
                    {
                        productNamesUsed[nextUnusedProductName.Index].HasBeenChecked = true;
                        productNamesUsed[nextUnusedProductName.Index].Name = PopupUntilCharValueFound(currentChar, nextUnusedProductName.Name);
                        continue;
                    }
                    else
                    {
                        if (lastUsedProductName != null && lastUsedProductName.Name.Contains(currentChar))
                        {
                            productNamesUsed[lastUsedProductName.Index].Name = PopupUntilCharValueFound(currentChar, lastUsedProductName.Name);
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

        private static Stack<char> PopupUntilCharValueFound(char currentChar, Stack<char> stackToCompare)
        {
            var stackToPop = new Stack<char>(new Stack<char>(stackToCompare));

            foreach (var charToPop in stackToCompare)
            {
                if (charToPop != currentChar)
                {
                    stackToPop.Pop();
                }
                else
                {
                    stackToPop.Pop();
                    break;
                }
            }
            return stackToPop;
        }
    }
}
