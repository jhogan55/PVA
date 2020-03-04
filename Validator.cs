using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Validator_Class
{
    public static class Validator
    {
        // make sure user entered data into text boxes
        public static bool TextEntered(TextBox t, string name)
        {
            bool result = true;
            if (t.Text == "")
            {
                MessageBox.Show(name + " has not been entered and is a required field.", "Required Field: Please Enter a Value");
                t.SelectAll();
                t.Focus();
                result = false;
            }
            return result;
        }
        // checks if relevant text boxes are valid decimals
        public static bool IsDecimal(TextBox t, string name)
        {
            bool result = true;
            decimal value;
            if (!Decimal.TryParse(t.Text, out value))
            {
                MessageBox.Show(name + " should be a number. Please enter a valid number.", "Invalid Entry: Decimal Required");
                t.SelectAll(); // select all text for replacement
                t.Focus();
                result = false;
            }
            return result;
        }

        // checks if relevant text boxes are valid integers
        public static bool IsInt(TextBox t, string name)
        {
            bool result = true;
            int value;
            if (!Int32.TryParse(t.Text, out value))
            {
                MessageBox.Show(name + " should be a number. Please enter a valid number.", "Invalid Entry: Decimal Required");
                t.SelectAll(); // select all text for replacement
                t.Focus();
                result = false;
            }
            return result;
        }

        // checks if a valid amount was entered 
        public static bool WithinRange(TextBox t, int MIN, int MAX, string name)
        {
            bool result = true;
            decimal value = Convert.ToDecimal(t.Text);
            if (value < MIN || value > MAX)
            {
                MessageBox.Show(name + " must be higher than " + MIN + " and below " + MAX + ". Please enter a number within that range.", "Invalid Entry: Out of Range");
                t.SelectAll(); // select all text for replacement
                t.Focus();
                result = false;
            }
            return result;
        }

        public static bool ComboUsed (ComboBox c, string name)
        {
            bool result = true;
            if (c.SelectedIndex == -1)
            {
                MessageBox.Show(name + " is a required field, please select an option from the box.", "Required Field Missing");
                c.Focus();
                result = false;
            }
            return result; 
        }
    }
}
