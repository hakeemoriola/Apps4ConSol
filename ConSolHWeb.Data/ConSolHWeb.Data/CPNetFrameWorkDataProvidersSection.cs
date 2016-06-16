using System.Configuration;

namespace ConSolHWeb.Data
{
    /// <summary>
    /// Summary description for CPNetFrameWorkDataProvidersSection
    /// </summary>
    ///

    public class CPNetFrameWorkDataProvidersSection : ConfigurationSection
    {
        [ConfigurationProperty("dataProviderName", IsRequired = true)]
        public string DataProviderName
        {
            get { return (string)this["dataProviderName"]; }
            set { this["dataProviderName"] = value; }
        }

        [ConfigurationProperty("DataProviders")]
        [ConfigurationValidatorAttribute(typeof(ProviderSettingsValidation))]
        public ProviderSettingsCollection DataProviders
        {
            get { return (ProviderSettingsCollection)this["DataProviders"]; }
        }
    }
}