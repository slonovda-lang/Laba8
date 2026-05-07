using PrinterSystem.Mediator;
using PrinterSystem.States;

namespace PrinterSystem.Models
{
    public class Document : Colleague
    {
        public string Title { get; set; }
        public IDocumentState State { get; private set; }

        public Document(string title)
        {
            Title = title;
            State = new NewState();
        }

        public void SetState(IDocumentState state) => State = state;

        public void Print() => State.Print(this);
        public void AddToQueue() => State.AddToQueue(this);
        public void CompletePrint() => State.CompletePrinting(this);
        public void FailPrinting() => State.FailPrinting(this);
        public void Reset() => State.Reset(this);

        public void DisplayState()
        {
            string stateName = State.GetType().Name.Replace("State", "");
            Console.WriteLine($"📄 Документ '{Title}' - состояние: {stateName}");
        }
    }
}