using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SalesTaxes
{
    public class Utility
    {
        public Utility()
        {

        }
        /// <summary>
        /// This function will inspect input strings if it meets these conditions:
        /// input line must be at least 3 words e.g 1 chocolate at 40,
        /// first word of the line must be an integer to designate the quantity,
        /// last word of the line must be a decimal which designate the price.
        /// If all these are met, it will return true otherwise, false.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public bool InspectLineInput(string inputString)
        {
            string[] breakInputIntoLines = inputString.Split(' ');
            if (breakInputIntoLines.Length < 3 || !int.TryParse(breakInputIntoLines[0], out int iValue) || !double.TryParse(breakInputIntoLines[breakInputIntoLines.Length - 1], out double dValue))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This will remove extra white spaces between input lines
        /// </summary>
        /// <param name="stInput"></param>
        /// <returns></returns>
        public string RemoveMultipleSpaceInString(string stInput)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            stInput = regex.Replace(stInput, " ");
            return stInput;
        }

        /// <summary>
        /// This function will check if input belongs to food, medical, book, general or imported category
        /// and return ProductCategory enum which specifies the correct category. While this idea of 
        /// creating a list might not be the best way but that is all I could think at this time
        /// so as to be able to submit on time.
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        public ProductCategory GetProductCategory(string productDetails)
        {
            List<string> foodCategory = new List<string>
            {
            "food",
            "candy",
            "Chocolate",
            "chocolates",
            "groceries",
            "grocery",
            "vegetable",
            "vegetables",
            "meat",
            "sea food",
            "fish",
            "Food"
            };
            List<string> medicalCategory = new List<string>
            {
                "medical",
                "medicine",
                "bandage",
                "first aid",
                "surgery",
                "pills",
                "pill",
                "Medical"
            };
            List<string> bookCategory = new List<string>
            {
                "Book",
                "text book",
                "text-book",
                "text books",
                "books",
                "book",
                "Books"
            };

            //group items into category and return ProductCategory enum value to calculate tax
            //to do: Ignore case while comapring strings****
            ProductCategory pc = new ProductCategory();
            if((foodCategory.Any(f=>productDetails.Contains(f))) || (medicalCategory.Any(m=>productDetails.Contains(m))) || (bookCategory.Any(b=>productDetails.Contains(b))))
            {
                if ((productDetails.Contains("imported") || productDetails.Contains("Imported")))
                {
                    pc = ProductCategory.ImportedEssential;
                }
                else
                {
                    pc = ProductCategory.Essentials;
                }
            
            }
            if ((!foodCategory.Any(f => productDetails.Contains(f))) && (!medicalCategory.Any(m => productDetails.Contains(m))) && (!bookCategory.Any(b => productDetails.Contains(b))))
            {
                if ((productDetails.Contains("imported") || productDetails.Contains("Imported")))
                {
                    pc = ProductCategory.ImportedGeneral;
                }
                else
                {
                    pc = ProductCategory.General;
                }
            }
            return pc;
        }

        /// <summary>
        /// This will calculate taxes based on input category 
        /// and round it up to nearest 5cent
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public decimal CalculateTaxandRounToFiveCent(decimal price)
        {
            //to do: round to 2 decimal places
            decimal tax = 0.10M * price;// Math.Round(0.10M * price, 2);
            return Math.Ceiling(tax * 20) / 20; 
        }

        /// <summary>
        /// This will calculate import duty of 5%
        /// for all imported items to generate 
        /// updated price.
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public decimal CalculateFivePercentImportDuty(decimal price)
        {
            decimal importTax = 0.05M * price;
            return Math.Ceiling(importTax * 20) / 20;
        }

        /// <summary>
        /// This will split each input lines and 
        /// process it to ProductObject
        /// </summary>
        /// <param name="sanitizedInputLine"></param>
        /// <returns></returns>
        public ProductObject ProcessItemLineIntoProductObject(string sanitizedInputLine)
        {
            //split input line on white space into string array
            string[] sst = sanitizedInputLine.Split(' '); 
            ProductObject po = new ProductObject();

            //get first array element and convert to integer which should be quantity
            //and create ProductObject po.
            po.Quantity = Convert.ToInt32(sst[0]);

            //convert last array element to decimal which should be price,
            //check if it belongs to imported category and call CalculateImportDutyandNewPrice()
            //to get the new price with 5% added
            decimal inputPrice = Convert.ToDecimal(sst[sst.Length - 1]) ;
            po.Price = inputPrice;

            //combine remaining array string together to form
            //product-description after removing the quantity on 0 index
            //and price on last index. Also, remove at the second to last
            //index if it contains one.
            int checkForAt = (sst[sst.Length - 2] == "at") ? sst.Length - 2 : sst.Length - 1;
            for(int i = 1; i < checkForAt; i++)
            {
                po.ProductDescription = $"{po.ProductDescription} {sst[i]}";

            }
            po.Category = GetProductCategory(po.ProductDescription);

            //return new ProductObject with the above details
            return po;
        }

        /// <summary>
        /// This is where tax calculation and logics for printing 
        /// receipt happens. It implements most of the defined methods
        /// above to get the final calculation and generate output 
        /// format to the UI.
        /// </summary>
        /// <param name="sanitizedInputList"></param>
        /// <returns></returns>
        public string CalculateTaxandPrintReciept(List<string> sanitizedInputList)
        {
            //declare ProductObject list to hold processed input and generate output format
            //implement string builder to print receipt
            List<ProductObject> poList = new List<ProductObject>();
            StringBuilder printReceipt = new StringBuilder();

            //loop through sanitized input list
            foreach(var item in sanitizedInputList)
            {
                //Implement ProcessItemLineIntoProductObject to process each input line into ProductObject
                //Implement GetProductCategory() to get input category and use the info to calculate tax and import taxes if applicable
                ProductObject poSingle = ProcessItemLineIntoProductObject(item);
                if(GetProductCategory(poSingle.ProductDescription) == ProductCategory.ImportedEssential)
                {
                    poSingle.ImportedTax = CalculateFivePercentImportDuty(poSingle.Price);
                    poSingle.Price += poSingle.ImportedTax;
                }
                if(GetProductCategory(poSingle.ProductDescription) == ProductCategory.General)
                {
                    poSingle.Tax = CalculateTaxandRounToFiveCent(poSingle.Price);
                    poSingle.Price += poSingle.Tax;
                }
                if(GetProductCategory(poSingle.ProductDescription) == ProductCategory.ImportedGeneral)
                {
                    poSingle.ImportedTax = CalculateFivePercentImportDuty(poSingle.Price);
                    poSingle.Tax = CalculateTaxandRounToFiveCent(poSingle.Price);
                    poSingle.Price += poSingle.ImportedTax + poSingle.Tax;
                }

                //check if product is already processed and among ProductObject list, if yes, update 
                //info and output format
                var existing = poList.FirstOrDefault(l => l.ProductDescription == poSingle.ProductDescription);
                if(existing != null)
                {
                    existing.Quantity += poSingle.Quantity;
                    existing.Price = poSingle.Price * existing.Quantity;
                    existing.Tax += poSingle.Tax + poSingle.ImportedTax;
                    existing.OutputFormat = $"{existing.ProductDescription}: {existing.Quantity * poSingle.Price} ({existing.Quantity} @ {poSingle.Price})";
                    
                }
                else
                {
                    //if processed object does not already exist on output list, then add to list
                    //and update output format
                    poSingle.OutputFormat = $"{poSingle.ProductDescription}: {poSingle.Price}";
                    poSingle.Tax += poSingle.ImportedTax;
                    poList.Add(poSingle);
                }
            }
            //declare decimal variable to hold total amount and sales taxes
            decimal total = 0.00M;
            decimal salesTaxes = 0.00M;

            //loop through ProductObject list and append each line to the above string builder
            //calculate total sales and taxes during this process as well
            foreach(ProductObject po in poList)
            {
                printReceipt.Append(po.OutputFormat);
                printReceipt.AppendLine();
                salesTaxes += po.Tax;
                total += po.Price;
            }

            //finally append sales taxes and total sales to the end of string builder
            //and return the string to UI.
            printReceipt.Append($"Sales Taxes: {salesTaxes}");
            printReceipt.AppendLine();
            printReceipt.Append($"Total: {total}");
            return printReceipt.ToString();
        }
        
    }

    public enum ProductCategory
    {
        Essentials = 1, //foods, medicals, books
        ImportedEssential = 2, //imported books, foods and medicals
        ImportedGeneral = 3, //imported generals, attracts 5% and 10% taxes
        General = 4 //non essentials, attracts 10% taxes
    }

    public class ProductObject
    {
        public string ProductDescription { get; set; }
        public ProductCategory? Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } = 0.00M;
        public decimal Tax { get; set; } = 0.00M;
        public string OutputFormat { get; set; }
        public decimal ImportedTax { get; set; } = 0.00M;
    }
}
