using Sitecore.Configuration;
using Sitecore.Data.Validators;
using Sitecore.Diagnostics;
using Sitecore.Modules.EmailCampaign.Validators;
using System;
using System.Runtime.Serialization;

namespace Sitecore.Support.Modules.EmailCampaign.Core.FieldValidators
{
    [Serializable]
    public class UrlFieldValidator : StandardValidator
    {
        private RegexValidator _urlRegexValidator;

        public override string Name => "UrlFieldValidator";

        private RegexValidator UrlRegexValidator => _urlRegexValidator ?? (_urlRegexValidator = (Factory.CreateObject("urlRegexValidator", assert: true) as RegexValidator));

        public UrlFieldValidator(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public UrlFieldValidator()
        {
        }

        protected override ValidatorResult Evaluate()
        {
            string controlValidationValue = base.ControlValidationValue;
            if (string.IsNullOrEmpty(controlValidationValue) || UrlRegexValidator.IsValid(controlValidationValue.Trim()))
            {
                return ValidatorResult.Valid;
            }
            base.Text = GetText("'{0}' is not a valid url.", controlValidationValue.Trim());
            return GetFailedResult(ValidatorResult.CriticalError);
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return GetFailedResult(ValidatorResult.CriticalError);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Assert.ArgumentNotNull(info, "info");
            #region commented the line since the same key-value is added in the base.GetObjectData method
            //info.AddValue("Name", this.Name);
            #endregion
            base.GetObjectData(info, context);
        }
    }
}
