using Ganss.XSS;
using System.ComponentModel.DataAnnotations;

namespace WebTravelTest.Entities.CustomValidations
{
    public class AntiXssModel : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try {
                string valorString = string.Empty;
                if (value != null)
                {
                    valorString = value.ToString();
                }
                var sanitiser = new HtmlSanitizer();
                var sanitised = sanitiser.Sanitize(valorString);
                if (valorString != sanitised)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch {
                return true;
            }
        }
    }
}