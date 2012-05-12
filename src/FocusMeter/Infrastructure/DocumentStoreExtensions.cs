using FocusMeter.Model;
using Raven.Client;

namespace FocusMeter.Infrastructure
{
    public static class DocumentStoreExtensions
    {
        public static Configuration LoadConfiguration(this IDocumentStore documentStore)
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Load<Configuration>(Constants.ConfigurationDocumentKey);
            }
        }

        public static void SaveOrUpdateConfiguration(this IDocumentStore documentStore, Configuration configuration)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(configuration, Constants.ConfigurationDocumentKey);
                session.SaveChanges();
            }
        }
    }
}