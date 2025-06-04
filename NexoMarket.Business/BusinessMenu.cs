using NexoMarket.Data.Mapper;
using NexoMarket.Data;
using NexoMarket.Data.Repository;
using NexoMarket.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BusinessMenu
    {
        private readonly MenuRepository _menuRepository;
        public BusinessMenu()
        {
            _menuRepository = new MenuRepository();
        }

        public List<MenuEntity> GetMenusByUser(int userId)
        {
            return _menuRepository.GetMenusByUser(userId);
        }
    }
}
