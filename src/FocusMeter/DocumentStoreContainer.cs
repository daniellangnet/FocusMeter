using System.Diagnostics;
using System.Net;
using Raven.Client.Embedded;

namespace FocusMeter
{
    public class DocumentStoreContainer
    {
        public static EmbeddableDocumentStore DocumentStore { get; set; }

        public static bool CanShowDatabase { get; private set; }

        public static void Initialize()
        {
            DocumentStore = new EmbeddableDocumentStore
            {
                DataDirectory = "Data",
                UseEmbeddedHttpServer = true
            };

            try
            {
                DocumentStore.Initialize();
                CanShowDatabase = true;
            }
            catch (HttpListenerException)
            {
                DocumentStore = new EmbeddableDocumentStore
                {
                    DataDirectory = "Data"
                };
                DocumentStore.Initialize();
                CanShowDatabase = false;
            }
        }

        public static void ShowDatabase()
        {
            if (CanShowDatabase)
            {
                Process.Start(DocumentStore.HttpServer.Configuration.ServerUrl);
            }
        }
    }
}