using StoreakApiService.Core.Helper;
using StoreakApiService.Core.Responses;

namespace Storeak.Demo.Panel.Service
{
    public class ResourceMessages
    {
        public IResponsesManager _responsesManager;
        public SessionHandler _sessionHandler;
        public ResourceMessages(IResponsesManager responsesManager, SessionHandler sessionHandler)
        {
            _responsesManager = responsesManager;
            _sessionHandler = sessionHandler;
        }
        public string InternalServerError { get { return getMessage("InternalServerError"); } }
        public string NewRecord { get { return getMessage("NewRecord"); } }
        public string Next { get { return getMessage("Next"); } }
        public string Previous { get { return getMessage("Previous"); } }
        public string Home { get { return getMessage("Home"); } }
        public string Create { get { return getMessage("Create"); } }
        public string Action { get { return getMessage("Action"); } }
        public string Actions { get { return getMessage("Actions"); } }
        public string Status { get { return getMessage("Status"); } }
        public string CreatedBy { get { return getMessage("CreatedBy"); } }
        public string Close { get { return getMessage("Close"); } }
        public string Enable { get { return getMessage("Enable"); } }
        public string Disable { get { return getMessage("Disable"); } }
        public string Update { get { return getMessage("Update"); } }
        public string Delete { get { return getMessage("Delete"); } }
        public string Classes { get { return getMessage("Classes"); } }
        public string SignOut { get { return getMessage("SignOut"); } }
        public string Name { get { return getMessage("Name"); } }
        public string IdRequired { get { return getMessage("IdRequired"); } }
        public string NameRequired { get { return getMessage("NameRequired"); } }
        public string StoreTypeCreatedSuccessfully { get { return getMessage("StoreTypeCreatedSuccessfully"); } }
        public string CreateStoreTypeModalTitle { get { return getMessage("CreateStoreTypeModalTitle"); } }
        public string UpdateStoreTypeModalTitle { get { return getMessage("UpdateStoreTypeModalTitle"); } }
        public string StoreType { get { return getMessage("StoreType"); } }
        public string StoreTypes { get { return getMessage("StoreTypes"); } }
        

        public string DeleteConfirmationMessage(string Paramter1, string Paramter2)
        {
            return string.Format(getMessage("DeleteConfirmationMessage"), Paramter1, Paramter2);
        }

        public string getMessage(string name)
        {
            string language = _sessionHandler.Language;
            return _responsesManager.GetResponceMessage(name, language);
        }
    }
}
