using System;
using PrinterSystem.Models;

namespace PrinterSystem.States
{
    public class ErrorState : IDocumentState
    {
        public void Print(Document document)
        {
            Console.WriteLine($"[State: Error] Документ '{document?.Title}' - печать невозможна из-за ошибки. Сначала сбросьте документ (Reset)");
        }

        public void AddToQueue(Document document)
        {
            Console.WriteLine($"[State: Error] Документ '{document?.Title}' - нельзя добавить в очередь из-за ошибки. Сначала сбросьте документ");
        }

        public void CompletePrinting(Document document)
        {
            Console.WriteLine($"[State: Error] Ошибка не устранена для документа '{document?.Title}'");
        }

        public void FailPrinting(Document document)
        {
            Console.WriteLine($"[State: Error] Документ '{document?.Title}' уже в состоянии ошибки");
        }

        public void Reset(Document document)
        {
            if (document != null)
            {
                document.SetState(new NewState());
                Console.WriteLine($"[State: Error -> New] Документ '{document.Title}' сброшен и готов к повторной печати");
            }
        }
    }
}