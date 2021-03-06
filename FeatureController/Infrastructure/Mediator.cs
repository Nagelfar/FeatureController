﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureController.Common;

namespace FeatureController.Infrastructure
{
    public class Mediator:IMediator
    {
        private readonly IFindQueryHandlers _queryHandlers;
        public interface IFindQueryHandlers{
            dynamic Handler<TResponse>(IQuery<TResponse> query);
        }

        public Mediator(IFindQueryHandlers queryHandlers)
        {
            _queryHandlers = queryHandlers;
        }
        public TResponse Response<TResponse>(IQuery<TResponse> query)
        {
            var handler = _queryHandlers.Handler<TResponse>(query);

            return handler.Handle((dynamic)query);
        }
    }
}