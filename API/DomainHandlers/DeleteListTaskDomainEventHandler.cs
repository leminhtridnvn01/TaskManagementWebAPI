using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.ListTasks;
using Domain.Projects.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.DomainHandlers
{
    public class DeleteListTaskDomainEventHandler : INotificationHandler<DeleteListTaskDomainEvent>
    {
        private readonly IListTaskRepository _listTaskRepository;
        private readonly ITaskItemService _taskItemService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteListTaskDomainEventHandler(
            IListTaskRepository listTaskRepository,
            ITaskItemService taskItemService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _listTaskRepository = listTaskRepository;
            _taskItemService = taskItemService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteListTaskDomainEvent notification, CancellationToken cancellationToken)
        {
            var listTask = await _listTaskRepository.GetAsync(s => s.Id == notification.ListTaskId);

            foreach (var taskItem in listTask.TaskItems)
            {
                await _taskItemService.DeleteTaskItem(taskItem.Id);
            }

            await _listTaskRepository.SoftDeleteAsync(listTask);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}