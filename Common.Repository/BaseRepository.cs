﻿using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : IHasId
    {
        private readonly List<TEntity> _data = new List<TEntity>();

        public TEntity[] GetList(
            int? offset = null,
            int? limit = null,
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            bool? desending = null)
        {

            IEnumerable<TEntity> result = _data;

            if(predicate !=  null)
            {
                result = result.Where(predicate.Compile());
            }


            if (orderBy != null)
            {
                result = desending.GetValueOrDefault() ? result.OrderByDescending(orderBy.Compile()) : result.OrderBy(orderBy.Compile());
            }

            result = result.Skip(offset.GetValueOrDefault());
            
            if(limit.HasValue)
            {
                result = result.Take(limit.Value);
            }

            return result.ToArray();
        }
        public TEntity? Add(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public bool Delete(TEntity item)
        {
            return _data.Remove(item);
        }

        public TEntity? Update(TEntity item)
        {
            var entity = _data.FirstOrDefault(x => x.Id == item.Id);
            _data.Remove(entity);
            _data.Add(item);
            return item;
        }

        public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>>? predicate = null)
        {
            if(predicate is null)
            {
                return _data.SingleOrDefault();
            }
            return _data.SingleOrDefault(predicate.Compile());
        }
    }
}