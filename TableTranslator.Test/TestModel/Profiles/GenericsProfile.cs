using System;
using System.Collections.Generic;
using TableTranslatorEx.Model;
using TableTranslatorEx.Model.Settings;

namespace TableTranslatorEx.Test.TestModel.Profiles
{
    public class GenericsProfile : TranslationProfile
    {
        protected override void Configure()
        {
            AddTranslation<Generics.OneGeneric
                <Generics.ThreeGenerics<int, DateTime, Generics.OneGeneric
                    <int>>>>(new TranslationSettings("ThreeDeep"))
                .AddColumnConfiguration(x => x.TData.JData.TData);

            AddTranslation<Generics.OneGeneric<int>>(new TranslationSettings("IntGeneric"))
                .AddColumnConfiguration(x => x.TData);

            AddTranslation<Generics.ThreeGenerics<int, DateTime, string>>(new TranslationSettings("IntDateTimeStringGeneric"))
                .AddColumnConfiguration(x => x.TData, new ColumnConfigurationSettings<int> { ColumnName = "T Data" })
                .AddColumnConfiguration(x => x.KData, new ColumnConfigurationSettings<DateTime> { ColumnName = "K Data" })
                .AddColumnConfiguration(x => x.JData, new ColumnConfigurationSettings<string> { ColumnName = "J Data" });

            AddTranslation<Generics.OneGeneric<Generics.OneGeneric<bool>>>(new TranslationSettings("NestedGeneric"))
                .AddColumnConfiguration(x => x.TData.TData);

            AddTranslation<Generics.OneGeneric<bool?>>(new TranslationSettings("NullableGeneric"))
                .AddColumnConfiguration(x => x.TData.Value);

            AddTranslation<List<bool>>()
                .AddColumnConfiguration(x => x.Count);
        }
    }
}