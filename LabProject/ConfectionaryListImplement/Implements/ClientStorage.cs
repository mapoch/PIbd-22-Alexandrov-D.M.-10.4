﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryListImplement.Models;

namespace ConfectionaryListImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly DataListSingleton source;

        public ClientStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ClientViewModel> GetFullList()
        {
            var result = new List<ClientViewModel>();
            foreach (var client in source.Clients) result.Add(CreateModel(client));
            return result;
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null) return null;

            var result = new List<ClientViewModel>();
            foreach (var client in source.Clients)
            {
                if (client.FIO.Contains(model.FIO)) result.Add(CreateModel(client));
            }
            return result;
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null) return null;

            foreach (var client in source.Clients)
            {
                if (client.Id == model.Id || client.FIO == model.FIO) return CreateModel(client);
            }
            return null;
        }

        public void Insert(ClientBindingModel model)
        {
            var tempClient = new Client { Id = 1 };

            foreach (var client in source.Clients)
            {
                if (client.Id >= tempClient.Id) tempClient.Id = client.Id + 1;
            }

            source.Clients.Add(CreateModel(model, tempClient));
        }

        public void Update(ClientBindingModel model)
        {
            Client tempClient = null;

            foreach (var client in source.Clients)
            {
                if (client.Id == model.Id) tempClient = client;
            }

            if (tempClient == null) throw new Exception("Элемент не найден");

            CreateModel(model, tempClient);
        }

        public void Delete(ClientBindingModel model)
        {
            for (int i = 0; i < source.Components.Count; ++i)
            {
                if (source.Components[i].Id == model.Id.Value)
                {
                    source.Components.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private static Client CreateModel(ClientBindingModel model, Client client)
        {
            client.FIO = model.FIO;
            client.Login = model.Login;
            client.Password = model.Password;
            return client;
        }

        private static ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel { Id = client.Id, FIO = client.FIO, Login = client.Login, Password = client.Password };
        }
    }
}