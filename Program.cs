using System;
using System.Threading;
using PrinterSystem.Models;
using PrinterSystem.Mediator;

namespace PrinterSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var printer = new Printer();
            var queue = new PrintQueue();
            var logger = new Logger();
            var dispatcher = new Dispatcher();

            var mediator = new PrintSystemMediator(printer, queue, logger, dispatcher);

            var doc1 = new Document("Отчет по проекту.pdf");
            var doc2 = new Document("Договор_подряда.docx");
            var doc3 = new Document("Презентация_итоги.pptx");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📌 НАЧАЛЬНЫЕ СОСТОЯНИЯ ДОКУМЕНТОВ:");
            Console.ResetColor();
            doc1.DisplayState();
            doc2.DisplayState();
            doc3.DisplayState();
            Console.WriteLine();

            // СЦЕНАРИЙ 1: Успешная печать
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("СЦЕНАРИЙ 1: УСПЕШНАЯ ПЕЧАТЬ");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.ResetColor();

            dispatcher.AddDocumentToQueue(doc1);
            dispatcher.CommandProcessQueue();
            Thread.Sleep(500);
            doc1.DisplayState();
            Console.WriteLine();

            // СЦЕНАРИЙ 2: Ошибка принтера и восстановление
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("СЦЕНАРИЙ 2: ОШИБКА ПРИНТЕРА И ВОССТАНОВЛЕНИЕ");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.ResetColor();

            printer.SimulateFailure = true;
            dispatcher.AddDocumentToQueue(doc2);
            dispatcher.CommandProcessQueue();
            Thread.Sleep(500);
            doc2.DisplayState();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("🔄 Восстановление документа после ошибки...");
            Console.ResetColor();
            dispatcher.ResetDocument(doc2);
            doc2.DisplayState();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("📤 Повторная отправка восстановленного документа...");
            Console.ResetColor();
            dispatcher.AddDocumentToQueue(doc2);
            dispatcher.CommandProcessQueue();
            Thread.Sleep(500);
            doc2.DisplayState();
            Console.WriteLine();

            // СЦЕНАРИЙ 3: Проверка финального состояния
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("СЦЕНАРИЙ 3: ПРОВЕРКА ФИНАЛЬНОГО СОСТОЯНИЯ");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.ResetColor();

            dispatcher.AddDocumentToQueue(doc3);
            dispatcher.CommandProcessQueue();
            Thread.Sleep(500);
            doc3.DisplayState();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("🧪 Попытка повторной печати уже напечатанного документа:");
            Console.ResetColor();
            dispatcher.AddDocumentToQueue(doc1);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("ИТОГОВЫЕ СОСТОЯНИЯ ДОКУМЕНТОВ:");
            Console.ResetColor();
            doc1.DisplayState();
            doc2.DisplayState();
            doc3.DisplayState();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Программа завершена. Нажмите любую клавишу для выхода...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}