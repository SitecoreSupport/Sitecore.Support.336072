using Sitecore.Configuration;
using Sitecore.Data.Validators;
using Sitecore.Diagnostics;
using Sitecore.Modules.EmailCampaign.Validators;
using System;
using System.Runtime.Serialization;

namespace Sitecore.Support.Modules.EmailCampaign.Core.FieldValidators
{
    [Serializable]
    public class EmailFieldValidator : StandardValidator
    {
        private RegexValidator _emailRegexValidator;

        public EmailFieldValidator()
        {
        }

        public EmailFieldValidator(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected override ValidatorResult Evaluate()
        {
            string controlValidationValue = base.ControlValidationValue;
            if (string.IsNullOrEmpty(controlValidationValue) || this.EmailRegexValidator.IsValid(controlValidationValue.Trim()))
            {
                return ValidatorResult.Valid;
            }
            string[] arguments = new string[] { controlValidationValue.Trim() };
            base.Text = this.GetText("'{0}' is not a valid email address.", arguments);
            return base.GetFailedResult(ValidatorResult.CriticalError);
        }

        protected override ValidatorResult GetMaxValidatorResult() =>
            base.GetFailedResult(ValidatorResult.CriticalError);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Assert.ArgumentNotNull(info, "info");
            #region commented the line since the same key-value is added in the base.GetObjectData method
            //info.AddValue("Name", this.Name);
            #endregion
            base.GetObjectData(info, context);
        }


        private RegexValidator EmailRegexValidator =>
            (this._emailRegexValidator ?? (this._emailRegexValidator = Factory.CreateObject("emailRegexValidator", true) as RegexValidator));

        public override string Name =>
            "EmailFieldValidator";
    }
}
