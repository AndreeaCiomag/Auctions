using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Auctions.Rules
{
    public class ValidationRules
    {
        public ValidationRules()
        {
            void PassLenValid(string pass) { }
            void PassMajValid(string pass) { }
            void PassSymbValid(string pass) { }
            void PassNrValid(string pass) { }
            void EmailValid(string email) { }
        }
        
        public void PassLenValid(string pass)
        {
            bool valid = true;
            if (pass.Length < 8) valid = false; 
        }

        public void PassMajValid(string pass)
        {
            bool valid = pass.Any(c => char.IsUpper(c));
        }

        public void PassSymbValid(string pass)
        {
            bool valid = pass.Any(c => char.IsSymbol(c));
        }
        public void PassNrValid(string pass)
        {
            bool valid = pass.Any(c => char.IsNumber(c));
        }
        public void EmailValid(string email)
        {
            bool valid = true;
            Regex emailRegex = new Regex(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
            if (!emailRegex.IsMatch(email)) valid = false;
        }
    }
}
