using PrinterSystem.Models;
using PrinterSystem.States;

namespace PrinterSystem.Mediator
{
    public class PrintSystemMediator : IMediator
    {
        private readonly Printer _printer;
        private readonly PrintQueue _queue;
        private readonly Logger _logger;
        private readonly Dispatcher _dispatcher;

        public PrintSystemMediator(Printer printer, PrintQueue queue, Logger logger, Dispatcher dispatcher)
        {
            _printer = printer;
            _queue = queue;
            _logger = logger;
            _dispatcher = dispatcher;

            _printer.SetMediator(this);
            _queue.SetMediator(this);
            _logger.SetMediator(this);
            _dispatcher.SetMediator(this);
        }

        public void Notify(Colleague sender, string ev, Document document = null)
        {
            switch (ev)
            {
                case "AddToQueue":
                    if (document != null)
                        _queue.EnqueueItem(document);
                    break;

                case "Enqueued":
                    if (document != null)
                        _logger.WriteMessage($"Документ '{document.Title}' помещен в очередь.");
                    break;

                case "RequestPrint":
                    if (document != null)
                    {
                        document.SetState(new PrintingState());
                        _printer.StartPrint(document);
                    }
                    break;

                case "ProcessQueue":
                    if (_queue.IsEmpty)
                    {
                        _logger.WriteMessage("Очередь пуста.");
                        return;
                    }
                    var nextDoc = _queue.DequeueItem();
                    if (nextDoc != null)
                    {
                        nextDoc.SetMediator(this);
                        _logger.WriteMessage($"Начинаем печать документа из очереди: '{nextDoc.Title}'");
                        nextDoc.Print();
                    }
                    break;

                case "PrintSuccess":
                    if (document != null)
                    {
                        document.CompletePrint();
                        _logger.WriteMessage($"Успешно напечатан документ '{document.Title}'.");
                    }
                    break;

                case "PrintFailed":
                    if (document != null)
                    {
                        document.FailPrinting();
                        _logger.WriteMessage($"ОШИБКА печати документа '{document.Title}'.");
                    }
                    break;

                case "ResetDocument":
                    if (document != null)
                    {
                        document.Reset();
                        _logger.WriteMessage($"Документ '{document.Title}' сброшен и может быть отправлен снова.");
                    }
                    break;

                case "AddToQueueFromDispatcher":
                    if (document != null)
                    {
                        _logger.WriteMessage($"Диспетчер добавляет документ '{document.Title}' в очередь.");
                        document.AddToQueue();
                    }
                    break;
            }
        }
    }
}