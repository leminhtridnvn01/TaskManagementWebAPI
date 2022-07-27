﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20220725022948_update-entity-project")]
    partial class updateentityproject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Domain.Base.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AffectedColumns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("TaskItemId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskItemId")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TaskItemId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.ListTodo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskItemId");

                    b.ToTable("ListTodoes");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.TagMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("TaskId");

                    b.ToTable("TagMappings");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AssigneeInProgressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<int>("ListTaskId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prioritized")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeInProgressId");

                    b.HasIndex("ListTaskId");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<int?>("ListTodoId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TodoParrentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ListTodoId");

                    b.HasIndex("TodoParrentId");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("Domain.ListTasks.ListTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ListTasks");
                });

            modelBuilder.Entity("Domain.Projects.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Domain.Projects.ProjectMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectMembers");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfBirth")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.Assignment", b =>
                {
                    b.HasOne("Domain.Entities.Tasks.TaskItem", "TaskItem")
                        .WithMany("Assignees")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.Attachment", b =>
                {
                    b.HasOne("Domain.Entities.Tasks.TaskItem", "TaskItem")
                        .WithMany("Attachments")
                        .HasForeignKey("TaskItemId");

                    b.Navigation("TaskItem");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.ListTodo", b =>
                {
                    b.HasOne("Domain.Entities.Tasks.TaskItem", "TaskItem")
                        .WithMany("ListTodoes")
                        .HasForeignKey("TaskItemId");

                    b.Navigation("TaskItem");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.TagMapping", b =>
                {
                    b.HasOne("Domain.Entities.Tasks.Tag", "Tag")
                        .WithMany("TaskItems")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Tasks.TaskItem", "Task")
                        .WithMany("Tags")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.TaskItem", b =>
                {
                    b.HasOne("Domain.Users.User", "AssigneeInProgress")
                        .WithMany()
                        .HasForeignKey("AssigneeInProgressId");

                    b.HasOne("Domain.ListTasks.ListTask", "ListTask")
                        .WithMany("TaskItems")
                        .HasForeignKey("ListTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssigneeInProgress");

                    b.Navigation("ListTask");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.TodoItem", b =>
                {
                    b.HasOne("Domain.Entities.Tasks.ListTodo", "ListTodo")
                        .WithMany("TodoItems")
                        .HasForeignKey("ListTodoId");

                    b.HasOne("Domain.Entities.Tasks.TodoItem", "TodoParrent")
                        .WithMany("SubTodoItems")
                        .HasForeignKey("TodoParrentId");

                    b.Navigation("ListTodo");

                    b.Navigation("TodoParrent");
                });

            modelBuilder.Entity("Domain.ListTasks.ListTask", b =>
                {
                    b.HasOne("Domain.Projects.Project", "Project")
                        .WithMany("ListTasks")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Projects.ProjectMember", b =>
                {
                    b.HasOne("Domain.Users.User", "Member")
                        .WithMany("Projects")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Projects.Project", "Project")
                        .WithMany("Members")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.ListTodo", b =>
                {
                    b.Navigation("TodoItems");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.Tag", b =>
                {
                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.TaskItem", b =>
                {
                    b.Navigation("Assignees");

                    b.Navigation("Attachments");

                    b.Navigation("ListTodoes");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Domain.Entities.Tasks.TodoItem", b =>
                {
                    b.Navigation("SubTodoItems");
                });

            modelBuilder.Entity("Domain.ListTasks.ListTask", b =>
                {
                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("Domain.Projects.Project", b =>
                {
                    b.Navigation("ListTasks");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}