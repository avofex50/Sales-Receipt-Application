using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesTaxes
{
    public partial class SalesTax : Form
    {
        public const string ErrorMessage = "Enter items in this format: Quantity Product-Details Price e.g 1 Vegetable Salad 2.90";
        
        readonly Utility uti;
        public SalesTax()
        {
            InitializeComponent();
            uti = new Utility();
        }

        private void btnCalculateTax_Click(object sender, EventArgs e)
        {
            //check if input value was null or empty
            if(string.IsNullOrEmpty(rtbInputList.Text))
            {
                MessageBox.Show(ErrorMessage);
                return;
            }

            //inspect if input order came in as expected and remove extra spaces between each line
            string tempString;
            List<string> InspectedInputLines = new List<string>();
            string[] tempHolder = rtbInputList.Text.Split('\n');
            for (int i = 0; i < tempHolder.Length; i++)
            {
                tempString = uti.RemoveMultipleSpaceInString(tempHolder[i]).Trim();
                if (tempString == "")
                {
                    continue;
                }
                if (!uti.InspectLineInput(tempString))
                {
                    MessageBox.Show(ErrorMessage);
                    return;
                }
                else
                {
                    InspectedInputLines.Add(tempString);
                }
            }
            Utility utiObject = new Utility();
            rtbOutputList.Text = utiObject.CalculateTaxandPrintReciept(InspectedInputLines);

        }
    }
}
