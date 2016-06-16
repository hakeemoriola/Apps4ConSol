using System;
using System.Configuration;
using System.Web.Configuration;

namespace ConSolHWeb.Data
{
    public static class DataService
    {
        private static bool _isInitialized = false;
        private static DataProvider _provider;
        private static CPNetFrameWorkDataProvidersSection _providersSection;

        public static DataProvider Provider
        {
            get
            {
                Initialize();
                return _provider;
            }
        }

        private static void Initialize()
        {
            if (!_isInitialized)
            {
                _providersSection = (ConfigurationManager.GetSection("CPNetFrameWorkDataProviders")) as CPNetFrameWorkDataProvidersSection;
                if (_providersSection == null)
                {
                    throw new Exception(Messages.DataProviderConfigSectionNotFound);
                }

                _provider = ProvidersHelper.InstantiateProvider(_providersSection.DataProviders[_providersSection.DataProviderName],
                    typeof(DataProvider)) as DataProvider;

                if (_provider == null)
                {
                    throw new Exception("DataProvider's default provider could not be instantiated");
                }
            }
        }
    }
}