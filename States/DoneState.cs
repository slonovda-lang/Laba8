using System;
using PrinterSystem.Models;

namespace PrinterSystem.States
{
    public class DoneState : IDocumentState
    {
        public void Print(Document document)
        {
            Console.WriteLine($"[State: Done] Документ '{document?.Title}' уже напечатан");
        }

        public void AddToQueue(Document document)
        {
            Console.WriteLine($"[State: Done] Документ '{document?.Title}' уже напечатан, нельзя добавить в очередь");
        }

        public void CompletePrinting(Document document)
        {
            Console.WriteLine($"[State: Done] Документ '{document?.Title}' уже завершен");
        }

        public void FailPrinting(Document document)
        {
            Console.WriteLine($"[State: Done] Невозможно: документ '{document?.Title}' уже напечатан");
        }

        public void Reset(Document document)
        {
            if (document != null)
            {
                document.SetState(new NewState());
                Console.WriteLine($"[State: Done -> New] Документ '{document.Title}' сброшен");
            }
        }
    }
}