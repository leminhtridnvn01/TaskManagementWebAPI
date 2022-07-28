using Domain.Interfaces;
using Domain.Projects;
using Domain.Users.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.DomainHandlers
{
    public class CreateDefaultProjectWhenRegisterDomainEventHandler : INotificationHandler<CreateDefaultProjectWhenRegisterDomainEvent>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDefaultProjectWhenRegisterDomainEventHandler(
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateDefaultProjectWhenRegisterDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = notification.User;

            var newProject = new Project("Default Project", "This is description");
            newProject.AddMember(user.Id);
            await _projectRepository.AddAsync(newProject);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}