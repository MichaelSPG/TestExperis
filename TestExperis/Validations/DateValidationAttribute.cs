using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestExperis.Validations
{
    public class TestDateValidationAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "La fecha de la entrevista debe ser mayor a la fecha actual";
        private string _basePropertyName;
        public TestDateValidationAttribute(string basePropertyName) : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }
        public override bool IsValid(object value)
        {
            DateTime dateTime = (DateTime)value;
            return dateTime >= DateTime.Now;
        }
    }   
}