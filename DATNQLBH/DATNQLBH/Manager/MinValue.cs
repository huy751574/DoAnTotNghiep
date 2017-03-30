using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATNQLBH.Manager
{
    public class MinValue : ValidationAttribute, IClientValidatable
    {
        private readonly double _minValue;

        public MinValue(double minValue)
        {
            _minValue = minValue;
            ErrorMessage = "Nhập giá trị lớn hơn hoặc bằng " + _minValue;
        }

        public MinValue(int minValue)
        {
            _minValue = minValue;
            ErrorMessage = "Nhập giá trị lớn hơn hoặc bằng " + _minValue;
        }

        public override bool IsValid(object value)
        {
            return Convert.ToDouble(value) >= _minValue;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = ErrorMessage;
            rule.ValidationParameters.Add("min", _minValue);
            rule.ValidationParameters.Add("max", Double.MaxValue);
            rule.ValidationType = "range";
            yield return rule;
        }

    }
}