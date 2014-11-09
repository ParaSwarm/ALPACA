using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPACA.Domain.Entities
{
    public class EmailDraft
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Body { get; set; }
    }
}
