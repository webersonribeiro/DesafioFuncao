using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace FI.AtividadeEntrevista.DML.Validations
{
    public class CPFCustomValidator : ValidationAttribute, IClientValidatable
    {
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            throw new NotImplementedException();    
        }

        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return false;

            return ValidaCPF(value.ToString());
        }

        private bool ValidaCPF(string value)
        {
            value = value.Replace(".", "").Replace("-", "").ToString();

            var InvalidCPF = new string[] { "00000000000",
                                            "11111111111",
                                            "22222222222",
                                            "33333333333",
                                            "44444444444",
                                            "55555555555",
                                            "66666666666",
                                            "77777777777",
                                            "88888888888",
                                            "99999999999"
            };

            if (InvalidCPF.Contains(value))
                return false;

            var isValid = false;

            if (value.Length == 11)
            {
                var firstMultipliers = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var secondMultipliers = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                var chunk = value.Substring(0, 9);
                var sum = 0;
                for (var i = 0; i < 9; i++)
                {
                    sum += int.Parse(chunk[i].ToString()) * firstMultipliers[i];
                }

                var rest = sum % 11;
                rest = rest < 2 ? 0 : 11 - rest;

                var verifierDigits = rest.ToString();
                chunk += verifierDigits;

                sum = 0;
                for (var i = 0; i < 10; i++)
                {
                    sum += int.Parse(chunk[i].ToString()) * secondMultipliers[i];
                }

                rest = sum % 11;
                rest = rest < 2 ? 0 : 11 - rest;

                verifierDigits += rest.ToString();
                isValid = value.EndsWith(verifierDigits);
            }

            return isValid;
        }
    }
}