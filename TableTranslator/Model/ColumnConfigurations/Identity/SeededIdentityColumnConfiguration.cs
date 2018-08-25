using System;
using System.Data;
using TableTranslatorEx.Exceptions;

namespace TableTranslatorEx.Model.ColumnConfigurations.Identity
{
    /// <summary>
    /// Base class for identity column configurations where the DataTable provides the values for the column based off of provided seed settings
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SeededIdentityColumnConfiguration<T> : IdentityColumnConfiguration
    {
        private long IdentityIncrement { get; set; }
        private long IdentitySeed { get; set; }

        /// <summary>
        /// Creates an instance of SeededIdentityColumnConfiguration
        /// </summary>
        /// <param name="identitySeed">Seed for the identity column</param>
        /// <param name="identityIncrement">Increment for the identity column</param>
        protected SeededIdentityColumnConfiguration(T identitySeed, T identityIncrement) : base(typeof(T))
        {
            Init(identitySeed, identityIncrement);
        }

        /// <summary>
        /// Creates an instance of SeededIdentityColumnConfiguration
        /// </summary>
        /// <param name="identitySeed">Seed for the identity column</param>
        /// <param name="identityIncrement">Increment for the identity column</param>
        /// <param name="columnName">Column name for the identity column</param>
        protected SeededIdentityColumnConfiguration(T identityIncrement, T identitySeed, string columnName)
            : base(typeof(T), columnName)
        {
            Init(identitySeed, identityIncrement);
        }

        private void Init(T identitySeed, T identityIncrement)
        {
            Validate(identityIncrement);
            this.IdentitySeed = long.Parse(identitySeed.ToString());
            this.IdentityIncrement = long.Parse(identityIncrement.ToString());
            this.IsAutoGenerated = true;
        }

        protected internal override DataColumn GenerateIdentityColumn()
        {
            var column = base.GenerateIdentityColumn();
            column.AutoIncrement = true;
            column.AutoIncrementSeed = this.IdentitySeed;
            column.AutoIncrementStep = this.IdentityIncrement;
            column.DataType = typeof(T);
            return column;
        }

        protected internal sealed override object GetNextValue(object previousValue)
        {
            throw new NotSupportedException("The value for SeededIdentityColumnConfiguration is automatically added by the data table, so the GetNextValue() should not be called");
        }

        private static void Validate(T identityIncrement)
        {
            long tester;

            if (default(T) == null || !long.TryParse(default(T).ToString(), out tester))
            {
                throw new TableTranslatorConfigurationException("The type for a seeded identity column configuration must be able to be parsed to a long.");
            }

            if (long.Parse(identityIncrement.ToString()) == 0)
            {
                throw new TableTranslatorConfigurationException("IdentityIncrement cannot be zero.");
            }
        }
    }
}