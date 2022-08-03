﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Simple.Repository.Models;

public abstract class EntityBase
{
    /// <summary>
    /// 使用 Fluent API 配置实体
    /// </summary>
    /// <param name="builder"></param>
    public virtual void ConfigureEntity(ModelBuilder builder)
    {
    }
}

public abstract class EntityBase<TKey> : EntityBase
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Key]
    public virtual TKey Id { get; set; }
}

/// <summary>
/// 业务基础
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class BusinessEntityBase<TKey> : EntityBase<TKey>, ISoftDelete, ICreatedInfo, IUpdatedInfo
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// 软删标记
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTime? CreatedTime { get; set; }

    /// <summary>
    /// 创建用户Id
    /// </summary>
    public virtual Guid? CreatedUserId { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public virtual DateTime? UpdatedTime { get; set; }

    /// <summary>
    /// 更新用户Id
    /// </summary>
    public virtual Guid? UpdatedUserId { get; set; }
}
