using System;
using System.Collections.Generic;
using Test.Data.Entities;
using Test.Services.ViewModels;

namespace Test.Services.Interfaces
{
    public interface IMenuService : IDisposable
    {
        Menu Add(MenuViewModel vm);

        Menu Edit(int id, MenuViewModel vm);

        Menu Delete(int id);

        List<MenuViewModel> GetAll(string keyword);

        void SaveChanges();
    }
}