using System.Collections.Generic;
using PrinterSystem.Mediator;

namespace PrinterSystem.Models
{
    public class PrintQueue : Colleague
    {
        private Queue<Document> _queue = new Queue<Document>();

        public bool IsEmpty => _queue.Count == 0;

        public void EnqueueItem(Document document)
        {
            _queue.Enqueue(document);
            Mediator?.Notify(this, "Enqueued", document);
        }

        public Document DequeueItem()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }

        public int Count => _queue.Count;
    }
}