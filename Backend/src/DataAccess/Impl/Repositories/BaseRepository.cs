namespace DataAccess.Impl.Repositories
{
    using System;
    using System.Linq;

    using AutoMapper;

    using DataAccess.Dto;

    using Microsoft.EntityFrameworkCore;

    using Services;

    internal abstract class BaseRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly AppDbContext DbContext;
        protected readonly IUserService UserService;
        protected readonly IPermissionsService PermissionsService;
        protected readonly IMapper Mapper;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(AppDbContext dbContext, IUserService userService, IPermissionsService permissionsService, IMapper mapper)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.UserService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.PermissionsService = permissionsService ?? throw new ArgumentNullException(nameof(permissionsService));
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.DbSet = dbContext.Set<TEntity>() ?? throw new InvalidOperationException("Передан контекст, который не содержит DbSet указанного типа");
        }

        protected virtual void ValidatePermissionsOrThrow(long chatId, Func<ChatPolicy, bool> checkPermissionFn, string exceptionMessage)
        {
            var currentUserId = this.UserService.GetCurrentUserId();
            if (!this.PermissionsService.CheckPermision(chatId, currentUserId, checkPermissionFn))
            {
                throw new DataAccess.Exceptions.AccessViolationException(exceptionMessage);
            }
        }

        protected virtual void ValidateChatTypeOrThrow(long chatId, Func<ChatType, bool> checkChatTypeFn, string exceptionMessage)
        {
            var chatType = this.DbContext.Chats.Where(x => x.Id == chatId).Select(x => x.ChatType).FirstOrDefault();
            if (!checkChatTypeFn(chatType))
            {
                throw new InvalidOperationException(exceptionMessage);
            }
        }

        protected virtual TEntity GetEntity(long entityId)
        {
            return this.DbSet.Find(new[] { entityId });
        }

        protected IQueryable<TProjection> ProjectTo<TProjection>(IQueryable source)
        {
            return this.Mapper.ProjectTo<TProjection>(source);
        }

        protected void SaveChanges()
        {
            this.DbContext.SaveChanges();
        }
    }
}
