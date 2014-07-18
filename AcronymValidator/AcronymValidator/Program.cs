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
                        nextUnusedProductName.HasBeenChecked = true;
                        nextUnusedProductName.Name = PopupUntilCharValueFound(currentChar, nextUnusedProductName.Name);
                        continue;
                    }
                    else
                    {
                        if (lastUsedProductName != null && lastUsedProductName.Name.Contains(currentChar))
                        {
                            lastUsedProductName.Name = PopupUntilCharValueFound(currentChar, lastUsedProductName.Name);
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
            var stackToPop = new Stack<char>(stackToCompare.Reverse());

            foreach (var charToPop in stackToCompare)
            {
                stackToPop.Pop();                
                if (charToPop == currentChar) { break; }
            }
            return stackToPop;
        }
    }
}
