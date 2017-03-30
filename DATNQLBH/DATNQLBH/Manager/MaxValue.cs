using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATNQLBH.Manager
{
    public class MaxValue : ValidationAttribute, IClientValidatable
    {
        private readonly double _maxValue;

        public MaxValue(int maxValue)
        {
            _maxValue = maxValue;
            ErrorMessage = "Nhập giá trị nhỏ hơn hoặc bằng " + maxValue;
        }
        public MaxValue(double maxValue)
        {
            _maxValue = maxValue;
            ErrorMessage = "Nhập giá trị nhỏ hơn hoặc bằng " + maxValue;
        }

        public override bool IsValid(object value)
        {
            return (int)value <= _maxValue;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = ErrorMessage;
            rule.ValidationParameters.Add("min", Double.MinValue);
            rule.ValidationParameters.Add("max", _maxValue);
            rule.ValidationType = "range";
            yield return rule;
        }
    }
}