namespace WarehouseData.Models
{
    public enum InvoiceType { Incoming, Outgoing }

    public class Invoice
    {
        private static int _counter = 0;
        public int Id { get; set; } = ++_counter;
        public string Number { get; set; } = string.Empty;
        public InvoiceType Type { get; set; } = InvoiceType.Incoming;
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsConfirmed { get; set; } = false;
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}