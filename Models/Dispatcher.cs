using PrinterSystem.Mediator;

namespace PrinterSystem.Models
{
    public class Dispatcher : Colleague
    {
        public void AddDocumentToQueue(Document document)
        {
            Mediator?.Notify(this, "AddToQueueFromDispatcher", document);
        }

        public void CommandProcessQueue()
        {
            Mediator?.Notify(this, "ProcessQueue");
        }

        public void ResetDocument(Document document)
        {
            Mediator?.Notify(this, "ResetDocument", document);
        }
    }
}