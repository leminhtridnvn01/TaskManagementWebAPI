using Domain.Base;

namespace Domain.Projects.Events
{
    public class DeleteListTaskDomainEvent : BaseDomainEvent
    {
        public int ListTaskId { get; set; }

        public DeleteListTaskDomainEvent(int listTaskId)
        {
            ListTaskId = listTaskId;
        }
    }
}