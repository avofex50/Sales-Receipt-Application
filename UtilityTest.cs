using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxes;
using System.Collections.Generic;

namespace UnitTestForUtilityMethods
{
    [TestClass]
    public class UtilityTest
    {
        public Utility ut = new Utility();
        [TestMethod]
        public void InspectLineInputTest()
        {
            string passTest = "1 Imported chocolate at 40.60"; //correct input format.
            string failTest = "Imported food at 40.60"; //not specifying quantity will fail.

            Assert.IsTrue(ut.InspectLineInput(passTest));  //this will pass

            Assert.IsFalse(ut.InspectLineInput(failTest));//this will fail
        }

        [TestMethod]
        public void RemoveMultipleSpaceInStringTest()
        {
            string multileWhiteSpace = "1            Medical pills   60.23";
            string expected = "1 Medical pills 60.23";

            Assert.AreEqual(expected, ut.RemoveMultipleSpaceInString(multileWhiteSpace));
        }

        [TestMethod]
        public void GetProductCategoryTest()
        {
            string importedEssentails = "1 Imported box of chocolates at 45.12";
            string general = "1 Four pair of tyres at 200.99";
            string importedGeneral = "1 Imported car tyre at 50.45";
            string essentails = "1 candy bar at 4.99";

            Assert.AreEqual(ProductCategory.ImportedEssential, ut.GetProductCategory(importedEssentails));
            Assert.AreEqual(ProductCategory.General, ut.GetProductCategory(general));
            Assert.AreEqual(ProductCategory.ImportedGeneral, ut.GetProductCategory(importedGeneral));
            Assert.AreEqual(ProductCategory.Essentials, ut.GetProductCategory(essentails));
        }

        [TestMethod]
        public void ProcessItemLineIntoProductObjectTest()
        {
            string inputLine = "1 Imported food at 56.78";
            ProductObject po = ut.ProcessItemLineIntoProductObject(inputLine);

            Assert.AreEqual(1, po.Quantity);
            StringAssert.Equals("Imported food", po.ProductDescription);
            Assert.AreEqual(56.78m, po.Price);
            Assert.IsNull(po.OutputFormat);
            Assert.AreEqual(0.00m, po.Tax);
            Assert.AreEqual(ProductCategory.ImportedEssential, po.Category);
            Assert.AreEqual(0.00m, po.ImportedTax);
           
        }

        [TestMethod]
        public void CalculateTaxandPrintRecieptTest()
        {
            List<string> sanitizedLineInputs = new List<string>()
            {
                "1 Imported bottle of perfume at 27.99",
                "1 Bottle of perfume at 18.99",
                "1 Packet of headache pills at 9.75",
                "1 Imported box of chocolates at 11.25",
                "1 Imported box of chocolates at 11.25"
            };

            string expectedResult = 
                "Imported bottle of perfume: 32.19\n" +
                "Bottle of perfume: 20.89\n" +
                "Packet of headache pills: 9.75\n" +
                "Imported box of chocolates: 23.70 (2 @ 11.85)\n" +
                "Sales Taxes: 7.30\n" +
                "Total: 86.53";

            StringAssert.Equals(expectedResult, ut.CalculateTaxandPrintReciept(sanitizedLineInputs));
        }
    }
}
