namespace ToDo.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public TodoItem() { }
        public TodoItem(int id, string label, bool isDone, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            Label = label;
            IsDone = isDone;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }
    }
}
