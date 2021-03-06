﻿using Microsoft.AspNetCore.SignalR;
using nehola.gameoflife.Entities;
using System;
using nehola.gameoflife.entities.Abstract;
using nehola.gameoflife.webapp.Logger;

namespace nehola.gameoflife.webapp.Core
{
    public class GameOfLifeWorker : IObserver<IWorld>
    {
        private IDisposable Unsubscriber { get; set; }

        private LifeSimulation Simulation { get; set; }

        private WebWorldLogger Logger { get; set; }

        public IHubContext HubProxy { get; set; }

        public GameOfLifeWorker(IHubContext hubProxy)
        {
            HubProxy = hubProxy;
        }

        public void Start()
        {
            Simulation = new LifeSimulation(100, 100);
            Logger = new WebWorldLogger();
            Subscribe(Simulation);
            Simulation.Start(0);
        }

        public void Subscribe(IObservable<IWorld> provider)
        {
            if (provider != null)
                Unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted()
        {
            Unsubscribe();
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IWorld world)
        {
            world.Print(Logger);

            HubProxy.Clients.All.GenerationUpdated(Logger.OutPut);

            Logger = new WebWorldLogger();
        }

        public virtual void Unsubscribe()
        {
            Unsubscriber.Dispose();
        }
    }
}