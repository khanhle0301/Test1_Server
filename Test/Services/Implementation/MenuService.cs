using System;
using System.Collections.Generic;
using System.Linq;
using Test.Data;
using Test.Data.Entities;
using Test.Exceptions;
using Test.Services.Interfaces;
using Test.Services.ViewModels;

namespace Test.Services.Implementation
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MenuService(
            IRepository<Menu> menuRepository,
            IUnitOfWork unitOfWork)
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
        }

        public Menu Add(MenuViewModel vm)
        {
            var exists = _menuRepository.FindAll(x => x.Name == vm.Name).Any();
            if (exists)
                throw new DuplicatedException($"{vm.Name} đã tồn tại!");

            var model = new Menu
            {
                Name = vm.Name,
                ParentId = vm.ParentId
            };

            return _menuRepository.Add(model);
        }

        public Menu Edit(int id, MenuViewModel vm)
        {
            var exists = _menuRepository.FindAll(x => x.Name == vm.Name && x.Id != id).Any();
            if (exists)
                throw new DuplicatedException($"{vm.Name} đã tồn tại!");

            var menu = _menuRepository.FindById(id);
            if (menu == null)
                throw new NotFoundException($"Menu không tồn tại!");

            menu.Name = vm.Name;
            return _menuRepository.Update(menu);
        }

        public Menu Delete(int id)
        {
            var menu = _menuRepository.FindById(id);
            if (menu == null)
                throw new NotFoundException($"Menu không tồn tại!");

            var children = _menuRepository.FindAll(x => x.ParentId == menu.Id).ToList();
            if (children.Any())
            {
                foreach (var item in children)
                {
                    item.ParentId = null;
                    _menuRepository.Update(item);
                }
            }

            return _menuRepository.Remove(menu);
        }

        public List<MenuViewModel> GetAll(string keyword)
        {
            List<MenuViewModel> data = new List<MenuViewModel>();

            var query = _menuRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            var all = query.ToList();
            var list = all.Where(x => x.ParentId == null);

            foreach (var item in list)
            {
                data.Add(new MenuViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ParentId = item.ParentId,
                    Children = FillRecursive(all, item.Id)
                });
            }

            return data;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private List<MenuViewModel> FillRecursive(List<Menu> all, int parentId)
        {
            List<MenuViewModel> menus = new List<MenuViewModel>();

            var list = all.Where(x => x.ParentId == parentId);
            foreach (var item in list)
            {
                menus.Add(new MenuViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ParentId = item.ParentId,
                    Children = FillRecursive(all, item.Id)
                });
            }

            return menus;
        }
    }
}