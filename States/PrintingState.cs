using System;
using PrinterSystem.Models;

namespace PrinterSystem.States
{
    public class PrintingState : IDocumentState
    {
        public void Print(Document document)
        {
            Console.WriteLine($"[State: Printing] Документ '{document?.Title}' уже печатается");
        }

        public void AddToQueue(Document document)
        {
            Console.WriteLine($"[State: Printing] Документ '{document?.Title}' уже печатается, нельзя добавить в очередь повторно");
        }

        public void CompletePrinting(Document document)
        {
            if (document != null)
            {
                document.SetState(new DoneState());
                Console.WriteLine($"[State: Printing -> Done] Документ '{document.Title}' успешно напечатан");
            }
        }

        public void FailPrinting(Document document)
        {
            if (document != null)
            {
                document.SetState(new ErrorState());
                Console.WriteLine($"[State: Printing -> Error] При печати документа '{document.Title}' произошла ошибка");
            }
        }

        public void Reset(Document document)
        {
            Console.WriteLine($"[State: Printing] Нельзя сбросить документ '{document?.Title}' во время печати");
        }
    }
}