using System;
using PrinterSystem.Models;

namespace PrinterSystem.States
{
    public class NewState : IDocumentState
    {
        public void Print(Document document)
        {
            Console.WriteLine($"[State: New] Документ '{document?.Title}' - сначала добавьте в очередь (AddToQueue)");
        }

        public void AddToQueue(Document document)
        {
            document?.Mediator?.Notify(document, "AddToQueue", document);
        }

        public void CompletePrinting(Document document)
        {
            Console.WriteLine($"[State: New] Документ '{document?.Title}' не находится в процессе печати");
        }

        public void FailPrinting(Document document)
        {
            Console.WriteLine($"[State: New] Документ '{document?.Title}' не находится в процессе печати");
        }

        public void Reset(Document document)
        {
            Console.WriteLine($"[State: New] Документ '{document?.Title}' уже в состоянии New");
        }
    }
}