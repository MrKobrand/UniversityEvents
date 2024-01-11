using System.Linq;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Extensions;

/// <summary>
/// Класс, содержащий расширения для Entity Framework.
/// </summary>
public static class EntityFrameworkExtensions
{
    /// <summary>
    /// Добавляет проверку на наличие измененных принадлежащих сущностей.
    /// </summary>
    /// <param name="entry">Сущность.</param>
    /// <returns>
    /// <see langword="true"/>, если элемент имеет измененные принадлежащие сущности, иначе - <see langword="false"/>.
    /// </returns>
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }

    /// <summary>
    /// Конфигурирует стандартные настройки базовой сущности.
    /// </summary>
    /// <typeparam name="TEntity">Тип, реализующий <see cref="IEntity"/>.</typeparam>
    /// <param name="builder">Билдер настроек сущности.</param>
    /// <returns>Тот же самый объект билдера.</returns>
    public static EntityTypeBuilder<TEntity> ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IEntity
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreateDate)
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(x => x.UpdateDate)
            .HasColumnType("timestamptz")
            .IsRequired();

        return builder;
    }
}
