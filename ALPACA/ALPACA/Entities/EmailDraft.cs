
namespace ALPACA.Entities
{
    public class EmailDraft
    {
        public virtual int Id { get; set; }
        public virtual string UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Body { get; set; }
    }
}
