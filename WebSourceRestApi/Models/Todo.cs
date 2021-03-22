using System;

namespace WebSourceRestApi.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
