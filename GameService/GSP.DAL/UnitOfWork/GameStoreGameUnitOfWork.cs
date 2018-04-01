﻿using GSP.Common.DAL.UnitOfWork;
using GSP.Games.DAL.Context;
using GSP.Games.DAL.Repositories.Contracts;
using GSP.Games.DAL.UnitOfWork.Contracts;

namespace GSP.Games.DAL.UnitOfWork
{
    public class GameStoreGameUnitOfWork : GameStoreUnitOfWork<GameStoreGameContext>, IGameStoreGameUnitOfWork
    {
        public GameStoreGameUnitOfWork(GameStoreGameContext context) : base(context)
        { }

        public IGameRepository GameRepository { get; set; }

        public IRateRepository RateRepository { get; set; }

        public ICategoryRepository CategoryRepository { get; set; }
    }
}