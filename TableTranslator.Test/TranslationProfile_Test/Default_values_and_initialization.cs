﻿using NUnit.Framework;
using TableTranslatorEx.Test.TestModel.Profiles;

namespace TableTranslatorEx.Test.TranslationProfile_Test
{
    [TestFixture]
    public class Default_values_and_initialization : InitializedTranslatorTestBase
    {
        [Test]
        public void Profile_name_is_the_name_of_the_class_by_default()
        {
            var profile = new DefaultNameProfile();
            Assert.AreEqual(typeof(DefaultNameProfile).Name, profile.ProfileName);
        }

        [Test]
        public void Configure_is_called_during_initialization()
        {
            var spyProfile = new SpyProfile();
            Translator.AddProfile(spyProfile);
            Translator.ApplyUpdates();
            Assert.IsTrue(spyProfile.ConfigureWasCalled);
        }
    }
}
