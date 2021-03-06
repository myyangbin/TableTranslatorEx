using System;
using TableTranslatorEx.Model;
using TableTranslatorEx.Model.ColumnConfigurations.Identity;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.Test.TestModel.Profiles
{
    public class WrongTypeIdentityProfile : TranslationProfile
    {
        protected override void Configure()
        {
            AddTranslation<int>(new TranslationSettings(new WrongTypeIdentity(), "WrongTypeIdentity"))
                .AddColumnConfiguration(x => x * 10, new ColumnConfigurationSettings<int> { ColumnName = "Times10" });
        }

        public class WrongTypeIdentity : ProviderIdentityColumnConfiguration
        {
            public WrongTypeIdentity() : base(typeof(decimal))
            {
            }

            protected override object GetNextValue(object previousValue)
            {
                return Guid.NewGuid();
            }
        }
    }
}