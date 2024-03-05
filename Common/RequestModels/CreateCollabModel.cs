using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RequestModels
{
    public class CreateCollabModel
    {
        public string CollabEmail { get; set; }
        public bool Trash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
