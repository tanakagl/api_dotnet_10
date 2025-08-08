using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
 : RepositoryBase<User>(context, logger), IUserRepository;