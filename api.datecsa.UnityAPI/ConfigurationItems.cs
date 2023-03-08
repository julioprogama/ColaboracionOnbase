using System;
using System.Collections.Generic;

using Hyland.Unity;

namespace api.datecsa.UnityAPI
{
	public class ConfigurationItems
	{
		Hyland.Unity.Application app = null;

		public List<string> GetDocumentTypes()
		{
			try
			{
				List<string> docTypes = new List<string>();
                // Loop through all of the Document Types the current user can access. DocumentTypes are accessed through the Core. The Core is accessed from the Application object.
                foreach (DocumentType docType in app.Core.DocumentTypes)
                {
                    // Add the document type names to the generic list of strings.
                    docTypes.Add(docType.Name);
                }
				return docTypes;
			}
			catch (SessionNotFoundException ex)
			{
				app.Diagnostics.Write(ex);
				throw new Exception("The Unity API session could not be found, please reconnect.", ex);
			}
			catch (UnityAPIException ex)
			{
				app.Diagnostics.Write(ex);
				throw new Exception("There was a Unity API exception.", ex);
			}
			catch (Exception ex)
			{
				app.Diagnostics.Write(ex);
				throw new Exception("There was an unknown exception.", ex);
			}
		}

		public ConfigurationItems(Hyland.Unity.Application app)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
			}
            this.app = app;
		}
	}
}
