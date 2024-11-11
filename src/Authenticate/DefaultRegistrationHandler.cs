﻿using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.Util;

namespace Taller1.Authenticate;

public class DefaultRegistrationHandler(
    IObjectRepository<User> userRepository,
    ApplicationDbContext applicationDbContext,
    IMapperFactory mapperFactory,
    IEncryptStrategy encryptStrategy
    ) : IRegistrationHandler
{

    private readonly DbSet<User> _users = 
        applicationDbContext.Users;

    private readonly IObjectMapper<UserCreation, User> _toUserMapper =
        mapperFactory.Get<UserCreation, User>();
    
    public RegistrationResponse Register(UserCreation userCreation)
    {
        
        var password = userCreation.Password;
        var repeatPassword = userCreation.RepeatPassword;

        if (password != repeatPassword)
        {
            return RegistrationResponse.
                Error("The password is not the same");
        }

        var userSearched = DbSearchBuilder<User>.NewBuilder(_users)
            .Filter(userSearched => userCreation.Rut == userSearched.Rut)
            .BuildAndGetFirst();

        if (userSearched != null)
        {
            return RegistrationResponse.Error(
                "The user already exists");
        }

        var passwordEncrypt = encryptStrategy.Encrypt(password);
        
        var user = _toUserMapper.Mapper(userCreation,
                ObjectParameters.Create()
                    .AddParameter("password", passwordEncrypt)
            );
        
        userRepository.Push(user);
        return RegistrationResponse.
            Success("Ok");
    }
    
}