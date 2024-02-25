using AutoMapper;
using Sample.Application.UseCases.CompleteTodoItem;
using Sample.Application.UseCases.CreateTodoItem;
using Sample.Domain.Entities;

namespace Sample.Application.AutoMapper
{

    public class SampleServiceMappingProfile : Profile
    {
        public SampleServiceMappingProfile()
        {
            RegisterMappings();
        }

        private void RegisterMappings()
        {
            CreateMap<CreateTodoItemCommand, TodoItem>().ForMember(destination => destination.Id, options => options.MapFrom(x => Guid.NewGuid()));
            CreateMap<TodoItem, CreateTodoItemCommandResult>();

            CreateMap<CompleteTodoItemCommand, TodoItem>();
            CreateMap<TodoItem, CompleteTodoItemCommandResult>();
        }
    }
}
