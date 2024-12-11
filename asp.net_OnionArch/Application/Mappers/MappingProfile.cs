using Application.DTOs;
using Application.Features.Authors.Commands;
using Application.Features.Books.Commands;
using Application.Features.BookTypes.Commands;
using Application.Features.Users.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User
        CreateMap<User, UserDto>();
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
            .ForMember(dest => dest.Phone, opt => opt.Condition(src => src.Phone != null));
        
        //Author
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorCommand, Author>();
        CreateMap<UpdateAuthorCommand, Author>()
            .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null));
        
        //BookType
        CreateMap<BookType, BookTypeDto>();
        CreateMap<CreateBookTypeCommand, BookType>();
        CreateMap<UpdateBookTypeCommand, BookType>()
            .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null));
        
        //Book
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.BookTypeId, opt => opt.MapFrom(src => src.BookTypeId));
        CreateMap<CreateBookCommand, Book>();
        CreateMap<UpdateBookCommand, Book>()
            .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
            .ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price != 0))
            .ForMember(dest => dest.AuthorId, opt => opt.Condition(src => src.AuthorId != 0))
            .ForMember(dest => dest.BookTypeId, opt => opt.Condition(src => src.BookTypeId != 0))
            .ForMember(dest => dest.ReleaseDate, opt => opt.Condition(src => src.ReleaseDate != default));
    }
}