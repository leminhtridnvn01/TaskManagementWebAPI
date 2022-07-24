using Domain.ListTasks;
using Domain.Projects;
using Domain.Entities.Tasks;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Infrastructure.Extensions;
using System.Linq;
using Domain._Histories;
using System.Collections.Generic;
using Domain.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System;

namespace Infrastructure.Data
{
    public class EFContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EFContext()
        {
        }

        public EFContext(DbContextOptions<EFContext> options, IMediator mediator, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ListTask> ListTasks { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ListTodo> ListTodoes { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<TagMapping> TagMappings { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("server=ADMIN\\MINHTRI;database=TaskManagement;user id=sa;password=123456;");
        }

        protected override void OnModelCreating (ModelBuilder  modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<Project>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<ListTask>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<TaskItem>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<Attachment>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<Tag>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<ListTodo>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<TodoItem>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<Assignment>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<ProjectMember>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<TagMapping>().HasQueryFilter(p => p.IsDeleted == false);
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
        public void OnBeforeSaveChanges()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) userId = "0";
            ChangeTracker.DetectChanges();
            var auditEntries = new List<HistoryEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is History || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new HistoryEntry();
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = Convert.ToInt32(userId);
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = HistoryType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = HistoryType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = HistoryType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                Histories.Add(auditEntry.ToAudit());
            }
        }
    }
}
